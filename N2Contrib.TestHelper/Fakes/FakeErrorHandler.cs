using System;
using N2.Web;

namespace N2Contrib.TestHelper.Fakes
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