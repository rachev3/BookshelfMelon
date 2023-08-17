using MelonBookshelf.Controllers.API;
using MelonBookshelf.Data.DTO;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using MelonBookshelf.ReportGenerator;
using MimeKit;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
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

        public async Task ImportDataFromExternalSource(Stream inputStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(inputStream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                var importedData = new List<ReportData>();
            if (worksheet != null)
            {

                int rowCount = worksheet.Dimension.Rows;
                for (int row = 3; row <= rowCount; row++) 
                {
                    var downloadDate = worksheet.Cells[row, 1].GetValue<DateTime>().Date;
                    var resourceName = worksheet.Cells[row, 2].GetValue<string>();
                    var downloadCount = worksheet.Cells[row, 3].GetValue<int>();

                    var reportData = new ReportData
                    {
                        DownloadDate = downloadDate.ToString(),
                        ResourceName = resourceName,
                        DownloadCount = downloadCount
                    };

                    importedData.Add(reportData);
                }

               
            }
            else
            {
                
            }
            foreach(var item in importedData)
            {
               var resource  = await resourceService.GetByName(item.ResourceName);
                if(resource == null)
                {
                    Resource newResource = new Resource {
                        Title = item.ResourceName,
                        Type = MelonBookshelf.Data.ResourceType.Digital
                    };
                     await resourceService.Add(newResource);
                }
            }
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
        public async Task<EmailJsonPayload> GenerateJsonPayload(DateTime date)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<ResourceDownloadHistory> history = await resourceService.ReportData(date);

            List<ReportData> groupedHistory = history
                 .GroupBy(item => item.ResourceName)
                 .Select(group => new ReportData
                 {
                     DownloadDate = group.First().DownloadDate.Date.Date.ToString(),
                     ResourceName = group.Key,
                     DownloadCount = group.Count()
                 }).ToList();

            var file = new FileInfo(_config.GetValue<string>("ReportStorage:Path"));

            if (file.Exists)
            {
                file.Delete();
            }

            using (var package = new ExcelPackage(file))
            {
                var ws = package.Workbook.Worksheets.Add("Report");

                var range = ws.Cells["A2"].LoadFromCollection(groupedHistory, true);
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

            // Read the generated Excel file into a byte array
            byte[] fileContent = File.ReadAllBytes(file.FullName);


            var excelFile = new FormFile(new MemoryStream(fileContent), 0, fileContent.Length, file.Name, file.Name);

            var generatedFilePath = _config.GetValue<string>("ReportStorage:Path");

            await File.WriteAllBytesAsync(generatedFilePath, fileContent);

            EmailJsonPayload emailJsonPayload = new EmailJsonPayload(){
                FilePathAttachments = generatedFilePath
            };

            return emailJsonPayload;
        }
    }
}
