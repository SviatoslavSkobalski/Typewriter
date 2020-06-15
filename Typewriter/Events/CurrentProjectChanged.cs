using Prism.Events;
using Typewriter.Core.Project;

namespace Typewriter.Commands.Events
{
    public class CurrentProjectChanged : PubSubEvent<IManuscriptProject>
    {
    }
}
