namespace Typewriter.Core
{
    public interface IManuscriptItemReference
    {
        int Id { get; set; }

        int ItemId { get; set; }

        string Source { get; set; }
    }
}