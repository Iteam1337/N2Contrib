using System;
using System.Web;
using System.Web.Routing;

namespace N2Contrib
{
	public class CacheService : IRouteHandler, IHttpHandler 
	{
		public bool IsReusable
		{
			// Return false in case your Managed Handler cannot be reused for another request.
			// Usually this would be false in case you have some state information preserved per request.
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			//write your handler implementation here.
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return this;
		}
	}
}
