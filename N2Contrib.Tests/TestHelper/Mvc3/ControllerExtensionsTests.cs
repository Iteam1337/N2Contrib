using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.TestHelper;
using FluentAssertions;
using Xunit;
using N2Contrib.Tests.TestHelper.Fakes;
using N2;
using N2.Web.Mvc;

namespace N2Contrib.Tests.TestHelper.Mvc3
{
    public class ControllerExtensionsTests
    {
        [Fact]
        public void InitializeContentController_sets_controller_contexts()
        {
            var controller = new FooPageController()
                .ControllerContext.Should().NotBeNull();
        }

        [Fact]
        public void SetCurrentItem_works_as_stated()
        {
            FooPage page = new FooPage();
            var controller = new FooPageController();

            controller.CurrentItem.Should().Be(page);
        }

        [Fact]
        public void SetCurrentItem_works_for_un_typed_controllers()
        {
            var page = new FooPage();
            var controller = new UnTypedContentController();
            controller.CurrentItem.Should().Be(page);
        }
    }
}
