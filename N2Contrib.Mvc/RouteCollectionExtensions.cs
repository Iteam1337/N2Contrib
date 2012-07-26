using System;
using System.Linq;
using System.Web.Routing;
using N2;
using N2.Engine;
using N2.Web.Mvc;
using N2Contrib.Mvc.Routing;

namespace N2Contrib
{
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps a custom sub route to a page type
        /// </summary>
        /// <param name="routes"></param>
        public static void MapContentSubRoute<T>(this RouteCollection routes, string name, IEngine engine, string url, object defaults = null, object constraints = null)
            where T : ContentItem
        {
            var contentRoute = routes.OfType<ContentRoute>().FirstOrDefault();
            if (contentRoute == null)
                throw new ApplicationException("The Sub routes should be mapped after the content route");

            var contentRouteIndex = routes.IndexOf(contentRoute);
            var route = new ContentSubRoute<T>(name, engine, url, defaults, constraints);
            routes.Insert(contentRouteIndex, route);
        }
    }
}
