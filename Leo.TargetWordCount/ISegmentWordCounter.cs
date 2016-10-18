namespace Leo.TargetWordCount
{
    using Models;
    using Sdl.FileTypeSupport.Framework.BilingualApi;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISegmentWordCounterContract))]
    public interface ISegmentWordCounter
    {
        FileCountInfo FileCountInfo { get; }

        string FileName { get; }

        void FileComplete();

        void Initialize(IDocumentProperties documentInfo);

        void ProcessParagraphUnit(IParagraphUnit paragraphUnit);
    }
}