namespace Leo.TargetWordCount.Contracts
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IWordCountBatchTaskSettings))]
    internal abstract class IWordCountBatchTaskSettingsContract : IWordCountBatchTaskSettings
    {
        public string Culture
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
            }
        }

        public List<InvoiceItem> InvoiceRates
        {
            get
            {
                Contract.Ensures(Contract.Result<List<InvoiceItem>>() != null);
                return default(List<InvoiceItem>);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }

        public bool ReportLockedSeperately { get; set; }

        public bool UseSource { get; set; }
    }
}