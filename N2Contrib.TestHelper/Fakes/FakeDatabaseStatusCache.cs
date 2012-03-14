using N2.Edit.Installation;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeDatabaseStatusCache : DatabaseStatusCache
	{
		public FakeDatabaseStatusCache(InstallationManager installer)
			: base(installer)
		{
		}

		public SystemStatusLevel level = SystemStatusLevel.UpAndRunning;
		public override SystemStatusLevel GetStatus()
		{
			return level;
		}
	}
}
