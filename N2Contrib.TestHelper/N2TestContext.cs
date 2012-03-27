using System;
using System.Linq;
using N2Contrib.TestHelper.Fakes;
using N2.Web;
using N2;
using System.Web.Routing;
using System.Collections.Generic;
using N2.Web.Mvc;

namespace N2Contrib.TestHelper
{
    /// <summary>
    /// A context to use for N2 while doing unittesting
    /// </summary>
    public class N2TestContext
    {
        /// <summary>
        /// Initializes a new N2TestContext
        /// </summary>
        public N2TestContext()
        {
            Url.DefaultExtension = "";

            Engine = new FakeEngine();
            HttpContext = Engine.Fakes.FakeHttpContext;
            SecurityManager = Engine.Fakes.SecurityManager;
            UrlParser = Engine.Fakes.UrlParser;
            RouteData = new RouteData();
            N2.Context.Replace(Engine);
        }

        /// <summary>
        /// Adds an component to the Fake Engine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        public void AddComponentToEngine<T>(T implementation)
        {
            Engine.Container.AddComponentInstance(implementation.GetType().Name, typeof(T), implementation);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T CreateStructure<T>(string path)
            where T : ContentItem, new()
        {
            var segments = path.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
            {
                var startPage = CreateItem<T>("start");
                startPage["IsStartPage"] = true;
                UrlParser.StartPage = startPage;
                Engine.Host.DefaultSite.StartPageID = startPage.ID;
                return startPage;
            }

            var parent = CreateStructure<T>(string.Join("/", segments.Take(segments.Length - 1).ToArray()));

            var item = CreateItem<T>(segments[segments.Length - 1]);
            item.AddTo(parent);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T CreateItem<T>(string name) where T : ContentItem, new()
        {
            var item = new T();
            item.Title = name;
            item.Name = name;
            ((N2.Engine.IInjectable<IUrlParser>)item).Set(Engine.UrlParser);
            Engine.Persister.Save(item);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        public IEnumerable<T> CreateItems<T>(params string[] names) where T : ContentItem, new()
        {
            foreach (var name in names)
            {
                yield return CreateItem<T>(name);
            }
        }

        /// <summary>
        /// Gets or sets the Fake Engine
        /// </summary>
        public FakeEngine Engine { get; set; }

        /// <summary>
        /// Gets or sets the Security Manager
        /// </summary>
        public FakeSecurityManager SecurityManager { get; set; }

        /// <summary>
        /// Gets or sets UrlParser
        /// </summary>
        public FakeUrlParser UrlParser { get; set; }

        /// <summary>
        /// Gets or sets the HttpContext
        /// </summary>
        public FakeHttpContext HttpContext { get; set; }

        /// <summary>
        /// Gets or sets the RouteData
        /// </summary>
        public RouteData RouteData { get; set; }
    }
}
