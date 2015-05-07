using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
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
            using (XmlReader reader = XmlReader.Create(xml.Trim().ToStream(), new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Auto }))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }

        public static T LoadConfigByXPath<T>(string xPath = null, string filePath = null) where T : class
        {
            filePath = ResolveFilePath(filePath ?? GetDataFilePathFromConfig());
            xPath = "/TestData" + (xPath ?? string.Empty);

            // Load XML from file
            var document = new XmlDocument();
            document.Load(filePath);

            // Select section by XPath 
            var section = document.SelectSingleNode(xPath);
            if (section != null)
            {
                string sectionXml = section.OuterXml;
                return sectionXml.ParseXml<T>();
            }
            return null;
        }

        private static string GetDataFilePathFromConfig()
        {
            return ConfigurationManager.AppSettings["WbTstr:DataFile"];
        }

        private static string ResolveFilePath(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");

            if (filePath.StartsWith("~"))
            {
                string executionPath = AssemblyDirectory();
                filePath = executionPath + filePath.Replace("~", string.Empty);
            }
            return filePath;
        }

        private static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}