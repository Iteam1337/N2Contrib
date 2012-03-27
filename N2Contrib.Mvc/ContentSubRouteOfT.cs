using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Engine;
using System.Web.Routing;
using System.Web;
using N2;
using N2.Web.Mvc;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Web.Mvc;

namespace N2Contrib.Mvc
{
    /// <summary>
    /// A SubRoute to a Content Route
    /// <remarks>
    /// 
    /// </remarks>
    /// </summary>
    public class ContentSubRoute<T> : RouteBase
        where T : ContentItem
    {
        readonly IEngine engine;
        readonly IControllerMapper controllerMapper;
        readonly string url;
        private RouteValueDictionary parameters;
		readonly RouteUrlRegexFactory regexFactory = new RouteUrlRegexFactory();
		private Dictionary<string, IRouteConstraint> constraints;
		private RouteValueDictionary dataTokens;

        class Required
        {
        }

        /// <summary>
        /// Initializes a new Content Sub Route
        /// </summary>
        public ContentSubRoute(string name, IEngine engine, string url, object defaults = null, object constraints = null, object dataTokens = null, IRouteHandler routeHandler = null)
        {
            this.engine = engine;
            this.url = url;

			segmentRegex = regexFactory.CreateExpression(url);

            // create a map of known parameters to this route
			// those only in the url expression are required
            parameters = defaults as RouteValueDictionary 
				?? new RouteValueDictionary(defaults);
            foreach(var g in segmentRegex.Match(url).Groups.OfType<Group>().Skip(1))
            {
				var key = regexFactory.ToKey(g.Value);
                if (!parameters.ContainsKey(key))
                    parameters.Add(key, new Required());
            }

			this.constraints = (constraints as RouteValueDictionary ?? new RouteValueDictionary(constraints))
				.ToDictionary(kvp => kvp.Key, kvp => CreateConstraint(kvp.Value));

			this.dataTokens = dataTokens as RouteValueDictionary 
				?? new RouteValueDictionary(dataTokens);
			this.dataTokens[N2.Web.Mvc.ContentRoute.ContentEngineKey] = engine;

			this.RouteHandler = routeHandler ?? new MvcRouteHandler();

            this.controllerMapper = engine.Resolve<IControllerMapper>();
        }

		private IRouteConstraint CreateConstraint(object constraint)
		{
			if (constraint is IRouteConstraint)
				return constraint as IRouteConstraint;
			if (constraint is string)
				return new RegexConstraint(new Regex(constraint as string));
			else if (constraint is Func<string, bool>)
				return new DelegateConstraint(constraint as Func<string, bool>);

			throw new InvalidOperationException("Cannot use route constraint " + constraint);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var path = engine.UrlParser.ResolvePath(httpContext.Request.Url.PathAndQuery);
            
            // No content route was found check for stopitem
			if (path.CurrentPage != null)
				return null;

			if (path.StopItem is T)
            {
				var values = GetRouteValues(path.Argument);
				if (values == null)
					// not matching url expression
					return null;

				if (!MatchesAllConstraints(httpContext, values))
					return null;

				return CreateRouteData(values);
            }

            return null;
        }

		private RouteData CreateRouteData(RouteValueDictionary values)
		{
			var data = new RouteData(this, this.RouteHandler);
			foreach (var kvp in values)
				data.Values[kvp.Key] = kvp.Value;
			foreach (var kvp in dataTokens)
				data.DataTokens[kvp.Key] = kvp.Value;
			return data;
		}

		private bool MatchesAllConstraints(HttpContextBase httpContext, RouteValueDictionary values)
		{
			foreach (var kvp in values)
			{
				IRouteConstraint constraint;
				if (constraints.TryGetValue(kvp.Key, out constraint))
					if (!constraint.Match(httpContext, null, kvp.Key, values, RouteDirection.IncomingRequest))
						// not matching constraint
						return false;
			}
			return true;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null; //TODO
        }

        public Regex segmentRegex { get; set; }

        public RouteValueDictionary GetRouteValues(string url)
        {
            var match = segmentRegex.Match(url);
            if(!match.Success)
                return null;

            var values = new RouteValueDictionary();
            foreach (var key in parameters.Keys)
            {
                var g = match.Groups[key];
                if(g.Success)
                    values[key] = g.Value;
                else if(parameters[key] is Required)
                    return null;
                else
                    values[key] = parameters[key];
            }
            return values;
        }

		public IRouteHandler RouteHandler { get; set; }
	}
}
