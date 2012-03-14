using System.Collections;
using System.Web;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeHttpSessionState : HttpSessionStateBase
	{
		public Hashtable state = new Hashtable();

		public override object this[string name]
		{
			get
			{
				return state[name];
			}
			set
			{
				state[name] = value;
			}
		}
	}
}
