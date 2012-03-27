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
    /// <summary>
    /// Extensions for the Test Context
    /// </summary>
    public static class TestContextExtensions
    {
        /// <summary>
        /// Initializes a ContentController by setting a mocked Controller Context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        public static void InitializeController(this N2TestContext context, ContentController controller)
        {
            controller.Engine = context.Engine;
            controller.ControllerContext = new ControllerContext(new RequestContext(context.HttpContext, context.RouteData), controller);
        }

        /// <summary>
        /// Sets the Current Item
        /// </summary>
        /// <param name="context"></param>
        /// <param name="item"></param>
        public static void SetCurrentItem(this N2TestContext context, ContentItem item)
        {
            context.Engine.RequestContext.CurrentPath = new N2.Web.PathData(item, null);
            context.RouteData.DataTokens["item"] = item;
            context.RouteData.ApplyCurrentItem(item, null);
        }

    }
}
