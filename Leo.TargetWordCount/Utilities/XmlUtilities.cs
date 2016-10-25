namespace Leo.TargetWordCount.Utilities
{
    using Models;
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Xml.Serialization;

    public static class XmlUtilities
    {
        public static SerializableSettings Deserialize(string openPath)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(openPath));
            Contract.Ensures(Contract.Result<SerializableSettings>() != null);

            XmlSerializer deserializer = new XmlSerializer(typeof(SerializableSettings));

            using (TextReader reader = new StreamReader(openPath))
            {
                object obj = deserializer.Deserialize(reader);
                return (SerializableSettings)obj;
            }
        }

        public static void Serialize(SerializableSettings settings, string savePath)
        {
            Contract.Requires<ArgumentNullException>(settings != null);
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(savePath));

            XmlSerializer serializer = new XmlSerializer(typeof(SerializableSettings));

            using (TextWriter writer = new StreamWriter(savePath))
            {
                serializer.Serialize(writer, settings);
            }
        }
    }
}