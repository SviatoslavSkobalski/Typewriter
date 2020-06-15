using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Typewriter.Core
{
    public interface IManuscriptItem
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;

        int Id { get; }

        string Name { get; set; }

        int CategoryId { get; }

        Guid ProjectId { get; }

        string Source { get; set; }

        List<IManuscriptItemReference> References { get; set; }

        void AddReference(IManuscriptItemReference reference);

        void RemoveReference(IManuscriptItemReference reference);
    }
}