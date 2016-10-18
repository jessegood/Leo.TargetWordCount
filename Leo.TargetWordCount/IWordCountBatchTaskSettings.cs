namespace Leo.TargetWordCount
{
    using Contracts;
    using Models;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(IWordCountBatchTaskSettingsContract))]
    public interface IWordCountBatchTaskSettings
    {
        List<InvoiceItem> InvoiceRates { get; set; }
        bool ReportLockedSeperately { get; set; }
        bool UseSource { get; set; }
        string Culture { get; set; }
    }
}