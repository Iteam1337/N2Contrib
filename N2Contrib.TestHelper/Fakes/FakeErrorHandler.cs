using System;
using N2.Web;

namespace N2.Tests.Fakes
{
	public class FakeErrorHandler : IErrorNotifier
	{
		#region IErrorNotifier Members

		public void Notify(Exception ex)
		{
			ErrorOccured(this, new ErrorEventArgs { Error = ex });
		}

		public event EventHandler<ErrorEventArgs> ErrorOccured = delegate { };

		#endregion
	}
}