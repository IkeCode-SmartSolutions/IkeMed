using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;

namespace IkeCode.Core.Log
{
    public class IkeCodeLog
    {
        #region Attributes

        public string FileNamePrefix { get; private set; }
        public static IkeCodeLog Default { get { return new IkeCodeLog(); } }
        private XDocument xml { get; set; }

        #endregion

        public IkeCodeLog()
        {
            xml = XDocument.Load(HostingEnvironment.ApplicationPhysicalPath + "\\Config\\IkeCodeLog.xml");
        }

        public IkeCodeLog(string fileNamePrefix)
            : this()
        {
            this.FileNamePrefix = fileNamePrefix;
        }

        #region Private Methods

        private string GetConfig(string key)
        {
            XElement element = null;

            if (xml != null)
            {
                element = (from item in xml.Root.Elements("default")
                           select item.Element(key)).FirstOrDefault();

            }
            return element != null ? element.Value : "[default" + "/" + key + "]";
        }

        private void Write(string message, string fileName)
        {
            var name = string.Format("{0}{1}_{2}.txt", this.FileNamePrefix, DateTime.Today.ToString("yyyyMMdd"), fileName);

            var path = this.GetConfig("logPath");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);
            var logMessage = string.Format("{0}: {1}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), message);

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    writer.WriteLine(logMessage);
                }
            }
        }

        private bool CheckIfEnabled(string type)
        {
            var isEnabled = false;

            if (xml != null)
            {
                var element = (from item in xml.Root.Elements(type)
                               select item.Element("enabled")).FirstOrDefault();

                if (element != null)
                    isEnabled = element.Value.ToBoolean();
            }

            return isEnabled;
        }

        #endregion

        #region Public Methods

        public void Verbose(string message)
        {
            if (CheckIfEnabled("verbose"))
            {
                this.Write(message, "Verbose");
            }
        }

        public void Exception(Exception exception)
        {
            if (CheckIfEnabled("exception"))
            {
                var message = string.Format("[{0}] -> StackTrace [{1}]", exception.Message, exception.StackTrace);
                this.Write(message, "Exception");
            }
        }

        public void Warning(string message)
        {
            if (CheckIfEnabled("warning"))
            {
                this.Write(message, "Warning");
            }
        }

        #endregion
    }
}
