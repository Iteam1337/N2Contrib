using System.Collections.Generic;
using System.Linq;
using N2.Edit.Workflow;
using N2.Persistence;
using N2;

namespace N2Contrib.TestHelper.Fakes
{
	public class FakeVersionManager : VersionManager
    {
        FakeRepository<ContentItem> itemRepository;

        public FakeVersionManager(FakeRepository<ContentItem> itemRepository, StateChanger stateChanger)
			: base(itemRepository, null, stateChanger, new N2.Configuration.EditSection())
		{
            this.itemRepository = itemRepository;
		}

        #region IVersionManager Members

        public override IList<ContentItem> GetVersionsOf(ContentItem publishedItem)
        {
            return itemRepository.database.Values.Where(i => i.VersionOf.Value == publishedItem || i == publishedItem).OrderByDescending(i => i.VersionIndex).ToList();
        }

        public override IList<ContentItem> GetVersionsOf(ContentItem publishedItem, int count)
        {
            return GetVersionsOf(publishedItem).Take(count).ToList();
        }

        public override void TrimVersionCountTo(ContentItem publishedItem, int maximumNumberOfVersions)
        {
        }

		#endregion
	}
}
