
namespace LiveSplit.UI.Components
{
    partial class QuestTrackerSettings
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
            this.Settings_Groupbox = new System.Windows.Forms.GroupBox();
            this.LiteMode_Desc = new System.Windows.Forms.Label();
            this.DisableReset_Desc = new System.Windows.Forms.Label();
            this.Checkbox_LiteMode = new System.Windows.Forms.CheckBox();
            this.Checkbox_AutoReset = new System.Windows.Forms.CheckBox();
            this.topLevelLayoutPanel.SuspendLayout();
            this.Settings_Groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // topLevelLayoutPanel
            // 
            this.topLevelLayoutPanel.AutoSize = true;
            this.topLevelLayoutPanel.ColumnCount = 1;
            this.topLevelLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLevelLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLevelLayoutPanel.Controls.Add(this.Settings_Groupbox, 0, 0);
            this.topLevelLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topLevelLayoutPanel.Name = "topLevelLayoutPanel";
            this.topLevelLayoutPanel.RowCount = 1;
            this.topLevelLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLevelLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLevelLayoutPanel.Size = new System.Drawing.Size(471, 229);
            this.topLevelLayoutPanel.TabIndex = 0;
            // 
            // Settings_Groupbox
            // 
            this.Settings_Groupbox.Controls.Add(this.LiteMode_Desc);
            this.Settings_Groupbox.Controls.Add(this.DisableReset_Desc);
            this.Settings_Groupbox.Controls.Add(this.Checkbox_LiteMode);
            this.Settings_Groupbox.Controls.Add(this.Checkbox_AutoReset);
            this.Settings_Groupbox.Location = new System.Drawing.Point(3, 3);
            this.Settings_Groupbox.Name = "Settings_Groupbox";
            this.Settings_Groupbox.Size = new System.Drawing.Size(465, 165);
            this.Settings_Groupbox.TabIndex = 0;
            this.Settings_Groupbox.TabStop = false;
            this.Settings_Groupbox.Text = "General Settings";
            // 
            // LiteMode_Desc
            // 
            this.LiteMode_Desc.AutoSize = true;
            this.LiteMode_Desc.Font = new System.Drawing.Font("Titillium Web", 9.749999F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LiteMode_Desc.Location = new System.Drawing.Point(38, 42);
            this.LiteMode_Desc.MaximumSize = new System.Drawing.Size(420, 0);
            this.LiteMode_Desc.Name = "LiteMode_Desc";
            this.LiteMode_Desc.Size = new System.Drawing.Size(409, 40);
            this.LiteMode_Desc.TabIndex = 1;
            this.LiteMode_Desc.Text = "Lite mode displays minimal information all in 1 row; for those who need as much s" +
    "pace as possible on their layout.";
            // 
            // DisableReset_Desc
            // 
            this.DisableReset_Desc.AutoSize = true;
            this.DisableReset_Desc.Font = new System.Drawing.Font("Titillium Web", 9.749999F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisableReset_Desc.Location = new System.Drawing.Point(38, 113);
            this.DisableReset_Desc.MaximumSize = new System.Drawing.Size(420, 0);
            this.DisableReset_Desc.Name = "DisableReset_Desc";
            this.DisableReset_Desc.Size = new System.Drawing.Size(397, 40);
            this.DisableReset_Desc.TabIndex = 2;
            this.DisableReset_Desc.Text = "Automatically toggle the reset function on your autosplitter for when your game c" +
    "rashes during save jump.";
            // 
            // Checkbox_LiteMode
            // 
            this.Checkbox_LiteMode.AutoSize = true;
            this.Checkbox_LiteMode.Font = new System.Drawing.Font("Titillium Web SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Checkbox_LiteMode.Location = new System.Drawing.Point(6, 19);
            this.Checkbox_LiteMode.Name = "Checkbox_LiteMode";
            this.Checkbox_LiteMode.Size = new System.Drawing.Size(84, 24);
            this.Checkbox_LiteMode.TabIndex = 0;
            this.Checkbox_LiteMode.Text = "Lite Mode";
            this.Checkbox_LiteMode.UseVisualStyleBackColor = true;
            // 
            // Checkbox_AutoReset
            // 
            this.Checkbox_AutoReset.AutoSize = true;
            this.Checkbox_AutoReset.Checked = true;
            this.Checkbox_AutoReset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Checkbox_AutoReset.Font = new System.Drawing.Font("Titillium Web SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Checkbox_AutoReset.Location = new System.Drawing.Point(6, 90);
            this.Checkbox_AutoReset.Name = "Checkbox_AutoReset";
            this.Checkbox_AutoReset.Size = new System.Drawing.Size(91, 24);
            this.Checkbox_AutoReset.TabIndex = 3;
            this.Checkbox_AutoReset.Text = "Auto Reset";
            this.Checkbox_AutoReset.UseVisualStyleBackColor = true;
            // 
            // QuestTrackerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.topLevelLayoutPanel);
            this.Name = "QuestTrackerSettings";
            this.Size = new System.Drawing.Size(474, 232);
            this.Load += new System.EventHandler(this.QuestTrackerSettings_Load);
            this.topLevelLayoutPanel.ResumeLayout(false);
            this.Settings_Groupbox.ResumeLayout(false);
            this.Settings_Groupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topLevelLayoutPanel;
        private System.Windows.Forms.GroupBox Settings_Groupbox;
        private System.Windows.Forms.CheckBox Checkbox_LiteMode;
        private System.Windows.Forms.Label LiteMode_Desc;
        private System.Windows.Forms.Label DisableReset_Desc;
        private System.Windows.Forms.CheckBox Checkbox_AutoReset;
    }
}
