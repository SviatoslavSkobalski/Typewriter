using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Typewriter.Core.Category
{
    public interface IManuscriptCategory
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;

        Guid ProjectId { get; set; }

        int Id { get; set; }

        string Name { get; set; }

        List<IManuscriptItem> Items { get; set; }

        void AddItem(IManuscriptItem item);

        void AddItem(IManuscriptItem item, string source);

        void RemoveItem(IManuscriptItem item);

        IManuscriptItem GetItemById(int id);
    }
}
