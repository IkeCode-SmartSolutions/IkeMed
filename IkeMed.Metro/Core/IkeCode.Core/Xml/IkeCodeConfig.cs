using IkeCode.Core.Cache;
using IkeCode.Core.Common;
using IkeCode.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IkeCode.Core.Xml
{
    public class IkeCodeConfig
    {
        #region Attributes

        private static FileWatcher Watcher = new FileWatcher();
        private string File { get; set; }
        private string Section { get; set; }
        private bool IsDictionary { get; set; }
        private bool IsAdmin { get; set; }

        public IkeCodeConfig Current { get; set; }

        #endregion

        #region Public Methods

        private IkeCodeConfig()
        {
            this.Current = this;
        }

        public IkeCodeConfig(string file, bool isDictionary = true)
            : this()
        {
            this.File = file;
            this.IsDictionary = isDictionary;
        }

        public IkeCodeConfig(string file, bool isDictionary = true, bool isAdmin = false)
            : this(file, isDictionary)
        {
            this.IsAdmin = isAdmin;
        }

        public IkeCodeConfig(string file, string section)
            : this()
        {
            this.File = file;
            this.Section = section;
        }

        public IkeCodeConfig(string file, string section, bool isDictionary)
            : this(file, section)
        {
            this.IsDictionary = isDictionary;
        }

        public static XDocument Load(string fileName, bool isDictionary, bool isAdmin, int cacheTime = 0, string cacheKey = "")
        {
            var cache = new IkeCodeCache();
            if (string.IsNullOrWhiteSpace(cacheKey))
                cacheKey = string.Format("IkeCodeConfig_Load_{0}_{1}", fileName, isAdmin);
            return (XDocument)cache.AutoCache<string, bool, bool, string, XDocument>(cacheKey, cacheTime,
                (res, res1, res2, res3) => LoadXmlNoCache(fileName, isDictionary, isAdmin, cacheKey), fileName, isDictionary, isAdmin, cacheKey).Value;
        }

        public static string GetConfigFolderPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"Config\";
        }

        public string GetText(string section, string key)
        {
            return this.GetValue(section, key);
        }

        public Dictionary<string, Dictionary<string, string>> GetDictionary()
        {
            var result = new Dictionary<string, Dictionary<string, string>>();

            var xml = Load(this.File, this.IsDictionary, this.IsAdmin);
            var doc = XDocument.Parse(xml.ToString());

            foreach (XElement element in doc.Elements().Descendants())
            {
                int keyInt = 0;
                var parentKeyName = element.Name.LocalName;
                var keyName = "";
                var values = new Dictionary<string, string>();

                foreach (var child in element.Descendants())
                {
                    keyName = child.Name.LocalName;
                    while (result.ContainsKey(child.Name.LocalName))
                        keyName = keyName + "_" + keyInt++;
                    values.Add(keyName, child.Value);
                }

                result.Add(parentKeyName, values);
            }

            return result;
        }

        public string GetJson()
        {
            var path = GetPath(this.File, this.IsDictionary, this.IsAdmin, true);
            var doc = new XmlDocument();
            doc.Load(path);

            return doc.ToJsonString();
        }

        public object GetAttribute(string name)
        {
            XDocument xml = Load(this.File, this.IsDictionary, this.IsAdmin, 500);
            XAttribute attributeValue = null;

            if (xml != null)
            {
                attributeValue = (from item in xml.Root.Descendants(this.Section)
                                  select item.Attribute(name)).FirstOrDefault();

                if (attributeValue == null)
                {
                    attributeValue = (from item in xml.Root.Descendants("default")
                                      select item.Attribute(name)).FirstOrDefault();
                }
            }

            return attributeValue != null ? attributeValue.Value : "[" + this.Section + "|default" + "/" + name + "]";
        }

        public string GetValue(string section, string key)
        {
            XDocument xml = Load(this.File, this.IsDictionary, this.IsAdmin, 500);

            var xmlElements = (from item in xml.Root.Descendants(section)
                               select item.Element(key)).FirstOrDefault();

            return xmlElements != null ? xmlElements.Value : "[" + section + "/" + key + "]";
        }

        public string GetString(string key)
        {
            return (string)this.GetValue(key);
        }

        public int GetInt(string key)
        {
            int result = 0;
            int.TryParse(this.GetString(key), out result);
            return result;
        }

        public decimal GetDecimal(string key)
        {
            decimal result = 0;
            decimal.TryParse(this.GetString(key), out result);
            return result;
        }

        public double GetDouble(string key)
        {
            double result = 0;
            double.TryParse(this.GetString(key), out result);
            return result;
        }

        public float GetFloat(string key)
        {
            float result = 0;
            float.TryParse(this.GetString(key), out result);
            return result;
        }

        public bool GetBool(string key)
        {
            bool result;
            bool.TryParse(this.GetString(key), out result);
            return result;
        }

        public DateTime GetDateTime(string key)
        {
            DateTime result = new DateTime();
            DateTime.TryParse(this.GetString(key), out result);
            return result;
        }

        public object GetObject(string key)
        {
            return this.GetValue(key);
        }

        #endregion

        #region Private Methods

        private static XDocument LoadXmlNoCache(string fileName, bool isDictionary = false, bool isAdmin = false, string cacheKey = "")
        {
            try
            {
                var path = GetPath(fileName, isDictionary, isAdmin, false);

                Watcher.Path = path;
                Watcher.FileName = fileName;
                Watcher.CacheKey = cacheKey;
                Watcher.Start();

                path += NormalizeFileName(fileName);

                IkeCodeLog.Default.Verbose("Loading configuration file: [" + path + "]");
                return XDocument.Load(path);
            }
            catch (FileNotFoundException ex)
            {
                IkeCodeLog.Default.Warning(string.Format("Configuration file not found on the configuration folders: [{0}]", ex.FileName));
                throw ex;
            }
            catch (Exception ex)
            {
                IkeCodeLog.Default.Exception(ex);
                throw ex;
            }
        }

        private static string GetPath(string fileName, bool isDictionary, bool isAdmin, bool includeFileName)
        {
            var pathSeparator = Path.DirectorySeparatorChar;
            var dicPath = (isDictionary ? isAdmin ? "DictionaryAdmin" : "Dictionary" : "");

            var path = GetConfigFolderPath();

            path += dicPath;
            path += !string.IsNullOrWhiteSpace(dicPath) ? pathSeparator.ToString() : "";
            path += includeFileName ? NormalizeFileName(fileName) : "";
            return path;
        }

        private static string NormalizeFileName(string fileName)
        {
            return fileName.EndsWith(".xml") ? fileName : fileName + ".xml";
        }

        private object GetValue(string key)
        {
            XDocument xml = Load(this.File, this.IsDictionary, this.IsAdmin, 500);
            XElement xmlElements = null;

            if (xml != null)
            {
                xmlElements = (from item in xml.Root.Descendants(this.Section)
                               select item.Element(key)).FirstOrDefault();

                if (xmlElements == null)
                {
                    xmlElements = (from item in xml.Root.Descendants("default")
                                   select item.Element(key)).FirstOrDefault();

                }
            }
            return xmlElements != null ? xmlElements.Value : "[" + this.Section + "|default" + "/" + key + "]";
        }

        #endregion
    }
}
