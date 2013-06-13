using N2.Configuration;
using N2.Edit;
using N2.Web;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeEditUrlManager : EditUrlManager
	{
		public FakeEditUrlManager(IUrlParser urlParser)
			: base(urlParser, new EditSection())
		{
		}
	}
}