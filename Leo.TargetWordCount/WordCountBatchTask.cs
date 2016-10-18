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

        public override void TaskComplete()
        {
            var report = ReportGenerator.Generate(counters, settings);

            CreateReport("Target Word Count", "Count for each file", report);
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
    }
}