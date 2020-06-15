using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace Typewriter.Core.Reference
{
    [DataContract]
    public class ManuscriptItemReference : IManuscriptItemReference
    {
        public ManuscriptItemReference(int id, int itemId, string source)
        {
            Id = id;
            ItemId = itemId;
            Source = source;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string Source { get; set; }
    }
}
