using System.IO;
using System.Web;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeHttpResponse : HttpResponseBase
	{
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return virtualPath;
		}

		public TextWriter output;
		public override TextWriter Output
		{
			get { return output ?? (output = new StringWriter()); }
		}

		public override string Status { get; set; }

		public override int StatusCode { get; set; }

		private HttpCookieCollection cookies = new HttpCookieCollection();
		public override HttpCookieCollection Cookies
		{
			get { return cookies; }
		}

		public override void Write(string s)
		{
			Output.Write(s);
		}
	}
}