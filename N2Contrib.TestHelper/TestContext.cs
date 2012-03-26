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

namespace N2Contrib.TestHelper
{
    public class TestContext
    {
        public TestContext()
        {
            HttpContext = new FakeHttpContext();
            Engine = new FakeEngine();
            SecurityManager = new FakeSecurityManager();
            UrlParser = new FakeUrlParser();

            AddComponentToEngine<IUrlParser>(UrlParser);
            AddComponentToEngine<IPersister>(new ContentPersister(new FakeRepository<ContentItem>(), new FakeRepository<ContentDetail>()));
            AddComponentToEngine<ISecurityManager>(SecurityManager);
            AddComponentToEngine<IDefinitionManager>(new DefinitionManager(new IDefinitionProvider[0], new ITemplateProvider[0], new ContentActivator(new StateChanger(), null, new EmptyProxyFactory()), new StateChanger()));
        }

        public void AddComponentToEngine<T>(T implementation)
        {
            Engine.Container.AddComponentInstance(implementation.GetType().Name, typeof(T), implementation);
        }

        public ContentItem CurrentItem
        {
            get{ return null; }
            set{}
        }

        public FakeEngine Engine { get; set; }
        public FakeSecurityManager SecurityManager { get; set; }
        public FakeUrlParser UrlParser { get; set; }
        public FakeHttpContext HttpContext { get; set; }
    }
}
