using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.TestHelper;
using Xunit;
using N2Contrib.Tests.TestHelper.Fakes;
using FluentAssertions;

namespace N2Contrib.Tests.TestHelper
{
	public class N2TestContextTests
	{
		N2TestContext testContext;

		public N2TestContextTests()
		{
			testContext = new N2TestContext();
		}

		[Fact]
		public void CreateItem_seeds_id()
		{
			FooPage page = testContext.CreateItem<FooPage>("Start");

			for (var i = 0; i < 10; i++)
			{
				var newPage = testContext.CreateItem<FooPage>("Foo");
				newPage.ID.Should().BeGreaterThan(page.ID);
				page = newPage;
			}
		}

		[Fact]
		public void CreateItem_setters_works_as_intended()
		{
			var page = testContext.CreateItem<FooPage>("Foo", false, (p) => p.Name = "Something", (p) => p.Title = "Hello");
			page.Name.Should().Be("Something");
			page.Title.Should().Be("Hello");
		}

		[Fact]
		public void CreateItem_startpage_works_as_intended()
		{
			var page = testContext.CreateItem<FooPage>("Foo", isStartPage: true);
			page["IsStartPage"].Should().Be(true);

			testContext.UrlParser.StartPage.Should().Be(page);
			testContext.Engine.Host.DefaultSite.StartPageID.Should().Be(page.ID);
		}
	}
}
