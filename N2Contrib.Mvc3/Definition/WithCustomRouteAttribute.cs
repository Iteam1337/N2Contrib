using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2Contrib.Mvc3.Definition
{
    public class WithCustomRouteAttribute
    {
        public WithCustomRouteAttribute(string name, string url, object defaults = null, object constaints = null)
        {
            this.Name = name;
            this.Url = url;
            this.Defaults = defaults;
            this.Constraints = constaints;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public object Defaults { get; set; }
        public object Constraints { get; set; }
    }
}
