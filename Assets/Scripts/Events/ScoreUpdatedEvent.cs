using Plugins.Common;

namespace Events
{
    public class ScoreUpdatedEvent : IEvent
    {
        public int score;

        public ScoreUpdatedEvent(int score)
        {
            this.score = score;
        }
    }
}