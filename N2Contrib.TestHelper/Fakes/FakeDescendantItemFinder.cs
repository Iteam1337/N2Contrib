using System.Collections.Generic;
using System.Linq;
using N2.Persistence;
using N2;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeDescendantItemFinder : DescendantItemFinder
	{
		public FakeDescendantItemFinder()
			: base(null, null)
		{
		}

		public override IEnumerable<T> Find<T>(ContentItem root)
		{
			return N2.Find.EnumerateChildren(root, true).OfType<T>();
		}
	}
}
