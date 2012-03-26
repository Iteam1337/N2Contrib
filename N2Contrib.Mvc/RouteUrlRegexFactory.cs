using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace N2Contrib.Mvc
{
	public class RouteUrlRegexFactory
	{
		readonly static Regex regexFactory = new Regex("(?<token>{[^}]*?(?<all>[*])?})(?<slash>/)?", RegexOptions.Compiled);

		/// <summary>
		/// Creates a new regex matching routes looking like the given url pattern.
		/// </summary>
		/// <param name="url">A route-like url pattern.</param>
		/// <returns>A regular expression that matches the given route.</returns>
		public virtual Regex CreateExpression(string url)
		{
			var urlPattern = regexFactory.Replace(url, ReplaceUrlPattern);
			Debug.WriteLine("pattern " + urlPattern);
			return new Regex(urlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		}

		private string ReplaceUrlPattern(Match m)
		{
			return "(?<" // start matching group used to retrieve route value
				+ ToKey(m.Groups["token"].Value) // name of group
				+ ">[^/]+" // match value until slash
				+ (m.Groups["all"].Success ? ".*" : "") // if catchall (*) continue matching into the group
				+ ")?"
				+ (m.Groups["slash"].Success ? "/?" : ""); // if trailing slash propagate it to the expression
		}

		public virtual string ToKey(string token)
		{
			return token.Trim('*', '{', '}');
		}
	}
}
