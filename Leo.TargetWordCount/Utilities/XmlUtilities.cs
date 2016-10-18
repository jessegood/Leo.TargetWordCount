namespace Leo.TargetWordCount.Utilities
{
    using Models;
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Xml.Serialization;

    public static class XmlUtilities
    {
        public static RateInfo Deserialize(string openPath)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(openPath));
            Contract.Ensures(Contract.Result<RateInfo>() != null);

            XmlSerializer deserializer = new XmlSerializer(typeof(RateInfo));

            using (TextReader reader = new StreamReader(openPath))
            {
                object obj = deserializer.Deserialize(reader);
                return (RateInfo)obj;
            }
        }

        public static void Serialize(RateInfo info, string savePath)
        {
            Contract.Requires<ArgumentNullException>(info != null);
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(savePath));
            Contract.Requires<ArgumentException>(info.Rate.Count > 0);

            XmlSerializer serializer = new XmlSerializer(typeof(RateInfo));

            using (TextWriter writer = new StreamWriter(savePath))
            {
                serializer.Serialize(writer, info);
            }
        }
    }
}