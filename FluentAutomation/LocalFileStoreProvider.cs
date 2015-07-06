using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class LocalFileStoreProvider : IFileStoreProvider
    {
        public bool SaveScreenshot(FluentSettings settings, byte[] contents, string fileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(settings.ScreenshotPrefix))
                    fileName = settings.ScreenshotPrefix + fileName;

	            if (Path.GetExtension(fileName) != ".png")
		            fileName += ".png";

                File.WriteAllBytes(Path.Combine(settings.ScreenshotPath, fileName), contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
