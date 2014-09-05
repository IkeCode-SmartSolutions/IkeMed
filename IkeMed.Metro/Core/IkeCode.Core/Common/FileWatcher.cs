using IkeCode.Core.Cache;
using IkeCode.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeCode.Core.Common
{
    public class FileWatcher
    {
        #region Attributes

        private FileSystemWatcher Watcher = new FileSystemWatcher();
        public string Path { get; set; }
        public string FileName { get; set; }
        public string CacheKey { get; set; }

        #endregion

        #region Public Methods

        public FileWatcher() { }

        public FileWatcher(string path, string fileName, string cacheKey)
        {
            this.Path = path;
            this.FileName = fileName;
            this.CacheKey = cacheKey;
        }

        public void Start()
        {
            Watcher.Path = this.Path;
            Watcher.IncludeSubdirectories = true;
            Watcher.Filter = "*.xml";
            Watcher.EnableRaisingEvents = true;
            Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.CreationTime;

            Watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            Watcher.Changed += new FileSystemEventHandler(watcher_Changed);
        }

        public void Stop()
        {
            Watcher.EnableRaisingEvents = false;
        }

        #endregion

        #region Events

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            new IkeCodeCache().Remove(this.CacheKey);
            IkeCodeLog.Default.Verbose(string.Format("File modified: [{0}/{1}]", e.FullPath, e.Name));
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            new IkeCodeCache().Remove(this.CacheKey);
            IkeCodeLog.Default.Warning(string.Format("File renamed: [{0}/{1}]", e.FullPath, e.Name));
        }

        #endregion
    }
}
