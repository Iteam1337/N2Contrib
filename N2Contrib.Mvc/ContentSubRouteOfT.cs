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

namespace N2Contrib
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
        readonly static Regex regexFactory = new Regex("({[^}]*?}/*)", RegexOptions.Compiled);
        private RouteValueDictionary parameters;

        class Required
        {
        }

        /// <summary>
        /// Initializes a new Content Sub Route
        /// </summary>
        public ContentSubRoute(string name, IEngine engine, string url, object defaults, object constraints)
        {
            this.engine = engine;
            this.url = url;
            var urlPattern = regexFactory.Replace(url, (m) => "(?<" + m.Groups[1].Value.Trim('/', '{', '}') + ">[^/]+)?/*");
            Debug.WriteLine("pattern " + urlPattern);
            segmentRegex = new Regex(urlPattern, RegexOptions.Compiled);

            // create a map of known parameters of which those in only in url are required
            parameters = new RouteValueDictionary(defaults);
            foreach(var g in segmentRegex.Match(url).Groups.OfType<Group>().Skip(1))
            {
                var key = g.Value.Trim('/', '{', '}');
                if (!parameters.ContainsKey(key))
                    parameters.Add(key, new Required());
            }

            this.controllerMapper = engine.Resolve<IControllerMapper>();
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
            if (path.CurrentPage == null)
            {
                var page = path.StopItem;
                if (page is T)
                {
                    var matches = segmentRegex.Matches(path.Argument);
                    foreach (Match m in matches)
                    {

                    }
                }
            }

            return null;
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

        public RouteValueDictionary ParseValuesFromSubPath(string url)
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
    }
}
