using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcContrib.TestHelper;
using N2.Web.Mvc;

namespace N2Contrib.TestHelper
{
    /// <summary>
    /// Extensions for the Test Controller Builder 
    /// </summary>
    public static class TestControllerBuilderExtensions
    {
        /// <summary>
        /// Initializes a Content Controller
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="controller"></param>
        public static void InitializeContentController(this TestControllerBuilder builder, ContentController controller)
        {
            builder.InitializeController(controller); 
        }
    }
}
