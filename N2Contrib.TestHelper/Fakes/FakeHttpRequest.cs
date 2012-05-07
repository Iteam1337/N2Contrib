using System;
using System.Collections.Specialized;
using System.Web;
using System.Linq;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeHttpRequest : HttpRequestBase
	{
		public string appRelativeCurrentExecutionFilePath = "~/";
		public NameValueCollection query = new NameValueCollection();
		public NameValueCollection param = new NameValueCollection();
		public string rawUrl = "/";

		public override System.Uri Url
		{
			get { return new Uri("http://localhost" + rawUrl, UriKind.RelativeOrAbsolute); }
		}
		public override string AppRelativeCurrentExecutionFilePath
		{
			get { return appRelativeCurrentExecutionFilePath; }
		}
		public override string RawUrl
		{
			get { return rawUrl; }
		}
		public override string this[string key]
		{
			get { return query[key]; }
		}
		public override string ApplicationPath
		{
			get { return "/"; }
		}
		public override string PathInfo
		{
			get { return ""; }
		}
		public override NameValueCollection QueryString
		{
			get { return query; }
		}

		public override NameValueCollection Params
		{
			get { return param; }
		}

		public override string PhysicalPath
		{
			get { return MapPath(appRelativeCurrentExecutionFilePath); }
		}

		public override string MapPath(string virtualPath)
		{
			return Environment.CurrentDirectory + virtualPath.Replace('/', '\\').Trim('~');
		}

		public NameValueCollection serverVariables = new NameValueCollection();
		public override NameValueCollection ServerVariables
		{
			get { return serverVariables; }
		}

		public override void ValidateInput()
		{
		}

		public void SetQuery(string queryString)
		{
			query = new System.Collections.Specialized.NameValueCollection();
			foreach (var kvp in N2.Web.Url.ParseQueryString(queryString))
				query[kvp.Key] = kvp.Value;
		}
	}
}