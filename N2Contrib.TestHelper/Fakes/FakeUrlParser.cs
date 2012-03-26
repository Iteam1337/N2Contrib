using System;
using N2.Web;
using N2;
using System.Collections.Generic;

namespace N2Contrib.TestHelper.Fakes
{
    public class FakeUrlParser : IUrlParser
    {
        public Dictionary<string, PathData> Paths = new Dictionary<string,PathData>();

        public FakeUrlParser()
        {
            PageNotFound += delegate { };
        }

        public event EventHandler<PageNotFoundEventArgs> PageNotFound;

        public void InvokePageNotFound(PageNotFoundEventArgs e)
        {
            PageNotFound(this, e);
        }

        public string Extension
        {
            get { throw new NotImplementedException(); }
        }

        public ContentItem StartPage
        {
            get { throw new NotImplementedException(); }
        }

        public ContentItem CurrentPage
        {
            get { throw new NotImplementedException(); }
        }

        public Site CurrentSite
        {
            get { throw new NotImplementedException(); }
        }

        public string BuildUrl(ContentItem item)
        {
            Url url = new Url("/");
            foreach (ContentItem parent in N2.Find.EnumerateParents(item, null, true))
            {
                if (true == parent["IsStartPage"] as bool?)
                    return url;
                url = url.PrependSegment(parent.Name);
            }
            return url;
        }

        public bool IsRootOrStartPage(ContentItem item)
        {
            throw new NotImplementedException();
        }

        public PathData ResolvePath(Url url, ContentItem startNode = null, string remainingPath = null)
        {
            if(Paths.ContainsKey(url.ToString()))
                return Paths[url.ToString()];

            return PathData.None(startNode, remainingPath);
        }

        public ContentItem Parse(string url)
        {
            throw new NotImplementedException();
        }

        public ContentItem ParsePage(string url)
        {
            throw new NotImplementedException();
        }

        public string StripDefaultDocument(string path)
        {
            return path;
        }
    }
}