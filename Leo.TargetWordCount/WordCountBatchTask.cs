namespace Leo.TargetWordCount
{
    using Sdl.FileTypeSupport.Framework.IntegrationApi;
    using Sdl.ProjectAutomation.AutomaticTasks;
    using Sdl.ProjectAutomation.Core;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [AutomaticTask("TargetWordCountID",
        "Target Word Count",
        "Counts number of words in target",
        GeneratedFileType = AutomaticTaskFileType.BilingualTarget)]
    [AutomaticTaskSupportedFileType(AutomaticTaskFileType.BilingualTarget)]
    [RequiresSettings(typeof(WordCountBatchTaskSettings), typeof(WordCountBatchTaskSettingsPage))]
    public class WordCountBatchTask : AbstractFileContentProcessingAutomaticTask
    {
        private readonly List<ISegmentWordCounter> counters = new List<ISegmentWordCounter>();
        private IWordCountBatchTaskSettings settings = null;

        public override bool OnFileComplete(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
        {
            var report = ReportGenerator.Generate(counters, settings);

            CreateReport(CreateReportName(projectFile.GetLanguageDirection()), "Count for each file", report, projectFile.GetLanguageDirection());

            counters.Clear();

            return false;
        }

        protected override void ConfigureConverter(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
        {
            Contract.Assume(settings != null);

            SegmentWordCounter counter = new SegmentWordCounter(projectFile.Name, settings, GetWordCounter(projectFile));
            multiFileConverter.AddBilingualProcessor(counter);

            counters.Add(counter);
        }

        protected override void OnInitializeTask()
        {
            settings = GetSetting<WordCountBatchTaskSettings>();
        }

        private string CreateReportName(LanguageDirection langDirection)
        {
            return $"Target Word Count {langDirection.SourceLanguage.IsoAbbreviation}_{langDirection.TargetLanguage.IsoAbbreviation}";
        }
    }
}