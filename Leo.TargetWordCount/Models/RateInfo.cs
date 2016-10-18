namespace Leo.TargetWordCount.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class RateInfo
    {
        [XmlArray("Invoice")]
        public List<InvoiceItem> Rate { get; set; } = new List<InvoiceItem>();
    }
}