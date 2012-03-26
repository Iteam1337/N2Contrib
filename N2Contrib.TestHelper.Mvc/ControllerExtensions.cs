//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Web.Mvc;
//using System.Web.Routing;
//using N2;
//using N2.Definitions;
//using N2.Details;
//using N2.Engine;
//using N2.Persistence;
//using N2.Persistence.NH;
//using N2.Security;
//using N2Contrib.TestHelper.Fakes;
//using N2.Web.Mvc;
//using N2.Web;

//namespace N2Contrib.TestHelper
//{
//    /// <summary>
//    /// Extensions for the Test Controller Builder 
//    /// </summary>
//    public static class ControllerExtensions
//    {
//        /// <summary>
//        /// Initializes a Content Controller setting controller context (unless it has been already set), engine and current item.
//        /// </summary>
//        public static TController InitializeContentController<TController, TContentItem>(this TController controller)
//            where TController : ContentController<TContentItem>
//            where TContentItem : ContentItem
//        {
//            AssignContext(controller);
//            Utility.SetProperty(controller, "Engine", CreateFakeEngine());
//            return controller;
//        }

//        /// <summary>
//        /// Sets 
//        /// </summary>
//        /// <typeparam name="TController"></typeparam>
//        /// <param name="controller"></param>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public static TController SetCurrentItem<TController, TContentItem>(this TController controller, TContentItem item)
//            where TController : ContentController<TContentItem>
//            where TContentItem : ContentItem
//        {
//            controller.ControllerContext.RouteData.ApplyCurrentItem(item, null);
//            controller.CurrentItem = item;
//            return controller;
//        }

//        /// <summary>
//        /// Initializes a Content Controller setting controller context (unless it has been already set), engine and current item.
//        /// </summary>
//        /// <param name="controller"></param>
//        private static ContentController<T> InitializeContentController<T>(this ContentController<T> controller) where T : ContentItem, new()
//        {
//            AssignContext(controller);
//            controller.Engine = CreateFakeEngine();
//            controller.CurrentItem = controller.CreateContentItem<T>("item");
//            return controller;
//        }

//        private static FakeEngine CreateFakeEngine()
//        {
//            var engine = new FakeEngine();
//            engine.AddComponent<IPersister>(new ContentPersister(new FakeRepository<ContentItem>(), new FakeRepository<ContentDetail>()));
//            engine.AddComponent<ISecurityManager>(new FakeSecurityManager());
//            engine.AddComponent<IDefinitionManager>(new DefinitionManager(new IDefinitionProvider[0], new ITemplateProvider[0], new ContentActivator(new N2.Edit.Workflow.StateChanger(), null, new N2.Persistence.Proxying.EmptyProxyFactory()), new N2.Edit.Workflow.StateChanger()));
//            engine.AddComponent<IUrlParser>(new FakeUrlParser());
//            return engine;
//        }

//        private static void AssignContext(Controller controller)
//        {
//            if (controller.ControllerContext == null)
//            {
//                var context = new FakeHttpContext();
//                controller.ControllerContext = new ControllerContext(new RequestContext(context, new RouteData()), controller);
//            }
//        }

//        static int id = 1;
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="builder"></param>
//        /// <returns></returns>
//        public static T CreateContentItem<T>(this Controller controller, string name, params ContentItem[] children) where T : ContentItem, new()
//        {
//            return CreateContentItem<T>(name, children);
//        }

//        public static T CreateContentItem<T>(string name, params ContentItem[] children) where T : ContentItem, new()
//        {
//            return (T)CreateContentItem(typeof(T), name, children);
//        }

//        public static ContentItem CreateContentItem(this Controller controller, Type contentType, string name, params ContentItem[] children)
//        {
//            return CreateContentItem(contentType, name, children);
//        }

//        public static ContentItem CreateContentItem(Type contentType, string name, params ContentItem[] children) 
//        {
//            var item = (ContentItem)Activator.CreateInstance(contentType);

//            item.ID = id++;
//            item.Name = name;
//            item.Title = name;
//            item.State = ContentState.Published;
//            item.Published = DateTime.Now;
//            ((IInjectable<IUrlParser>)item).Set(new FakeUrlParser());
//            foreach (var child in children)
//            {
//                child.AddTo(item);
//                child.AncestralTrail = item.GetTrail();
//            }
//            return item;
//        }

//        public static IEnumerable<T> CreateContentItems<T>(this Controller controller, params string[] names) where T : ContentItem, new()
//        {
//            return CreateContentItems<T>(names);
//        }
//        public static IEnumerable<T> CreateContentItems<T>(params string[] names) where T : ContentItem, new()
//        {
//            return names.Select(n => CreateContentItem<T>(n));
//        }

//        public static void AddComponent<T>(this IEngine engine, T implementation)
//        {
//            engine.Container.AddComponentInstance(implementation.GetType().Name, typeof(T), implementation);
//        }

//        public static Controller MakeUserAdmin<T>(this Controller controller)
//        {
//            controller.HttpContext.User = new GenericPrincipal(new GenericIdentity("Admin"), new string[0]);
//            return controller;
//        }

//    }
//}
