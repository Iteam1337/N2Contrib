using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Engine;
using System.Web.Routing;
using System.Web;
using N2;
using N2.Web.Mvc;

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

        /// <summary>
        /// Initializes a new Content Sub Route
        /// </summary>
        public ContentSubRoute(string name, IEngine engine, string url, object defaults, object constraints)
        {
            this.engine = engine;
            this.url = url;
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
            throw new NotImplementedException();
        }
    }
}
