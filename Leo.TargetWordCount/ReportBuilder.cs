namespace Leo.TargetWordCount
{
    using Models;
    using System;
    using System.Diagnostics.Contracts;
    using System.Xml.Linq;

    public class ReportBuilder
    {
        private readonly XElement root = new XElement("Report");
        private decimal totalAmount = 0M;

        public void BuildFileTable(CountTotal total, IWordCountBatchTaskSettings settings)
        {
            Contract.Requires<ArgumentNullException>(total != null);
            Contract.Requires<ArgumentNullException>(settings != null);

            string countType = GetCountType(total, settings);

            var parent = new XElement("File",
                                     new XAttribute("Name", total.FileName),
                                     new XAttribute("CountType", countType));

            BuildTable(total, settings, parent);

            root.Add(parent);
        }

        public void BuildTotalTable(CountTotal total, IWordCountBatchTaskSettings settings)
        {
            Contract.Requires<ArgumentNullException>(total != null);
            Contract.Requires<ArgumentNullException>(settings != null);

            string countType = GetCountType(total, settings);

            var parent = new XElement("GrandTotal",
                                      new XAttribute("CountType", countType));

            BuildTable(total, settings, parent);

            root.Add(parent);
        }

        public string GetReport()
        {
            return root.ToString();
        }

        private static string GetCountType(CountTotal total, IWordCountBatchTaskSettings settings)
        {
            string countType = total.CountMethod == CountUnit.Character ? "Characters" : "Words";

            if (settings.UseLineCount)
            {
                countType = "Number of Lines";
            }

            return countType;
        }
        private void BuildTable(CountTotal total, IWordCountBatchTaskSettings settings, XElement parent)
        {
            if (settings.UseLineCount)
            {
                if (settings.ReportLockedSeperately)
                {
                    parent.Add(CreateXElement(CountTotal.Locked, RateType.Locked, total, settings));
                }

                parent.Add(CreateXElement(CountTotal.Total, RateType.Total, total, settings));
            }
            else
            {
                if (settings.ReportLockedSeperately)
                {
                    parent.Add(CreateXElement(CountTotal.Locked, RateType.Locked, total, settings));
                }

                parent.Add(CreateXElement(CountTotal.PerfectMatch, RateType.PerfectMatch, total, settings));
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
        }

        private XElement CreateXElement(string item, RateType type, CountTotal total, IWordCountBatchTaskSettings settings)
        {
            var countData = total.Totals[item];
            var count = (total.CountMethod == CountUnit.Character) ? countData.Characters : countData.Words;
            var segments = countData.Segments;

            var invoiceItem = settings.InvoiceRates[(int)type];

            decimal rate = 0M;
            if (!string.IsNullOrEmpty(invoiceItem.Rate))
            {
                rate = decimal.Parse(invoiceItem.Rate, System.Globalization.NumberStyles.Currency, CultureRepository.Cultures[settings.Culture]);
            }

            var amount = count * rate;
            int output = 0;
            if (settings.UseLineCount)
            {
                if (!string.IsNullOrWhiteSpace(settings.CharactersPerLine))
                {
                    if (int.TryParse(settings.CharactersPerLine, out output))
                    {
                        var num = Math.Round(Convert.ToDecimal(countData.Characters) / Convert.ToDecimal(output), MidpointRounding.AwayFromZero);
                        count = Convert.ToInt32(num);
                        amount = count * rate;
                    }
                }
            }

            totalAmount += amount;

            if (RateType.Total != type)
            {
                if (settings.UseLineCount)
                {
                    return new XElement(item,
                                new XAttribute("Segments", segments),
                                new XAttribute("TotalCharacters", countData.Characters),
                                new XAttribute("CharactersPerLine", output),
                                new XAttribute("Count", count),
                                new XAttribute("Rate", string.IsNullOrWhiteSpace(invoiceItem.Rate) ? "0" : invoiceItem.Rate),
                                new XAttribute("Amount", amount.ToString("C2", CultureRepository.Cultures[settings.Culture])));
                }
                else
                {
                    return new XElement(item,
                                new XAttribute("Segments", segments),
                                new XAttribute("Count", count),
                                new XAttribute("Rate", string.IsNullOrWhiteSpace(invoiceItem.Rate) ? "0" : invoiceItem.Rate),
                                new XAttribute("Amount", amount.ToString("C2", CultureRepository.Cultures[settings.Culture])));
                }
            }
            else
            {
                var t = totalAmount;
                totalAmount = 0M;

                if (settings.UseLineCount)
                {
                    return new XElement(item,
                               new XAttribute("Segments", segments),
                               new XAttribute("TotalCharacters", countData.Characters),
                               new XAttribute("CharactersPerLine", output),
                               new XAttribute("Count", count),
                               new XAttribute("Rate", string.IsNullOrWhiteSpace(invoiceItem.Rate) ? "0" : invoiceItem.Rate),
                               new XAttribute("Amount", t.ToString("C2", CultureRepository.Cultures[settings.Culture])));
                }
                else
                {
                    return new XElement(item,
                               new XAttribute("Segments", segments),
                               new XAttribute("Count", count),
                               new XAttribute("Rate", string.IsNullOrWhiteSpace(invoiceItem.Rate) ? "0" : invoiceItem.Rate),
                               new XAttribute("Amount", t.ToString("C2", CultureRepository.Cultures[settings.Culture])));
                }

            }
        }
    }
}