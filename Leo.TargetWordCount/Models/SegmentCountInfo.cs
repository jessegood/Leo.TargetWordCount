namespace Leo.TargetWordCount.Models
{
    using Sdl.FileTypeSupport.Framework.NativeApi;
    using Sdl.ProjectAutomation.Core;
    using System.Diagnostics.Contracts;

    public class SegmentCountInfo
    {
        public SegmentCountInfo(ITranslationOrigin translationOrigin, CountData countData, bool isLocked)
        {
            TranslationOrigin = translationOrigin;
            CountData = countData;
            IsLocked = isLocked;
        }

        public CountData CountData { get; }

        public bool IsLocked { get; }

        public ITranslationOrigin TranslationOrigin { get; }
    }
}