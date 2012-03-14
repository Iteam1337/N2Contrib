using System.Web;
using N2.Web.Mvc;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeHttpServerUtility : HttpServerUtilityBase
	{
		public override void Execute(IHttpHandler handler, System.IO.TextWriter writer, bool preserveForm)
		{
			writer.Write(GetType().Name);
		}
	}
}
