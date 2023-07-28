namespace MelonBookshelf.Models.ExcelReport
{
    public class ExcelReportModel
    {
        public ExcelReportModel()
        {

        }

        public ExcelReportModel(DateTime date, string resourceName, int downloadCount)
        {
            Date = date;
            ResourceName = resourceName;
            DownloadCount = downloadCount;
        }

        public DateTime Date { get; set; }
        public string ResourceName { get; set; }
        public int DownloadCount { get; set; }
    }
}
