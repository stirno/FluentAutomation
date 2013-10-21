using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentAutomation
{
    public static class EmbeddedResources
    {
        public static string WorkingDirectory
        {
            get
            {
                var execAssembly = Assembly.GetExecutingAssembly();
                var dirBasePath = Path.GetDirectoryName(AppDomain.CurrentDomain.ShadowCopyFiles ? execAssembly.CodeBase : execAssembly.Location);

                return new Uri(dirBasePath).LocalPath;
            }
        }

        public static string FileExists(string fileName)
        {
            var localPath = Path.Combine(WorkingDirectory, fileName);
            if (File.Exists(fileName)) return localPath;

            var path = Environment.GetEnvironmentVariable("PATH");
            if (path == null) return string.Empty;

            var invalidChars = Path.GetInvalidPathChars();
            var pathValues = path.Split(Path.PathSeparator);
            foreach (var pathValue in pathValues)
            {
                if (pathValue.IndexOfAny(invalidChars) == -1)
                {
                    var filePath = Path.Combine(pathValue, fileName);
                    if (File.Exists(filePath))
                    {
                        return filePath;
                    }
                }
            }

            return string.Empty;
        }

        public static string UnpackFromAssembly(string resourceFileName, Assembly assembly)
        {
            return UnpackFromAssembly(resourceFileName, resourceFileName, assembly);
        }

        public static string UnpackFromAssembly(string resourceFileName, string outputFileName, Assembly assembly)
        {
            var filePath = FileExists(outputFileName);
            if (filePath != string.Empty) return filePath;

            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceFileName));
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            var resourceBytes = new byte[(int)resourceStream.Length];

            var tmpPath = Path.Combine(Path.GetTempPath(), outputFileName);
            resourceStream.Read(resourceBytes, 0, resourceBytes.Length);
            File.WriteAllBytes(tmpPath, resourceBytes);

            return tmpPath;
        }
    }
}