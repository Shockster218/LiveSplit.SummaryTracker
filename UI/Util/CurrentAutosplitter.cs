using LiveSplit.UI.Components;
using System.Xml;

namespace LiveSplit.UI.Util
{
    public static class CurrentAutosplitter
    {
        public static IComponent component { get; set; }

        public static void ActivateReset()
        {
            XmlElement settings = (XmlElement)component.GetSettings(new XmlDocument());
            settings["Reset"].InnerText = "True";
            component.SetSettings(settings);
        }

        public static void DeactivateReset()
        {
            XmlElement settings = (XmlElement)component.GetSettings(new XmlDocument());
            settings["Reset"].InnerText = "False";
            component.SetSettings(settings);
        }
    }
}
