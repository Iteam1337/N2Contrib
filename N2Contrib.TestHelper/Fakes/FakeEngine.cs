using System;
using System.Linq;
using System.Collections.Generic;
using N2.Definitions;
using N2.Edit;
using N2.Engine;
using N2.Integrity;
using N2.Persistence;
using N2.Security;
using N2.Web;
using N2;
using N2.Details;
using N2.Persistence.NH;
using N2.Edit.Workflow;
using N2.Persistence.Proxying;

namespace N2Contrib.TestHelper.Fakes
{
    public class FakeEngine : IEngine
    {
        public FakeServiceContainer container = new FakeServiceContainer();



		public FakeEngine()
		{
			Fakes = new FakesCollection();

			AddComponent<IUrlParser>(Fakes.UrlParser = new FakeUrlParser());
			Fakes.FakeHttpContext = new FakeHttpContext();
			AddComponent<IWebContext>(Fakes.WebContext = new FakeWebContextWrapper(Fakes.FakeHttpContext));
			AddComponent<IRepository<ContentItem>>(Fakes.ContentItemRepository = new FakeRepository<ContentItem>());
			AddComponent<IRepository<ContentDetail>>(Fakes.ContentDetailRepository = new FakeRepository<ContentDetail>());
			AddComponent<IPersister>(new ContentPersister(Fakes.ContentItemRepository, Fakes.ContentDetailRepository));
			AddComponent<ISecurityManager>(Fakes.SecurityManager = new FakeSecurityManager());
			AddComponent<IErrorNotifier>(Fakes.ErrorHandler = new FakeErrorHandler());
			var contentTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => typeof(ContentItem).IsAssignableFrom(t)).Where(t => !t.IsAbstract)).ToArray();
			AddComponent<ITypeFinder>(Fakes.TypeFinder = new FakeTypeFinder(contentTypes));
            AddComponent<IDefinitionManager>(Fakes.Definitions = new DefinitionManager(new IDefinitionProvider[0], new ITemplateProvider[0], new ContentActivator(new StateChanger(), null, new EmptyProxyFactory()), new StateChanger()));
            AddComponent<IHost>(Fakes.Host = new Host(Fakes.WebContext, new N2.Configuration.HostSection{ Web = new N2.Configuration.WebElement { Extension = "" }}));
		}

		public void AddComponent<TService>(TService instance)
		{
			Container.AddComponentInstance(instance.GetType().FullName, typeof(TService), instance);
		}

        #region IEngine Members

        public N2.Persistence.IPersister Persister
        {
            get { return container.Resolve<IPersister>(); }
        }

        public N2.Web.IUrlParser UrlParser
        {
            get { return container.Resolve<IUrlParser>(); }
        }

        public IDefinitionManager Definitions
        {
            get { return container.Resolve<IDefinitionManager>(); }
        }

        public N2.Integrity.IIntegrityManager IntegrityManager
        {
            get { return container.Resolve<IIntegrityManager>(); }
        }

        public N2.Security.ISecurityManager SecurityManager
        {
            get { return container.Resolve<ISecurityManager>(); }
        }

        public N2.Edit.IEditManager EditManager
        {
            get { return container.Resolve<IEditManager>(); }
        }

        public N2.Edit.IEditUrlManager ManagementPaths
        {
            get { return container.Resolve<IEditUrlManager>(); }
        }

        public N2.Web.IWebContext RequestContext
        {
            get { return container.Resolve<IWebContext>(); }
        }

        public N2.Web.IHost Host
        {
            get { return container.Resolve<IHost>(); }
        }

        public IServiceContainer Container
        {
            get { return container; }
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Attach(EventBroker application)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public object Resolve(string key)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Use Container.AddComponent")]
        public void AddComponent(string key, Type serviceType)
        {
            AddComponent(key, serviceType, serviceType);
        }

        [Obsolete("Use Container.AddComponent")]
        public void AddComponent(string key, Type serviceType, Type classType)
        {
            AddComponentInstance(key, serviceType, Activator.CreateInstance(classType));
        }

        [Obsolete("Use Container.AddComponentInstance")]
        public void AddComponentInstance(string key, Type serviceType, object instance)
        {
            container.AddComponentInstance(key, serviceType, instance);
        }

        [Obsolete("Use Container.AddComponentLifeStyle")]
        public void AddComponentLifeStyle(string key, Type serviceType, ComponentLifeStyle lifeStyle)
        {
            container.AddComponent(key, serviceType, serviceType);
        }

        [Obsolete("Not supportable by all service containers. Use the specific IServiceContainer implementation", true)]
        public void AddFacility(string key, object facility)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Use Container.Release")]
        public void Release(object instance)
        {
            throw new NotImplementedException();
        }

        public ContentHelperBase Content
        {
            get { return new ContentHelperBase(this, () => RequestContext.CurrentPath); }
        }

        #endregion

		public FakesCollection Fakes { get; set; }

		public class FakesCollection
		{
			public FakeUrlParser UrlParser { get; set; }

			public FakeWebContextWrapper WebContext { get; set; }

			public FakeRepository<ContentItem> ContentItemRepository { get; set; }

			public FakeRepository<ContentDetail> ContentDetailRepository { get; set; }

			public FakeSecurityManager SecurityManager { get; set; }

			public FakeErrorHandler ErrorHandler { get; set; }

			public FakeHttpContext FakeHttpContext { get; set; }

			public FakeTypeFinder TypeFinder { get; set; }

            public DefinitionManager Definitions { get; set; }

            public Host Host { get; set; }
        }

        public class FakeServiceContainer : IServiceContainer
        {
            Dictionary<Type, object> services = new Dictionary<Type, object>();

            #region IServiceContainer Members

            public void AddComponent(string key, Type serviceType, Type classType)
            {
                services[serviceType] = Activator.CreateInstance(classType);
            }

            public void AddComponentInstance(string key, Type serviceType, object instance)
            {
                services[serviceType] = instance;
            }

            public void AddComponentLifeStyle(string key, Type serviceType, ComponentLifeStyle lifeStyle)
            {
                throw new NotImplementedException();
            }

            public void AddComponentWithParameters(string key, Type serviceType, Type classType, IDictionary<string, string> properties)
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>()
            {
                if (services.ContainsKey(typeof(T)) == false)
                    throw new InvalidOperationException("No component for service " + typeof(T).Name + " registered");

                return (T)services[typeof(T)];
            }

            public T Resolve<T>(string key)
            {
                return (T)services[typeof(T)];
            }

            public object Resolve(Type type)
            {
                return services[type];
            }

            public void Release(object instance)
            {
            }

            public Array ResolveAll(Type serviceType)
            {
                if (!this.services.ContainsKey(serviceType))
                    return new object[0];

                return new object[] { Resolve(serviceType) };
            }

            public IEnumerable<ServiceInfo> Diagnose()
            {
                yield break;
            }

            public T[] ResolveAll<T>()
            {
                if (!this.services.ContainsKey(typeof(T)))
                    return new T[0];

                return new T[] { Resolve<T>() };
            }

            public N2.Engine.Configuration.IServiceContainerConfigurer ServiceContainerConfigurer
            {
                get { throw new NotImplementedException(); }
            }

            public void StartComponents()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

    }
}