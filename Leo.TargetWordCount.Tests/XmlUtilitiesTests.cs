namespace Leo.TargetWordCount.Tests
{
    using Models;
    using System;
    using Utilities;
    using Xunit;

    internal class XmlUtilitiesTests
    {
        [Fact]
        public void SerializeTest()
        {
            // Arrange
            RateInfo info = new RateInfo();

            for (int i = 0; i < Enum.GetValues(typeof(RateType)).Length - 1; ++i)
            {
                info.Rate.Add(new InvoiceItem((RateType)i, "10"));
            }

            XmlUtilities.Serialize(info, @"C:\Users\jesse_good\Desktop\");
        }
    }
}