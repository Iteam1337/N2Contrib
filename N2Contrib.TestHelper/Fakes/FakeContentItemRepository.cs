using N2;
using N2.Persistence;
using System;
using System.Collections.Generic;

namespace N2Contrib.TestHelper.Fakes
{
    public class FakeContentItemRepository : IContentItemRepository
    {
        public IEnumerable<DiscriminatorCount> FindDescendantDiscriminators(ContentItem ancestor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentItem> FindDescendants(ContentItem ancestor, string discriminator)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentItem> FindReferencing(ContentItem linkTarget)
        {
            throw new NotImplementedException();
        }

        public int RemoveReferencesToRecursive(ContentItem target)
        {
            throw new NotImplementedException();
        }

        public ITransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public long Count(IParameter parameters)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(ContentItem entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentItem> Find(IParameter parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentItem> Find(params Parameter[] propertyValuesToMatchAll)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentItem> Find(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(object id)
        {
            throw new NotImplementedException();
        }

        public ContentItem Get(object id)
        {
            throw new NotImplementedException();
        }

        public ITransaction GetTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(ContentItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDictionary<string, object>> Select(IParameter parameters, params string[] properties)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
