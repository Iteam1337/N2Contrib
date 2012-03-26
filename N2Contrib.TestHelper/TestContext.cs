using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.TestHelper.Fakes;
using N2.Engine;
using N2.Web;
using N2.Persistence;
using N2.Persistence.NH;
using N2;
using N2.Details;
using N2.Security;
using N2.Definitions;
using N2.Edit.Workflow;
using N2.Persistence.Proxying;
using System.Web.Routing;

namespace N2Contrib.TestHelper
{
    public class TestContext
    {
        public TestContext()
        {
            Url.DefaultExtension = "";

            Engine = new FakeEngine();
            HttpContext = Engine.Fakes.FakeHttpContext;
            SecurityManager = Engine.Fakes.SecurityManager;
            UrlParser = Engine.Fakes.UrlParser;
            RouteData = new RouteData();
        }

        public void AddComponentToEngine<T>(T implementation)
        {
            Engine.Container.AddComponentInstance(implementation.GetType().Name, typeof(T), implementation);
        }

        public FakeEngine Engine { get; set; }
        public FakeSecurityManager SecurityManager { get; set; }
        public FakeUrlParser UrlParser { get; set; }
        public FakeHttpContext HttpContext { get; set; }
        public RouteData RouteData {get;set;}

        public T CreateStructure<T>(string path)
            where T: ContentItem, new()
        {
            var segments = path.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
            {
                var startPage = CreateItem<T>("start");
                startPage["IsStartPage"] = true;
                Engine.Host.DefaultSite.StartPageID = startPage.ID;
                return startPage;
            }

            var parent = CreateStructure<T>(string.Join("/", segments.Take(segments.Length - 1).ToArray()));

            var item = CreateItem<T>(segments[segments.Length - 1]);
            item.AddTo(parent);
            return item;
        }

        private T CreateItem<T>(string name)
            where T : ContentItem, new()
        {
            var item = new T();
            item.Title = name;
            item.Name = name;
            ((N2.Engine.IInjectable<IUrlParser>)item).Set(Engine.UrlParser);
            Engine.Persister.Save(item);
            return item;
        }
    }
}
