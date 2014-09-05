using IkeCode.Core.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IkeMed.Metro.Web.Base
{
    public class GeneralConfig : IkeCodeConfig
    {
        public GeneralConfig(string section)
            : base("General.xml", section)
        {

        }

        private static GeneralConfig _default;
        public static GeneralConfig Default
        {
            get
            {
                _default = _default ?? new GeneralConfig("default");
                return _default;
            }
        }

        public string UploadPath { get { return this.GetString("uploadPath"); } }
    }
}