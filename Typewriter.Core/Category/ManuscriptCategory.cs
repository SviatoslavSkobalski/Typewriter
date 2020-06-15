using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Typewriter.Core.Category
{
    [DataContract]
    public class ManuscriptCategory : IManuscriptCategory, INotifyCollectionChanged
    {
        public ManuscriptCategory(Guid projectId, int id, string name)
        {
            ProjectId = projectId;
            Id = id;
            Name = name;
            Items = new List<IManuscriptItem>();
        }

        public ManuscriptCategory(Guid projectId, int id, string name, List<IManuscriptItem> items) : this(projectId, id, name)
        {
            Items = items;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<IManuscriptItem> Items { get; set; }

        public void AddItem(IManuscriptItem item)
        {
            if (item != null)
            {
                Items.Add(item);

                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        public void AddItem(IManuscriptItem item, string source)
        {
            if (item != null && !File.Exists(source))
            {
                item.Source = source;

                Items.Add(item);

                File.Create(source);

                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        public void RemoveItem(IManuscriptItem item)
        {
            if (item == null)
            {
                return;
            }

            Items.Remove(item);

            OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, item.Id));
        }

        public IManuscriptItem GetItemById(int id)
        {
            return Items.Where(i => i.Id == id).FirstOrDefault();
        }

        protected virtual void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
