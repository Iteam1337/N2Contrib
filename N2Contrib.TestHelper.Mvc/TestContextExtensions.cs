using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using N2;

namespace N2Contrib.TestHelper
{
    public static class TestContextExtensions
    {
        public static void InitializeController(this TestContext context, ContentController controller)
        {
            controller.ControllerContext = new ControllerContext(new RequestContext(context.HttpContext, context.RouteData), controller);
        }

        public static void SetCurrentItem(this TestContext context, ContentItem item)
        {
            context.Engine.RequestContext.CurrentPath = new N2.Web.PathData(item, null);
            context.RouteData.DataTokens["item"] = item;
            context.RouteData.ApplyCurrentItem(item, null);
        }

    }
}
