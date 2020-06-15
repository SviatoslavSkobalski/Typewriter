using System.Drawing;
using System.IO;

namespace Typewriter.DAL
{
    public class FileContentLoader
    {
        private readonly string _source;

        public FileContentLoader(string source)
        {
            _source = source;
        }

        public object LoadContent()
        {
            if (!File.Exists(_source))
            {
                throw new FileNotFoundException();
            }

            if (Path.GetExtension(_source) == ".jpg")
            {
                return Image.FromFile(_source);
            }

            if (Path.GetExtension(_source) == ".txt")
            {
                return File.ReadAllText(_source);
            }

            return new object();
        }
    }
}
