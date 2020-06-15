using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Typewriter.Core.Category;
using Typewriter.Core.Item;
using Typewriter.Core.Reference;

namespace Typewriter.Core.Project
{
    [DataContract]
    [KnownType(typeof(ManuscriptProject))]
    [KnownType(typeof(ManuscriptItem))]
    [KnownType(typeof(ManuscriptItemReference))]
    [KnownType(typeof(ManuscriptCategory))]
    public class ManuscriptProject : IManuscriptProject
    {
        public ManuscriptProject(Guid id, string name)
        {
            Id = id;
            Name = name;
            Categories = new List<IManuscriptCategory>()
                    {
                        new ManuscriptCategory(id, 0, "Script", new List<IManuscriptItem>()),
                        new ManuscriptCategory(id, 1, "Character", new List<IManuscriptItem>()),
                        new ManuscriptCategory(id, 2, "Research", new List<IManuscriptItem>()),
                    };
        }

        public ManuscriptProject(Guid id, string name, string source, List<IManuscriptCategory> items)
        {
            Id = id;
            Name = name;
            Source = source;
            Categories = items;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public List<IManuscriptCategory> Categories { get; set; }
    }
}
