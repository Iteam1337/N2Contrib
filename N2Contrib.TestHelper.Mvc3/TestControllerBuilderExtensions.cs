using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using N2;
using N2.Definitions;
using N2.Details;
using N2.Engine;
using N2.Persistence;
using N2.Persistence.NH;
using N2.Security;
using N2.Tests.Fakes;
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
        public static ContentController<T> InitializeContentController<T>(this ContentController<T> controller, bool userIsAdmin = false, bool userIsEditor = false) where T : ContentItem, new()
        {
            if (controller.ControllerContext == null)
            {
                var context = new FakeHttpContext();
                controller.ControllerContext = new ControllerContext(new RequestContext(context, new RouteData()), controller);
            }
            controller.Engine = new FakeEngine();
            controller.MockN2Service<IPersister>(new ContentPersister(new FakeRepository<ContentItem>(), new FakeRepository<ContentDetail>()));
            controller.MockN2Service<ISecurityManager>(new FakeSecurityManager());
            controller.MockN2Service<IDefinitionManager>(new DefinitionManager(new IDefinitionProvider[0], new ITemplateProvider[0], new ContentActivator(new N2.Edit.Workflow.StateChanger(), null, new N2.Persistence.Proxying.EmptyProxyFactory()), new N2.Edit.Workflow.StateChanger()));
            controller.CurrentItem = controller.CreateContentItem<T>("item");
            return controller;
        }



        static int id = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static T CreateContentItem<T>(this Controller controller, string name, params ContentItem[] children) where T : ContentItem, new()
        {
            var item = new T();

			item.ID = id++;
			item.Name = name;
			item.Title = name;
			item.State = ContentState.Published;
			item.Published = DateTime.Now;
			foreach (var child in children)
			{
				child.AddTo(item);
				child.AncestralTrail = item.GetTrail();
			}
			return item;
        }

        public static IEnumerable<T> CreateContentItems<T>(this Controller controller, params string[] names) where T : ContentItem, new()
        {
            return names.Select(n => controller.CreateContentItem<T>(n));
        }

        public static Controller MockN2Service<T>(this Controller controller, T implementation)
        {
            var engine = N2.Utility.GetProperty(controller, "Engine") as IEngine;
            engine.Container.AddComponentInstance(implementation.GetType().Name, typeof(T), implementation);
            return controller;
        }

        public static Controller MakeUserAdmin<T>(this Controller controller)
        {
            controller.HttpContext.User = new GenericPrincipal(new GenericIdentity("Admin"), new string[0]);
            return controller;
        }

    }
}
