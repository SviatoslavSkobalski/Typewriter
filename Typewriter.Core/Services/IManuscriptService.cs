using System.Collections.Generic;
using Typewriter.Core.Category;
using Typewriter.Core.Project;

namespace Typewriter.Core
{
    public interface IManuscriptService
    {
        IManuscriptProject CurrentProject { get; set; }

        List<IManuscriptCategory> CurrentProjectCategories { get; }

        IManuscriptProject CreateDefaultProject(bool setCurrent);

        IManuscriptProject GetFromFile(string filePath);

        IManuscriptCategory GetCategoryById(int id);

        void SaveToFile(string filePath, IManuscriptProject project);
    }
}