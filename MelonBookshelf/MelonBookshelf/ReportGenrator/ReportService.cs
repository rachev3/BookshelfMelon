using MelonBookshelf.Data.DTO;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MelonBookshelf.ReportGenrator
{
    public class ReportService:IReportService
    {
        private readonly IResourceService resourceService;
        private readonly IConfiguration _config;

        public ReportService(IResourceService resourceService, IConfiguration configuration)
        {
            this.resourceService = resourceService;
            this._config = configuration;
        }

        public async Task Data(DateTime date)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<ResourceDownloadHistory> history = await resourceService.ReportData(date);

            List<ReportData> groupedHistory = history
                 .GroupBy(item => item.ResourceName)
                 .Select(group => new ReportData
                 {
                     DownloadDate = group.First().DownloadDate.Date.ToString(),
                     ResourceName = group.Key,
                     DownloadCount = group.Count()
                 }).ToList();

            

            await ExcelConfiguration(groupedHistory);


        }
        public async Task ExcelConfiguration(List<ReportData> data)
        {
            var file = new FileInfo(_config.GetValue<string>("ReportStorage:Path"));

            if (file.Exists)
            {
                file.Delete();
            }
            using var package = new ExcelPackage(file);

            var ws = package.Workbook.Worksheets.Add("Report");

            var range = ws.Cells["A2"].LoadFromCollection(data, true);
            range.AutoFitColumns();

            ws.Cells["A1"].Value = "Daily Download Report";
            ws.Cells["A1:C1"].Merge = true;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(1).Style.Font.Size = 24;
            ws.Row(1).Style.Font.Color.SetColor(Color.Blue);

            ws.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(2).Style.Font.Bold = true;
            ws.Column(3).Width = 20;

            await package.SaveAsync();
        }
    }
}
