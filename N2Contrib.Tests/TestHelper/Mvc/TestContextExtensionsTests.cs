using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.Tests.TestHelper.Fakes;
using N2Contrib.TestHelper;
using FluentAssertions;
using Xunit;

namespace N2Contrib.Tests.TestHelper.Mvc
{
    public class TestContextExtensionsTests
    {
        private TestContext testContext;
        private FooPageController controller; 

        public TestContextExtensionsTests()
        {
            testContext = new TestContext();
            controller = new FooPageController();
        }

        [Fact]
        public void It_sets_controller_context()
        {
            testContext.InitializeController(controller);
            controller.ControllerContext.Should().NotBeNull();
        }

        [Fact]
        public void It_sets_current_item_correctly()
        {
            testContext.InitializeController(controller);
            var page = new FooPage();
            testContext.SetCurrentItem(page);

            controller.CurrentItem.Should().Be(page);
        }

        [Fact]
        public void It_creates_page_structure()
        {
            testContext.InitializeController(controller);
            var world = testContext.CreateStructure<FooPage>("/hello/world");

            world.Url.Should().Be("/hello/world");
            world.Parent.Url.Should().Be("/hello");
            world.Parent.Parent.Url.Should().Be("/");
        }
    }
}
