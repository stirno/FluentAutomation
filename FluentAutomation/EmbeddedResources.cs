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
        public static void UnpackFromAssembly(string resourceFileName, Assembly assembly)
        {
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resourceFileName));
            if (!File.Exists(resourceFileName))
            {
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                var resourceBytes = new byte[(int)resourceStream.Length];

                resourceStream.Read(resourceBytes, 0, resourceBytes.Length);
                File.WriteAllBytes(resourceFileName, resourceBytes);
            }
        }
    }
}
