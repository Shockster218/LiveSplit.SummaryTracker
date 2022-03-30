using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class QuestTrackerSettings : UserControl
    {
        public string Username { get; set; }
        public bool LiteMode { get; set; }
        public bool AutoReset { get; set; }
        public LayoutMode Mode { get; set; }
        public QuestTrackerSettings()
        {
            InitializeComponent();
            LiteMode = false;
            AutoReset = true;
        }

        private void QuestTrackerSettings_Load(object sender, EventArgs e)
        {
            Checkbox_LiteMode.DataBindings.Clear();
            Checkbox_AutoReset.DataBindings.Clear();
            UsernameTB.DataBindings.Clear();

            if (Mode == LayoutMode.Horizontal)
            {
                Checkbox_LiteMode.Enabled = false;
            }
            else
            {
                Checkbox_LiteMode.DataBindings.Add("Checked", this, "LiteMode", false, DataSourceUpdateMode.OnPropertyChanged);
            }

            Checkbox_AutoReset.DataBindings.Add("Checked", this, "AutoReset", false, DataSourceUpdateMode.OnPropertyChanged);
            UsernameTB.DataBindings.Add("Text", this, "Username");
        }
        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", Constants.Version) ^
                SettingsHelper.CreateSetting(document, parent, "LiteMode", LiteMode) ^
                SettingsHelper.CreateSetting(document, parent, "AutoReset", AutoReset) ^
                SettingsHelper.CreateSetting(document, parent, "Username", Username);
        }
        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            LiteMode = SettingsHelper.ParseBool(element["LiteMode"]);
            AutoReset = SettingsHelper.ParseBool(element["AutoReset"]);
            Username = SettingsHelper.ParseString(element["Username"]);
        }
    }
}
