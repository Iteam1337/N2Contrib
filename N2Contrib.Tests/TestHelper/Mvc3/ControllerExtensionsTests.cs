using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2Contrib.TestHelper;
using FluentAssertions;
using Xunit;
using N2Contrib.Tests.TestHelper.Fakes;

namespace N2Contrib.Tests.TestHelper.Mvc3
{
    public class ControllerExtensionsTests
    {
        FooPageController controller;

        public ControllerExtensionsTests()
        {
            controller = new FooPageController();
        }

        [Fact]
        public void InitializeContentController_sets_controller_contexts()
        {
            controller.InitializeContentController()
                .ControllerContext.Should().NotBeNull();
        }

        [Fact]
        public void SetCurrentItem_works_as_stated()
        {
            FooPage page = null;
            controller.SetCurrentItem(() => page = new FooPage());
            controller.CurrentItem.Should().Be(page);
        }
    }
}
