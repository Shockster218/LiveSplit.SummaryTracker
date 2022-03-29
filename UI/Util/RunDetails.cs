using System;
using LiveSplit.Model;

namespace LiveSplit.UI.Util
{
    public class RunDetails
    {
        public string player { get; set; }
        public DateTime completionDate { get; set; }
        public Time runTime { get; set; }
        public Level questMissed { get; set; }

        public RunDetails(string player)
        {
            this.player = player;
            completionDate = new DateTime();
            runTime = Time.Zero;
            questMissed = Level.None;
        }

        public void Clear()
        {
            completionDate = new DateTime();
            runTime = Time.Zero;
            questMissed = Level.None;
        }
    }
}
