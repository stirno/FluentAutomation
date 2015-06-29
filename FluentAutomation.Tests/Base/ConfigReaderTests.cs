using System;
using System.Configuration;
using System.IO;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class ConfigReaderTests
    {
        [Fact]
        public void ConfigReader_ExternalFile_ValidResult()
        {
            // Arrange
            const string ConfigFileContent = @"<?xml version='1.0' encoding='utf-8'?><configuration><configSections><section name='settings' type='System.Configuration.NameValueSectionHandler' /></configSections><settings><add key='DummyKey' value='DummyValue' /></settings></configuration>";
            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, ConfigFileContent);

            // Act
            string setting = ConfigReader.GetSetting("DummyKey", configFilePath);

            // Assert
            Assert.Equal(setting, "DummyValue");
        }

        [Fact]
        public void ConfigReader_MissingExternalFile_NoResult()
        {
            // Arrange
            const string ConfigFilePath = @"C:\FakePath\config.xml";

            // Act
            string setting = ConfigReader.GetSetting("DummyKey", ConfigFilePath);

            // Assert
            Assert.Null(setting);
        }

        [Fact]
        public void ConfigReader_InvalidExternalFile_ValidResult()
        {
            // Arrange
            const string ConfigFileContent = @"<content>This is not a valid config file</content>";
            string configFilePath = Path.GetTempFileName();
            File.WriteAllText(configFilePath, ConfigFileContent);

            // Act
            Action action = () => ConfigReader.GetSetting("DummyKey", configFilePath);

            // Assert
            Assert.Throws<ConfigurationErrorsException>(() => action());
        }

        [Fact]
        public void ConfigReader_AppSetting_ValidResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_StringSetting";

            // Act
            string setting = ConfigReader.GetSetting(DummySettingKey);

            // Assert
            Assert.NotNull(setting);
            Assert.Equal(setting, "important");
        }

        [Fact]
        public void ConfigReader_MissingAppSetting_NoResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_MissingSetting";

            // Act
            string setting = ConfigReader.GetSetting(DummySettingKey);

            // Assert
            Assert.Null(setting);
        }

        [Fact]
        public void ConfigReader_AppSettingAsBoolean_ValidResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_BooleanSetting";

            // Act
            bool? setting = ConfigReader.GetSettingAsBoolean(DummySettingKey);

            // Assert
            Assert.True(setting.HasValue);
            Assert.Equal(setting.Value, false);
        }

        [Fact]
        public void ConfigReader_AppSettingAsInteger_ValidResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_IntegerSetting";
            
            // Act
            int? setting = ConfigReader.GetSettingAsInteger(DummySettingKey);

            // Assert
            Assert.True(setting.HasValue);
            Assert.Equal(setting.Value, 42);
        }

        [Fact]
        public void ConfigReader_InvalidAppSettingAsBoolean_NoResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_StringSetting";

            // Act
            bool? setting = ConfigReader.GetSettingAsBoolean(DummySettingKey);

            // Assert
            Assert.False(setting.HasValue);
            Assert.Null(setting);
        }

        [Fact]
        public void ConfigReader_InvalidAppSettingAsInteger_NoResult()
        {
            // Arrange
            const string DummySettingKey = "Dummy_StringSetting";

            // Act
            int? setting = ConfigReader.GetSettingAsInteger(DummySettingKey);

            // Assert
            Assert.False(setting.HasValue);
            Assert.Null(setting);
        }
    }
}
