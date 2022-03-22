using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class QuestTrackerFactory : IComponentFactory
    {
        // The displayed name of the component in the Layout Editor.
        public string ComponentName => "All Quests Tracker";

        public string Description => "Displays if All Quests have been done during the run.";

        // The sub-menu this component will appear under when adding the component to the layout.
        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new QuestTrackerComponent(state);

        public string UpdateName => ComponentName;

        // Fill in this empty string with the URL of the repository where your component is hosted.
        // This should be the raw content version of the repository. If you're not uploading this
        // to GitHub or somewhere, you can ignore this.
        public string UpdateURL => "";

        // Fill in this empty string with the path of the XML file containing update information.
        // Check other LiveSplit components for examples of this. If you're not uploading this to
        // GitHub or somewhere, you can ignore this.
        public string XMLURL => UpdateURL + "";

        public Version Version => Version.Parse("1.0.0");
    }
}