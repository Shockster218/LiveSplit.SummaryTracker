using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class SummaryTrackerFactory : IComponentFactory
    {
        // The displayed name of the component in the Layout Editor.
        public string ComponentName => "The Hobbit - Summary Tracker";

        public string Description => "A tool for The Hobbit that helps verify if All Quests or 100% have been successfully completed during the run.";

        // The sub-menu this component will appear under when adding the component to the layout.
        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state) => new SummaryTrackerComponent(state);

        public string UpdateName => ComponentName;

        // Fill in this empty string with the URL of the repository where your component is hosted.
        // This should be the raw content version of the repository. If you're not uploading this
        // to GitHub or somewhere, you can ignore this.
        public string UpdateURL => "";

        // Fill in this empty string with the path of the XML file containing update information.
        // Check other LiveSplit components for examples of this. If you're not uploading this to
        // GitHub or somewhere, you can ignore this.
        public string XMLURL => UpdateURL + "";

        public Version Version => Version.Parse(Constants.Version);

    }
}