using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeCode.Core.Model
{
    public class CacheModel
    {
        public TimeSpan Expiration { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
