using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Plugin;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeConnectionMonitor : ConnectionMonitor
	{
		public FakeConnectionMonitor()
		{
			IsConnected = true;
		}
	}
}
