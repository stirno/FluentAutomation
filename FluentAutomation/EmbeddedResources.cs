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

        public static void UnpackFromAssembly(string resourceFileName, Assembly assembly)
        {
            UnpackFromAssembly(resourceFileName, resourceFileName, assembly);
        }

        public static void UnpackFromAssembly(string resourceFileName, string outputFileName, Assembly assembly)
        {
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceFileName));
            var outputName = Path.Combine(WorkingDirectory, outputFileName);

            if (!File.Exists(outputName))
            {
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                var resourceBytes = new byte[(int)resourceStream.Length];

                resourceStream.Read(resourceBytes, 0, resourceBytes.Length);
                File.WriteAllBytes(outputName, resourceBytes);
            }
        }
    }
}
