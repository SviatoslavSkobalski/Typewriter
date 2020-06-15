using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Typewriter.Core.Category;
using Typewriter.Core.Project;

namespace Typewriter.Core
{
    public class ManuscriptService : IManuscriptService
    {
        public IManuscriptProject CurrentProject { get; set; }

        public List<IManuscriptCategory> CurrentProjectCategories { get => CurrentProject.Categories; }

        public IManuscriptProject CreateDefaultProject()
        {
            return new ManuscriptProject(Guid.NewGuid(), "New Project");
        }

        public IManuscriptProject CreateDefaultProject(bool setCurrent)
        {
            var current = CreateDefaultProject();

            if (setCurrent)
            {
                CurrentProject = current;
            }

            return current;
        }

        public IManuscriptProject GetFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "The file path was empty");
            }

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var project = ReadFromStream(file);
                CurrentProject = project;
                return project;
            }
        }

        public IManuscriptCategory GetCategoryById(int id)
        {
            return CurrentProjectCategories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void SaveToFile(string filePath, IManuscriptProject project)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "The file path was empty");
            }

            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                project.Name = Path.GetFileNameWithoutExtension(filePath);
                project.Source = filePath;
                WriteToStream(file, project);
            }
        }

        private IManuscriptProject ReadFromStream(Stream stream)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ManuscriptProject));
            var obj = jsonSerializer.ReadObject(stream) as IManuscriptProject;
            return obj;
        }

        private void WriteToStream(Stream stream, IManuscriptProject project)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ManuscriptProject));
            jsonSerializer.WriteObject(stream, project);
        }
    }
}
