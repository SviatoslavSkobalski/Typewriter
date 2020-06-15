using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Typewriter.Core.Item
{
    [DataContract]
    public class ManuscriptItem : IManuscriptItem, INotifyCollectionChanged
    {
        public ManuscriptItem(int id, int categoryId, Guid projectId, string name)
        {
            Id = id;
            CategoryId = categoryId;
            ProjectId = projectId;
            Name = name;
            References = new List<IManuscriptItemReference>();
        }

        public ManuscriptItem(int id, int categoryId, Guid projectId, string name, string source) : this(id, categoryId, projectId, name)
        {
            Source = source;
            References = new List<IManuscriptItemReference>();
        }

        public ManuscriptItem(int id, int categoryId, Guid projectId, string name, string source, ICollection<IManuscriptItemReference> references) : this(id, categoryId, projectId, name, source)
        {
            References = references as List<IManuscriptItemReference>;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [DataMember]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public List<IManuscriptItemReference> References { get; set; }

        public void AddReference(IManuscriptItemReference reference)
        {
            if (reference != null)
            {
                References.Add(reference);

                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, reference));
            }
        }

        public void RemoveReference(IManuscriptItemReference reference)
        {
            if (reference != null)
            {
                References.Remove(reference);

                OnCollectionChange(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, reference));
            }
        }

        protected virtual void OnCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
