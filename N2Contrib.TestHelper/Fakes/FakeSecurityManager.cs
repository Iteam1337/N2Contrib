using System;
using System.Collections.Generic;
using System.Security.Principal;
using N2;
using N2.Collections;
using N2.Security;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeSecurityManager : ISecurityManager
	{
		public Func<IPrincipal, ContentItem, bool> AuthorizationDelegate = (user, item) => true;

		#region ISecurityManager Members

		public bool IsEditor(System.Security.Principal.IPrincipal principal)
		{
			return principal != null && principal.Identity.Name == "Editor" || principal.IsInRole("Editors");
		}

		public bool IsAdmin(System.Security.Principal.IPrincipal principal)
		{
			return principal != null && principal.Identity.Name == "Admin" || principal.IsInRole("Administrators");
		}

		public bool IsAuthorized(ContentItem item, System.Security.Principal.IPrincipal user)
		{
			return AuthorizationDelegate(user, item);
		}

		public bool IsPublished(ContentItem item)
		{
			return PublishedFilter.IsPublished(item);
		}

		public bool Enabled { get; set; }

		public bool ScopeEnabled { get; set; }

		public bool IsAuthorized(System.Security.Principal.IPrincipal user, IEnumerable<string> roles)
		{
			return AuthorizationDelegate(user, null);
		}

		public bool IsAuthorized(IPrincipal user, Permission permission)
		{
			return AuthorizationDelegate(user, null);
		}

		public bool IsAuthorized(IPrincipal principal, ContentItem item, Permission permission)
		{
			return AuthorizationDelegate(principal, item);
		}

		public void CopyPermissions(ContentItem source, ContentItem destination)
		{
		}

		public Permission GetPermissions(IPrincipal user, ContentItem item)
		{
			return item["Permission"] as Permission? ?? Permission.None;
		}

		public ItemFilter GetAuthorizationFilter(Permission permission)
		{
			return new NullFilter();
		}

		#endregion
	}
}
