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
        public bool SaveScreenshot(byte[] contents, string fileName)
        {
            try
            {
                if (fileName.Substring(0, fileName.Length - 4) != ".png")
                {
                    fileName += ".png";
                }

                File.WriteAllBytes(Path.Combine(FluentSettings.Current.ScreenshotPath, fileName), contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
