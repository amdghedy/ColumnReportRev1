using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColumnReport
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ColumnReportExporter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            // Show the form to get the file path
            using (MainForm form = new MainForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string filePath = form.FilePath;

                    // Filter for structural columns
                    FilteredElementCollector collector = new FilteredElementCollector(doc)
                        .OfCategory(BuiltInCategory.OST_StructuralColumns)
                        .WhereElementIsNotElementType();

                    // Export data to Excel
                    ExportToExcel(doc, collector, filePath);

                    // Open the exported Excel file
                    OpenExcelFile(filePath);
                }
            }

            return Result.Succeeded;
        }

        private void ExportToExcel(Document doc, FilteredElementCollector collector, string filePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook workbook = null;
            Worksheet worksheet = null;

            try
            {
                // Initialize Excel
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                workbook = excelApp.Workbooks.Add();
                worksheet = (Worksheet)workbook.Worksheets[1];

                // Write headers
                string[] headers = {
                    "Family Name", "Type Name", "Element ID", "Easting (m)", "Northing (m)",
                    "Base Level", "Base Offset (m)", "Top Level", "Top Offset (m)", "Height (m)", "Volume (m^3)"
                };
                WriteHeaders(worksheet, headers);

                // Write data to Excel
                WriteDataToExcel(doc, collector, worksheet);

                // Save and close Excel
                workbook.SaveAs(filePath);
                workbook.Close(false);
                excelApp.Quit();
            }
            finally
            {
                // Release COM objects
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }

        private void WriteHeaders(Worksheet worksheet, string[] headers)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1] = headers[i];
            }
        }

        private void WriteDataToExcel(Document doc, FilteredElementCollector collector, Worksheet worksheet)
        {
            int row = 2;
            foreach (Element elem in collector)
            {
                FamilyInstance column = elem as FamilyInstance;
                if (column != null)
                {
                    string familyName = column.Symbol.Family.Name;
                    string typeName = column.Name;
                    int elementId = column.Id.IntegerValue;
                    LocationPoint location = column.Location as LocationPoint;
                    XYZ point = location.Point;
                    double easting = UnitUtils.ConvertFromInternalUnits(point.X, UnitTypeId.Meters);
                    double northing = UnitUtils.ConvertFromInternalUnits(point.Y, UnitTypeId.Meters);
                    Level baseLevel = doc.GetElement(column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId()) as Level;
                    double baseOffset = UnitUtils.ConvertFromInternalUnits(column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).AsDouble(), UnitTypeId.Meters);
                    Level topLevel = doc.GetElement(column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).AsElementId()) as Level;
                    double topOffset = UnitUtils.ConvertFromInternalUnits(column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).AsDouble(), UnitTypeId.Meters);
                    double height = UnitUtils.ConvertFromInternalUnits(column.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM).AsDouble(), UnitTypeId.Meters);
                    double volume = UnitUtils.ConvertFromInternalUnits(column.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), UnitTypeId.CubicMeters);

                    worksheet.Cells[row, 1] = familyName;
                    worksheet.Cells[row, 2] = typeName;
                    worksheet.Cells[row, 3] = elementId;
                    worksheet.Cells[row, 4] = easting;
                    worksheet.Cells[row, 5] = northing;
                    worksheet.Cells[row, 6] = baseLevel?.Name;
                    worksheet.Cells[row, 7] = baseOffset;
                    worksheet.Cells[row, 8] = topLevel?.Name;
                    worksheet.Cells[row, 9] = topOffset;
                    worksheet.Cells[row, 10] = height;
                    worksheet.Cells[row, 11] = volume;

                    row++;
                }
            }
        }

        private void OpenExcelFile(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open the file. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
