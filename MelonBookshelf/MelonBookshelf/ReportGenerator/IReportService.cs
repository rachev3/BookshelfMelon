﻿using MelonBookshelf.ReportGenerator;

namespace MelonBookshelf.ReportGenrator
{
    public interface IReportService
    {
        Task Data(DateTime date);
        Task ExcelConfiguration(List<ReportData> data);
        Task<EmailJsonPayload> GenerateJsonPayload(DateTime date);
        Task ImportDataFromExternalSource(Stream inputStream);
    }
}
