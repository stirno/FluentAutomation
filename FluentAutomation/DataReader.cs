using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FluentAutomation
{
    public static class DataReader
    {
        public static Stream ToStream(this string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXml<T>(this string xml) where T : class
        {
            using (XmlReader reader = XmlReader.Create(xml.Trim().ToStream(), new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document }))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }

        public static T LoadConfigByXPath<T>(string xPath) where T : class
        {
            xPath = "testData/" + xPath;
            var doc = new XmlDocument();
            var testDataFile = GetExternalDataFilePath();
            var testDataPath = Path.Combine(Directory.GetCurrentDirectory(), testDataFile);
            doc.LoadXml(testDataPath);
            var xmlNode = doc.SelectSingleNode(xPath).InnerXml;
            var config = xmlNode.ParseXml<T>();
            return config;
        }

        private static string GetExternalDataFilePath()
        {
            return ConfigurationManager.AppSettings["WbTstr:DataFile"];
        }
    }
}