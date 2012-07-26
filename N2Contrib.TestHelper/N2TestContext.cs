using System;
using System.Linq;
using N2Contrib.TestHelper.Fakes;
using N2.Web;
using N2;
using System.Web.Routing;
using System.Collections.Generic;

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
        /// Helper method that creates an item and stores it in the faked 
		/// underlying storage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
		/// <param name="startpage"></param>
		/// <param name="setters"></param>
        /// <returns>the newly created item</returns>
        public T CreateItem<T>(string name, bool isStartPage = false, params Action<T>[] setters) where T : ContentItem, new()
        {
			// Create the new Content Item
            var item = new T();

			// Set the mandatory properties
            item.Title = name;
            item.Name = name;

			// Set the internal start page flag
			if (isStartPage)
				item["IsStartPage"] = true;
			
			// Any provided custom setters?
			foreach (var setter in setters)
				setter(item);

			// Set a fake url parser
            ((N2.Engine.IInjectable<IUrlParser>)item).Set(Engine.UrlParser);

			// Persist it in the fake persister
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
