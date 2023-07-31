namespace MelonBookshelf.ReportGenrator
{
    public interface IReportService
    {
        public Task Data(DateTime date);
        public Task ExcelConfiguration(List<ReportData> data);
    }
}
