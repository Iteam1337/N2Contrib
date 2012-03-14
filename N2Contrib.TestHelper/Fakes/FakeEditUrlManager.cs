using N2.Configuration;
using N2.Edit;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeEditUrlManager : EditUrlManager
	{
		public FakeEditUrlManager()
			: base(new EditSection())
		{
		}
	}
}