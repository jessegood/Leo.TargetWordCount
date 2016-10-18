namespace Leo.TargetWordCount
{
    using Models;
    using Sdl.Core.Globalization;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using System.Diagnostics.Contracts;
    using System;
    using System.Diagnostics.CodeAnalysis;
    public static class ReportGenerator
    {
        private const string Characters = "Characters";
        private const string Words = "Words";

        public static string Generate(List<ISegmentWordCounter> counters, IWordCountBatchTaskSettings settings)
        {
            Contract.Requires<ArgumentNullException>(counters != null);
            Contract.Requires<ArgumentNullException>(settings != null);

            CountTotal grandTotal = new CountTotal();
            List<CountTotal> fileData = new List<CountTotal>();

            CollectFileData(counters, settings, grandTotal, fileData);

            grandTotal.CountMethod = fileData.First().CountMethod;
            grandTotal.FileName = "Total";

            return CreateReport(grandTotal, fileData, settings);
        }

        private static void AccumulateCountData(IWordCountBatchTaskSettings settings, ISegmentWordCounter counter, CountTotal info)
        {
            info.FileName = counter.FileName;

            SetCountMethod(settings, counter, info);

            foreach (var segInfo in counter.FileCountInfo.SegmentCounts)
            {
                var origin = segInfo.TranslationOrigin;

                if (settings.ReportLockedSeperately && segInfo.IsLocked)
                {
                    info.Increment(CountTotal.Locked, segInfo.CountData);
                }
                else if (origin.OriginType == "document-match")
                {
                    info.Increment(CountTotal.PerfectMatch, segInfo.CountData);
                }
                else if (origin.IsRepeated)
                {
                    info.Increment(CountTotal.Repetitions, segInfo.CountData);
                }
                else if (origin.IsStructureContextMatch)
                {
                    info.Increment(CountTotal.ContextMatch, segInfo.CountData);
                }
                else if (origin.MatchPercent == 100)
                {
                    info.Increment(CountTotal.OneHundredPercent, segInfo.CountData);
                }
                else if (origin.MatchPercent >= 95)
                {
                    info.Increment(CountTotal.NinetyFivePercent, segInfo.CountData);
                }
                else if (origin.MatchPercent >= 85)
                {
                    info.Increment(CountTotal.EightyFivePercent, segInfo.CountData);
                }
                else if (origin.MatchPercent >= 75)
                {
                    info.Increment(CountTotal.SeventyFivePercent, segInfo.CountData);
                }
                else if (origin.MatchPercent >= 50)
                {
                    info.Increment(CountTotal.FiftyPercent, segInfo.CountData);
                }
                else
                {
                    info.Increment(CountTotal.New, segInfo.CountData);
                }

                info.Increment(CountTotal.Total, segInfo.CountData);
            }
        }

        private static void CollectFileData(List<ISegmentWordCounter> counters, IWordCountBatchTaskSettings settings, CountTotal grandTotal, List<CountTotal> fileData)
        {
            foreach (var counter in counters)
            {
                var info = new CountTotal();

                AccumulateCountData(settings, counter, info);

                fileData.Add(info);
                grandTotal.Increment(info);
            }
        }

        private static string CreateReport(CountTotal grandTotal, List<CountTotal> fileData, IWordCountBatchTaskSettings settings)
        {
            ReportBuilder builder = new ReportBuilder();

            // Build grand total table
            builder.BuildTotalTable(grandTotal, settings);

            // Build individual file tables
            foreach (var data in fileData)
            {
                builder.BuildFileTable(data, settings);
            }

            return builder.GetReport();
        }

        private static void SetCountMethod(IWordCountBatchTaskSettings settings, ISegmentWordCounter counter, CountTotal info)
        {
            Language language = null;

            if (settings.UseSource)
            {
                language = counter.FileCountInfo.SourceInfo;
            }
            else
            {
                language = counter.FileCountInfo.TargetInfo;
            }

            if (language.UsesCharacterCounts)
            {
                info.CountMethod = CountUnit.Character;
            }
            else
            {
                info.CountMethod = CountUnit.Word;
            }
        }
    }
}