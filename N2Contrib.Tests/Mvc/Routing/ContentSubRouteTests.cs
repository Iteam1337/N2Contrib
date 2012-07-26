using System.Linq;
using FluentAssertions;
using N2;
using N2.Web;
using N2.Web.Mvc;
using N2Contrib.Mvc;
using N2Contrib.TestHelper.Fakes;
using N2Contrib.TestHelper.Mvc.Fakes;
using N2Contrib.Tests.TestHelper.Fakes;
using Xunit;
using N2Contrib.Mvc.Routing;

namespace N2Contrib.Tests.Mvc.Routing
{
    public class ContentSubRouteTests
    {
        private ContentSubRoute<ContentItem> route;
		private FakeControllerMapper controllerMapper;
        private FakeEngine engine;

        public ContentSubRouteTests()
        {
            engine = new FakeEngine();
			controllerMapper = new FakeControllerMapper();
			controllerMapper.controllerName = "Foo";
            engine.AddComponent<IControllerMapper>(controllerMapper);
            route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", new { hello = "world" }, null);
        }

        [Fact]
        public void It_should_give_route_values_for_path_matching_url_expression()
        {
            var values = route.GetRouteValues("universe");

			values.ContainsKey("hello").Should().BeTrue();
            values["hello"].ToString().Should().Be("universe");
        }

		[Fact]
		public void It_should_set_controller_from_controller_mapper()
		{
			var values = route.GetRouteValues("");
			values["controller"].Should().Be("Foo");
		}

		public void It_should_allow_custom_controller()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", new { controller= "Bar", hello = "world" }, null);
			var values = route.GetRouteValues("");
			values["controller"].Should().Be("Bar");
		}

        [Fact]
		public void It_should_give_route_defaults_for_empty_path_when_using_defaults()
        {
            var values = route.GetRouteValues("");

			values.ContainsKey("hello").Should().BeTrue();
			values["hello"].ToString().Should().Be("world");
        }

		[Fact]
		public void It_should_give_null_for_empty_path_when_no_defaults()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}/{hej}", null, null);

			var values = route.GetRouteValues("");

			values.Should().BeNull();
		}

		[Fact]
		public void It_should_give_null_for_empty_path_when_no_default_for_second_parameter()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}/{hej}", null, null);

			var values = route.GetRouteValues("world");

			values.Should().BeNull();
		}

        [Fact]
        public void It_should_give_default_for_second_segment()
        {
            route = new ContentSubRoute<ContentItem>("x", engine, "{hello}/{hej}", new { hej = "världen" }, null);
            
            var values = route.GetRouteValues("sverige");

            values.Count.Should().Be(3);
			values["controller"].Should().Be("Foo");
            values["hello"].Should().Be("sverige");
            values["hej"].Should().Be("världen");
        }

		[Fact]
		public void It_should_prefer_passed_path_before_using_default()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}/{hej}", new { hej = "världen" }, null);

			var values = route.GetRouteValues("sverige/wow");

			values.Count.Should().Be(3);
			values["hello"].Should().Be("sverige");
			values["hej"].Should().Be("wow");
		}

		[Fact]
		public void It_should_match_static_segments()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "hello/{hej}", null, null);

			var values = route.GetRouteValues("hello/världen");

			values.Count.Should().Be(2);
			values["hej"].Should().Be("världen");
		}

		[Fact]
		public void It_should_ignore_case()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "hello/{hej}", null, null);

			var values = route.GetRouteValues("HELLO/världen");

			values.Count.Should().Be(2);
		}

		[Fact]
		public void It_should_support_catch_all_token()
		{
			route = new ContentSubRoute<ContentItem>("x", engine, "{hej*}", null, null);

			var values = route.GetRouteValues("hela/världen");

			values.Count.Should().Be(2);
			values["hej"].Should().Be("hela/världen");
		}

		[Fact]
		public void It_should_call_constraints()
		{
			engine.Fakes.UrlParser.Paths["/hello"] = PathData.None(new FooPage(), "hello");

			bool wasCalled = false;
			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", null, new { hello = new DelegateConstraint((v) => wasCalled = true) });

			route.GetRouteData(new FakeHttpContext("/hello"));

			wasCalled.Should().Be(true);
		}

		[Fact]
		public void It_should_ignore_paths_not_matching_IRouteConstraint()
		{
			engine.Fakes.UrlParser.Paths["/world"] = PathData.None(new FooPage(), "world");

			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", null, new { hello = new DelegateConstraint((v) => false) });

			var data = route.GetRouteData(new FakeHttpContext("/world"));

			data.Should().BeNull();
		}

		[Fact]
		public void It_should_route_paths_matching_IRouteConstraint()
		{
			engine.Fakes.UrlParser.Paths["/world"] = PathData.None(new FooPage(), "world");

			route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", null, new { hello = new DelegateConstraint((v) => v == "world") });

			var data = route.GetRouteData(new FakeHttpContext("/world"));

			data.Values["hello"].Should().Be("world");
		}
    }
}
