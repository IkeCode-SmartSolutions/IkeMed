using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;

namespace IkeCode.Core.Mvc
{
    public class AsDefinedBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
