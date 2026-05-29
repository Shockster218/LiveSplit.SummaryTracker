using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class SummaryTrackerSettings : UserControl
    {
        public LayoutMode Mode { get; set;}
        public String Category { get; set; }
        public String Details { get; set; }
        public SummaryTrackerSettings()
        {
            InitializeComponent();
        }
        private void QuestTrackerSettings_Load(object sender, EventArgs e)
        {
            categoryComboBox.DataBindings.Clear();
            detailsLabel.DataBindings.Clear();

            categoryComboBox.DataBindings.Add("SelectedItem", this, "Category", false, DataSourceUpdateMode.OnPropertyChanged);

            detailsLabel.DataBindings.Add("Text", this, "Details");
        }
        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", Constants.Version) ^
            SettingsHelper.CreateSetting(document, parent, "Category", categoryComboBox.SelectedItem) ^
            SettingsHelper.CreateSetting(document, parent, "Details", detailsLabel.Text);
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
            Category = SettingsHelper.ParseString(element["Category"]);
            if (Category == String.Empty) Category = "All Quests";
            Details = SettingsHelper.ParseString(element["Details"]);
        }

        public void SetDetails(string _details)
        {
            Details = _details;
        }
    }
}
