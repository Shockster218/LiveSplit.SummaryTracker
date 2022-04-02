
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
            this.Settings_Groupbox = new System.Windows.Forms.GroupBox();
            this.UsernameDesc = new System.Windows.Forms.Label();
            this.UsernameTitle = new System.Windows.Forms.Label();
            this.UsernameTB = new System.Windows.Forms.TextBox();
            this.LiteMode_Desc = new System.Windows.Forms.Label();
            this.Checkbox_LiteMode = new System.Windows.Forms.CheckBox();
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
            this.topLevelLayoutPanel.Size = new System.Drawing.Size(471, 302);
            this.topLevelLayoutPanel.TabIndex = 0;
            // 
            // Settings_Groupbox
            // 
            this.Settings_Groupbox.Controls.Add(this.UsernameDesc);
            this.Settings_Groupbox.Controls.Add(this.UsernameTitle);
            this.Settings_Groupbox.Controls.Add(this.UsernameTB);
            this.Settings_Groupbox.Controls.Add(this.LiteMode_Desc);
            this.Settings_Groupbox.Controls.Add(this.Checkbox_LiteMode);
            this.Settings_Groupbox.Location = new System.Drawing.Point(3, 3);
            this.Settings_Groupbox.Name = "Settings_Groupbox";
            this.Settings_Groupbox.Size = new System.Drawing.Size(465, 296);
            this.Settings_Groupbox.TabIndex = 0;
            this.Settings_Groupbox.TabStop = false;
            this.Settings_Groupbox.Text = "Settings";
            // 
            // UsernameDesc
            // 
            this.UsernameDesc.AutoSize = true;
            this.UsernameDesc.Font = new System.Drawing.Font("Titillium Web", 9.749999F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameDesc.Location = new System.Drawing.Point(47, 65);
            this.UsernameDesc.MaximumSize = new System.Drawing.Size(420, 0);
            this.UsernameDesc.Name = "UsernameDesc";
            this.UsernameDesc.Size = new System.Drawing.Size(406, 40);
            this.UsernameDesc.TabIndex = 6;
            this.UsernameDesc.Text = "Set your username so the verifiers can easily determine who\'s run they are lookin" +
    "g at.";
            // 
            // UsernameTitle
            // 
            this.UsernameTitle.AutoSize = true;
            this.UsernameTitle.Font = new System.Drawing.Font("Titillium Web SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTitle.Location = new System.Drawing.Point(15, 35);
            this.UsernameTitle.Name = "UsernameTitle";
            this.UsernameTitle.Size = new System.Drawing.Size(70, 20);
            this.UsernameTitle.TabIndex = 5;
            this.UsernameTitle.Text = "Username:";
            // 
            // UsernameTB
            // 
            this.UsernameTB.Font = new System.Drawing.Font("Titillium Web SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTB.Location = new System.Drawing.Point(91, 32);
            this.UsernameTB.MaxLength = 25;
            this.UsernameTB.Name = "UsernameTB";
            this.UsernameTB.Size = new System.Drawing.Size(174, 27);
            this.UsernameTB.TabIndex = 4;
            // 
            // LiteMode_Desc
            // 
            this.LiteMode_Desc.AutoSize = true;
            this.LiteMode_Desc.Font = new System.Drawing.Font("Titillium Web", 9.749999F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LiteMode_Desc.Location = new System.Drawing.Point(47, 139);
            this.LiteMode_Desc.MaximumSize = new System.Drawing.Size(420, 0);
            this.LiteMode_Desc.Name = "LiteMode_Desc";
            this.LiteMode_Desc.Size = new System.Drawing.Size(409, 40);
            this.LiteMode_Desc.TabIndex = 1;
            this.LiteMode_Desc.Text = "Lite mode displays minimal information all in 1 row; for those who need as much s" +
    "pace as possible on their layout.";
            // 
            // Checkbox_LiteMode
            // 
            this.Checkbox_LiteMode.AutoSize = true;
            this.Checkbox_LiteMode.Font = new System.Drawing.Font("Titillium Web SemiBold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Checkbox_LiteMode.Location = new System.Drawing.Point(15, 116);
            this.Checkbox_LiteMode.Name = "Checkbox_LiteMode";
            this.Checkbox_LiteMode.Size = new System.Drawing.Size(84, 24);
            this.Checkbox_LiteMode.TabIndex = 0;
            this.Checkbox_LiteMode.Text = "Lite Mode";
            this.Checkbox_LiteMode.UseVisualStyleBackColor = true;
            // 
            // SummaryTrackerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.topLevelLayoutPanel);
            this.Name = "SummaryTrackerSettings";
            this.Size = new System.Drawing.Size(474, 305);
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
        private System.Windows.Forms.Label UsernameTitle;
        private System.Windows.Forms.TextBox UsernameTB;
        private System.Windows.Forms.Label UsernameDesc;
    }
}
