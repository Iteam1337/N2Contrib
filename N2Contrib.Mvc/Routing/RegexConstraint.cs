using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace N2Contrib.Mvc.Routing
{
	public class RegexConstraint : IRouteConstraint
	{
		private Regex regex;

		public RegexConstraint(Regex regex)
		{
			this.regex = regex;
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return regex.IsMatch((string)values[parameterName]);
		}
	}
}
