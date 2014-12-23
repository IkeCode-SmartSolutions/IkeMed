using IkeMed.Metro.Web.Base;
using IkeMed.Model;
using IkeMed.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IkeMed.Metro.Web.ViewModels
{
    public class NotifyViewModel
    {
        public string type { get; private set; }
        public string theme { get; private set; }
        public string title { get; private set; }
        public string message { get; private set; }

        public string position { get; private set; }
        public int interval { get; private set; }
        public bool close { get; private set; }

        /// <summary>
        /// Model for Notify
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="type">alert or notification</param>
        /// <param name="theme">blue, dark, green and orange</param>
        /// <param name="position">topleft, bottomleft, bottomright, bottomcenter, topcenter, center</param>
        /// <param name="interval"></param>
        /// <param name="close"></param>
        public NotifyViewModel(string message = "",
            string title = "", 
            string type = "notification", 
            string theme = "green", 
            string position = "topcenter",
            int interval = 3000,
            bool close = true)
        {
            this.message = message;
            this.title = title;
            this.type = !string.IsNullOrWhiteSpace(type) ? type : "notification";
            this.theme = !string.IsNullOrWhiteSpace(theme) ? theme : "green";
            this.position = !string.IsNullOrWhiteSpace(position) ? position : "topcenter";
            this.interval = interval > 100 ? interval : 3000;
            this.close = close;
        }
    }
}