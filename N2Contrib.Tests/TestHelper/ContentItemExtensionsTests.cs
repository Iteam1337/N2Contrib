using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using N2Contrib.TestHelper;
using N2Contrib.Tests.TestHelper.Fakes;
using FluentAssertions;

namespace N2Contrib.Tests.TestHelper
{
    public class ContentItemExtensionsTests
    {
        [Fact]
        public void AddChildren_works_as_expected()
        {
            FooPage parent, child1, child2, grandChild;

            parent = new FooPage()
                .AddChildren(
                    child1 = new FooPage(),
                    child2 = new FooPage()
                        .AddChildren(
                            grandChild = new FooPage()
                        )
                );

            parent.Should().NotBeNull();
            parent.Children.Count.Should().Be(2);
            parent.Children[1].Children.First().Should().Be(grandChild);
        }
    }
}
