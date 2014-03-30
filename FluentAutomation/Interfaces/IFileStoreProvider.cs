using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public interface IFileStoreProvider
    {
        bool SaveScreenshot(FluentSettings settings, byte[] contents, string fileName);
    }
}
