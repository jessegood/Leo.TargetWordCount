using System.Diagnostics.Contracts;
namespace Leo.TargetWordCount
{
    using Models;
    using System.Xml.Linq;
    using System.Diagnostics.Contracts;
    using System;
    public class ReportBuilder
    {
        private readonly XElement root = new XElement("Report");
        private decimal totalAmount = 0M;

        public void BuildFileTable(CountTotal total, IWordCountBatchTaskSettings settings)
        {
            Contract.Requires<ArgumentNullException>(total != null);
            Contract.Requires<ArgumentNullException>(settings != null);

            var parent = new XElement("File",
                                     new XAttribute("Name", total.FileName),
                                     new XAttribute("CountType", total.CountMethod == CountUnit.Character ? "Characters" : "Words"));

            BuildTable(total, settings, parent);

            root.Add(parent);
        }

        public void BuildTotalTable(CountTotal total, IWordCountBatchTaskSettings settings)
        {
            Contract.Requires<ArgumentNullException>(total != null);
            Contract.Requires<ArgumentNullException>(settings != null);

            var parent = new XElement("GrandTotal");

            BuildTable(total, settings, parent);

            root.Add(parent);
        }

        public string GetReport()
        {
            return root.ToString();
        }

        private void BuildTable(CountTotal total, IWordCountBatchTaskSettings settings, XElement parent)
        {
            parent.Add(CreateXElement(CountTotal.ContextMatch, RateType.PerfectMatch, total, settings));
            parent.Add(CreateXElement(CountTotal.ContextMatch, RateType.ContextMatch, total, settings));
            parent.Add(CreateXElement(CountTotal.Repetitions, RateType.Repetitions, total, settings));
            parent.Add(CreateXElement(CountTotal.CrossFileRepetitions, RateType.CrossFileRepetitions, total, settings));
            parent.Add(CreateXElement(CountTotal.OneHundredPercent, RateType.OneHundred, total, settings));
            parent.Add(CreateXElement(CountTotal.NinetyFivePercent, RateType.NinetyFive, total, settings));
            parent.Add(CreateXElement(CountTotal.EightyFivePercent, RateType.EightyFive, total, settings));
            parent.Add(CreateXElement(CountTotal.SeventyFivePercent, RateType.SeventyFive, total, settings));
            parent.Add(CreateXElement(CountTotal.FiftyPercent, RateType.Fifty, total, settings));
            parent.Add(CreateXElement(CountTotal.New, RateType.New, total, settings));
            parent.Add(CreateXElement(CountTotal.Total, RateType.Total, total, settings));
        }

        private XElement CreateXElement(string item, RateType type, CountTotal total, IWordCountBatchTaskSettings settings)
        {
            var countData = total.Totals[item];
            var count = ReferenceEquals(total.CountMethod, CountUnit.Character) ? countData.Characters : countData.Words;
            var segments = countData.Segments;
            if (RateType.Total != type)
            {
                var invoiceItem = settings.InvoiceRates[(int)type];

                decimal rate = 0M;
                if (!string.IsNullOrEmpty(invoiceItem.Rate))
                {
                    rate = decimal.Parse(invoiceItem.Rate, System.Globalization.NumberStyles.Currency, CultureRepository.Cultures[settings.Culture]);
                }

                var amount = count * rate;
                totalAmount += amount;
                return new XElement(item,
                               new XAttribute("Segments", segments),
                               new XAttribute("Count", count),
                               new XAttribute("Rate", invoiceItem.Rate),
                               new XAttribute("Amount", (amount).ToString("C", CultureRepository.Cultures[settings.Culture])));
            }
            else
            {
                var amount = totalAmount;
                totalAmount = 0M;

                return new XElement(item,
                               new XAttribute("Segments", segments),
                               new XAttribute("Count", count),
                               new XAttribute("Amount", amount.ToString("C", CultureRepository.Cultures[settings.Culture])));
            }
        }
    }
}