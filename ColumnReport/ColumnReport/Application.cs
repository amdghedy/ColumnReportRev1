using System;
using System.IO;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ColumnReport
{
    public class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a new ribbon panel
            string tabName = "KAITECH-BD-R06";
            string panelName = "Structure";
            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, panelName);

            // Create a push button to trigger the ColumnReport command
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData(
                "ColumnsReportButton",
                "Columns Report",
                assemblyPath,
                "ColumnReport.ColumnReportExporter"
            );

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Export structural columns report to Excel.";
            Uri uriImage = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "icon.png"));
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // Clean up when Revit shuts down
            return Result.Succeeded;
        }
    }
}
