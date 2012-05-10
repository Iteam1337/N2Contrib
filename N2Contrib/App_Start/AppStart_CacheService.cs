using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.App_Start;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppStart_CacheService), "Start")]
namespace N2Contrib.App_Start
{
	public class AppStart_CacheService
	{
		public static void Start()
		{
			var route = new Route("N2Contrib/CacheService.ashx", new CacheService());
			RouteTable.Routes.Insert(0, route);
		}
	}
}
