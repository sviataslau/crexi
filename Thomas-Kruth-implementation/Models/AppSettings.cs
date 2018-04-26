using System;
using System.Collections.Generic;

namespace ThomasKruthimplementation.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
        }

        public DefaultRequests DefaultCurrentRequests { get; set; }
        public DefaultRequests DefaultWeekRequests { get; set; }

        public List<CustomIPRule> CustomCurrentIPRules { get; set; }
        public List<CustomIPRule> CustomWeekIPRules { get; set; }
    }

    public class DefaultRequests : Attribute {
        public int AllowedTries { get; set; }
        public int Minutes { get; set; }
        public Dictionary<string, IPRequest> IPs { get; set; }

        public DefaultRequests(){
            IPs = new Dictionary<string, IPRequest>();
        }
    }

    public class IPRequest {
        public int Tries { get; set; }
        public DateTime LastRequest { get; set; }
    }

    public class CustomIPRule : Attribute
    {
        public string IP { get; set; }
        public int AllowedTries { get; set; }
        public int Minutes { get; set; }
        public int Tries { get; set; }
        public DateTime LastRequest { get; set; }

    }
}
