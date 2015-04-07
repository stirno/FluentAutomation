using System;
using Xunit;
using System.IO;
using System.Configuration;

namespace FluentAutomation.Tests.Base
{
    public class ConfigReaderTests
    {
        [Fact]
        public void ConfigReader_ExternalFile_Result()
        {
            // Arrange
            const string ConfigFileContent = @"<?xml version='1.0' encoding='utf-8'?><configuration><configSections><section name='settings' type='System.Configuration.NameValueSectionHandler' /></configSections><settings><add key='DummyKey' value='DummyValue' /></settings></configuration>";
            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, ConfigFileContent);

            // Act
            string setting = ConfigReader.GetEnvironmentVariableOrAppSetting("DummyKey", configFilePath);

            // Assert
            Assert.Equal(setting, "DummyValue");
        }

        [Fact]
        public void ConfigReader_MissingExternalFile_NoResult()
        {
            // Arrange
            const string ConfigFilePath = @"C:\FakePath\config.xml";

            // Act
            string setting = ConfigReader.GetEnvironmentVariableOrAppSetting("DummyKey", ConfigFilePath);

            // Assert
            Assert.Null(setting);
        }

        [Fact]
        public void ConfigReader_InvalidExternalFile_Result()
        {
            // Arrange
            const string ConfigFileContent = @"<content>This is not a valid config file</content>";
            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, ConfigFileContent);

            // Act
            Assert.ThrowsDelegate action = () => ConfigReader.GetEnvironmentVariableOrAppSetting("DummyKey", configFilePath);

            // Assert
            Assert.Throws<ConfigurationErrorsException>(action);
        }
    }
}
