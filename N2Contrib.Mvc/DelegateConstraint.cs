using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace N2Contrib.Mvc
{
	public class DelegateConstraint : IRouteConstraint
	{
		private Func<HttpContextBase,Route,string,RouteValueDictionary,RouteDirection,bool> matcher;

		public DelegateConstraint(Func<HttpContextBase, Route, string, RouteValueDictionary, RouteDirection, bool> matcher)
		{
			this.matcher = matcher;
		}

		public DelegateConstraint(Func<string, bool> func)
		{
			this.matcher = (hc, r, pn, v, rd) => func((string)v[pn]);
		}

		public bool Match(System.Web.HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return matcher(httpContext, route, parameterName, values, routeDirection);
		}
	}
}
