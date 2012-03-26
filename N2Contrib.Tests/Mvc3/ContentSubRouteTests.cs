using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using N2;
using N2Contrib.TestHelper.Fakes;
using N2.Web.Mvc;
using FluentAssertions;

namespace N2Contrib.Tests.Mvc3
{
    public class ContentSubRouteTests
    {
        private ContentSubRoute<ContentItem> route;
        private FakeEngine engine;

        public ContentSubRouteTests()
        {
            engine = new FakeEngine();
            engine.AddComponent<IControllerMapper>(new FakeControllerMapper());
            route = new ContentSubRoute<ContentItem>("x", engine, "{hello}", new { hello = "world" }, null);
        }

        [Fact]
        public void Should_parse_simple_path()
        {
            var values = route.ParseValuesFromSubPath("universe");

            values.Keys.Single().Should().Be("hello");
            values.Values.Single().Should().Be("universe");
        }

        [Fact]
        public void Should_not_parse_empty_url()
        {
            var values = route.ParseValuesFromSubPath("");

            values.Should().BeNull();
        }

        [Fact]
        public void Should_support_defaults()
        {
            route = new ContentSubRoute<ContentItem>("x", engine, "{hello}/{hej}", new { hej = "världen" }, null);
            
            var values = route.ParseValuesFromSubPath("sverige");

            values.Count.Should().Be(2);
            values["hello"].Should().Be("sverige");
            values["hej"].Should().Be("världen");
        }
    }
}
