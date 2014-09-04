using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IkeMed.Metro.Web.Base
{
    public class GeneralConfig
    {
        public GeneralConfig()
        {

        }

        private static GeneralConfig _default;
        public static GeneralConfig Default
        {
            get
            {
                _default = _default ?? new GeneralConfig();
                return _default;
            }
        }

        //public string PhisicalPath { get { return Server.MapPath("~/Uploads/ProfileImages"); } }
    }
}