using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace N2Contrib.Mvc
{
	public class RegexConstraint : IRouteConstraint
	{
		private Regex regex;

		public RegexConstraint(Regex regex)
		{
			this.regex = regex;
		}

		public bool Match(System.Web.HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return regex.IsMatch((string)values[parameterName]);
		}
	}
}
