using System;
using System.Collections.Generic;
using Typewriter.Core.Category;

namespace Typewriter.Core.Project
{
    public interface IManuscriptProject
    {
        Guid Id { get; set; }

        List<IManuscriptCategory> Categories { get; set; }

        string Name { get; set; }

        string Source { get; set; }
    }
}