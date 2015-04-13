using System;
using System.IO;

using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class DataReaderTests
    {
        public class SimpleConfig
        {
            public string Name { get; set; }
        }

        [Fact]
        public void DataReader_ExternalDataFile_ValidResult()
        {
            // Arrange
            string configFileContent = @"
                <?xml version='1.0' ?>
                <TestData>
	                <SimpleConfig>
		                <Name>Onno</Name>
	                </SimpleConfig>
                </TestData>
            ".Trim();
            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, configFileContent);

            // Act
            SimpleConfig config = DataReader.LoadConfigByXPath<SimpleConfig>("/SimpleConfig", configFilePath);

            // Assert
            Assert.NotNull(config);
            Assert.Equal(config.Name, "Onno");
        }

        [Fact]
        public void DataReader_LocalDataFile_ValidResult()
        {
            // Arrange
            const string ConfigFilePath = @"~\TestData\TestData.xml";

            // Act
            SimpleConfig config = DataReader.LoadConfigByXPath<SimpleConfig>("/SimpleConfig", ConfigFilePath);

            // Assert
            Assert.NotNull(config);
            Assert.Equal(config.Name, "Onno");
        }
    }
}
