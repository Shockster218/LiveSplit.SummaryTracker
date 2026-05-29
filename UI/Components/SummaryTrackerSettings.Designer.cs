
namespace LiveSplit.UI.Components
{
    partial class SummaryTrackerSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topLevelLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.categoryLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.logLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logHeaderLabel = new System.Windows.Forms.Label();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.topLevelLayoutPanel.SuspendLayout();
            this.categoryLayoutPanel.SuspendLayout();
            this.logLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topLevelLayoutPanel
            // 
            this.topLevelLayoutPanel.AutoSize = true;
            this.topLevelLayoutPanel.ColumnCount = 1;
            this.topLevelLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLevelLayoutPanel.Controls.Add(this.categoryLayoutPanel, 0, 0);
            this.topLevelLayoutPanel.Controls.Add(this.logLayoutPanel, 0, 1);
            this.topLevelLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLevelLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topLevelLayoutPanel.Name = "topLevelLayoutPanel";
            this.topLevelLayoutPanel.RowCount = 2;
            this.topLevelLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.topLevelLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.topLevelLayoutPanel.Size = new System.Drawing.Size(478, 376);
            this.topLevelLayoutPanel.TabIndex = 0;
            // 
            // categoryLayoutPanel
            // 
            this.categoryLayoutPanel.AutoSize = true;
            this.categoryLayoutPanel.ColumnCount = 2;
            this.categoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.categoryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.categoryLayoutPanel.Controls.Add(this.categoryLabel, 0, 0);
            this.categoryLayoutPanel.Controls.Add(this.categoryComboBox, 1, 0);
            this.categoryLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.categoryLayoutPanel.Name = "categoryLayoutPanel";
            this.categoryLayoutPanel.RowCount = 1;
            this.categoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.categoryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.categoryLayoutPanel.Size = new System.Drawing.Size(472, 106);
            this.categoryLayoutPanel.TabIndex = 0;
            // 
            // categoryLabel
            // 
            this.categoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("Titillium Web SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryLabel.Location = new System.Drawing.Point(3, 37);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(230, 32);
            this.categoryLabel.TabIndex = 0;
            this.categoryLabel.Text = "Choose Category";
            this.categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Font = new System.Drawing.Font("Titillium Web SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Items.AddRange(new object[] {
            "All Quests",
            "100%",
            "ARQ Glitchless"});
            this.categoryComboBox.Location = new System.Drawing.Point(239, 37);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(230, 32);
            this.categoryComboBox.TabIndex = 7;
            // 
            // logLayoutPanel
            // 
            this.logLayoutPanel.AutoSize = true;
            this.logLayoutPanel.ColumnCount = 1;
            this.logLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.logLayoutPanel.Controls.Add(this.logHeaderLabel, 0, 0);
            this.logLayoutPanel.Controls.Add(this.detailsLabel, 0, 1);
            this.logLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logLayoutPanel.Location = new System.Drawing.Point(3, 115);
            this.logLayoutPanel.Name = "logLayoutPanel";
            this.logLayoutPanel.RowCount = 2;
            this.logLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.logLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.logLayoutPanel.Size = new System.Drawing.Size(472, 258);
            this.logLayoutPanel.TabIndex = 1;
            // 
            // logHeaderLabel
            // 
            this.logHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.logHeaderLabel.AutoSize = true;
            this.logHeaderLabel.Font = new System.Drawing.Font("Titillium Web", 15.75F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logHeaderLabel.Location = new System.Drawing.Point(3, 4);
            this.logHeaderLabel.Name = "logHeaderLabel";
            this.logHeaderLabel.Size = new System.Drawing.Size(466, 32);
            this.logHeaderLabel.TabIndex = 0;
            this.logHeaderLabel.Text = "Last Failed Run Log";
            this.logHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailsLabel
            // 
            this.detailsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Font = new System.Drawing.Font("Titillium Web SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.detailsLabel.Location = new System.Drawing.Point(3, 40);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Padding = new System.Windows.Forms.Padding(25);
            this.detailsLabel.Size = new System.Drawing.Size(466, 218);
            this.detailsLabel.TabIndex = 1;
            this.detailsLabel.Text = "Nothing to Display";
            // 
            // SummaryTrackerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.topLevelLayoutPanel);
            this.Name = "SummaryTrackerSettings";
            this.Size = new System.Drawing.Size(478, 376);
            this.Load += new System.EventHandler(this.QuestTrackerSettings_Load);
            this.topLevelLayoutPanel.ResumeLayout(false);
            this.topLevelLayoutPanel.PerformLayout();
            this.categoryLayoutPanel.ResumeLayout(false);
            this.categoryLayoutPanel.PerformLayout();
            this.logLayoutPanel.ResumeLayout(false);
            this.logLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topLevelLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel categoryLayoutPanel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.TableLayoutPanel logLayoutPanel;
        private System.Windows.Forms.Label logHeaderLabel;
        private System.Windows.Forms.Label detailsLabel;
    }
}
