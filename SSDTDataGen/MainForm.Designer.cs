namespace SSDTDataGen
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serverAddressTextBox = new System.Windows.Forms.TextBox();
			this.serverAddressLabel = new System.Windows.Forms.Label();
			this.serverRefreshButton = new System.Windows.Forms.Button();
			this.databaseDDL = new System.Windows.Forms.ComboBox();
			this.SchemaDDL = new System.Windows.Forms.ComboBox();
			this.TableDDL = new System.Windows.Forms.ComboBox();
			this.databaseLabel = new System.Windows.Forms.Label();
			this.schemaLabel = new System.Windows.Forms.Label();
			this.tableLable = new System.Windows.Forms.Label();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.IdentityInsertCB = new System.Windows.Forms.CheckBox();
			this.Step1Panel = new System.Windows.Forms.Panel();
			this.SqlAuthCB = new System.Windows.Forms.CheckBox();
			this.Step1Label = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.WhereLabel = new System.Windows.Forms.Label();
			this.WhereTextBox = new System.Windows.Forms.TextBox();
			this.Step2Label = new System.Windows.Forms.Label();
			this.Step3Label = new System.Windows.Forms.Label();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.Step1Panel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// serverAddressTextBox
			// 
			this.serverAddressTextBox.Location = new System.Drawing.Point(103, 12);
			this.serverAddressTextBox.Name = "serverAddressTextBox";
			this.serverAddressTextBox.Size = new System.Drawing.Size(313, 22);
			this.serverAddressTextBox.TabIndex = 0;
			// 
			// serverAddressLabel
			// 
			this.serverAddressLabel.AutoSize = true;
			this.serverAddressLabel.Location = new System.Drawing.Point(23, 12);
			this.serverAddressLabel.Name = "serverAddressLabel";
			this.serverAddressLabel.Size = new System.Drawing.Size(54, 17);
			this.serverAddressLabel.TabIndex = 1;
			this.serverAddressLabel.Text = "Server:";
			// 
			// serverRefreshButton
			// 
			this.serverRefreshButton.Location = new System.Drawing.Point(171, 193);
			this.serverRefreshButton.Name = "serverRefreshButton";
			this.serverRefreshButton.Size = new System.Drawing.Size(128, 23);
			this.serverRefreshButton.TabIndex = 2;
			this.serverRefreshButton.Text = "Refresh";
			this.serverRefreshButton.UseVisualStyleBackColor = true;
			this.serverRefreshButton.Click += new System.EventHandler(this.serverRefreshButton_Click);
			// 
			// databaseDDL
			// 
			this.databaseDDL.FormattingEnabled = true;
			this.databaseDDL.Location = new System.Drawing.Point(50, 30);
			this.databaseDDL.Name = "databaseDDL";
			this.databaseDDL.Size = new System.Drawing.Size(161, 24);
			this.databaseDDL.TabIndex = 3;
			this.databaseDDL.SelectedIndexChanged += new System.EventHandler(this.databaseDDL_SelectedIndexChanged);
			// 
			// SchemaDDL
			// 
			this.SchemaDDL.FormattingEnabled = true;
			this.SchemaDDL.Location = new System.Drawing.Point(246, 30);
			this.SchemaDDL.Name = "SchemaDDL";
			this.SchemaDDL.Size = new System.Drawing.Size(161, 24);
			this.SchemaDDL.TabIndex = 4;
			this.SchemaDDL.SelectedIndexChanged += new System.EventHandler(this.SchemaDDL_SelectedIndexChanged);
			// 
			// TableDDL
			// 
			this.TableDDL.FormattingEnabled = true;
			this.TableDDL.Location = new System.Drawing.Point(50, 94);
			this.TableDDL.Name = "TableDDL";
			this.TableDDL.Size = new System.Drawing.Size(357, 24);
			this.TableDDL.TabIndex = 5;
			this.TableDDL.SelectedIndexChanged += new System.EventHandler(this.TableDDL_SelectedIndexChanged);
			// 
			// databaseLabel
			// 
			this.databaseLabel.AutoSize = true;
			this.databaseLabel.Location = new System.Drawing.Point(96, 10);
			this.databaseLabel.Name = "databaseLabel";
			this.databaseLabel.Size = new System.Drawing.Size(69, 17);
			this.databaseLabel.TabIndex = 6;
			this.databaseLabel.Text = "Database";
			// 
			// schemaLabel
			// 
			this.schemaLabel.AutoSize = true;
			this.schemaLabel.Location = new System.Drawing.Point(276, 6);
			this.schemaLabel.Name = "schemaLabel";
			this.schemaLabel.Size = new System.Drawing.Size(59, 17);
			this.schemaLabel.TabIndex = 7;
			this.schemaLabel.Text = "Schema";
			// 
			// tableLable
			// 
			this.tableLable.AutoSize = true;
			this.tableLable.Location = new System.Drawing.Point(195, 68);
			this.tableLable.Name = "tableLable";
			this.tableLable.Size = new System.Drawing.Size(44, 17);
			this.tableLable.TabIndex = 8;
			this.tableLable.Text = "Table";
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(297, 637);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(128, 23);
			this.GenerateButton.TabIndex = 9;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// IdentityInsertCB
			// 
			this.IdentityInsertCB.AutoSize = true;
			this.IdentityInsertCB.Checked = true;
			this.IdentityInsertCB.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IdentityInsertCB.Location = new System.Drawing.Point(27, 233);
			this.IdentityInsertCB.Name = "IdentityInsertCB";
			this.IdentityInsertCB.Size = new System.Drawing.Size(114, 21);
			this.IdentityInsertCB.TabIndex = 10;
			this.IdentityInsertCB.Text = "Identity Insert";
			this.IdentityInsertCB.UseVisualStyleBackColor = true;
			// 
			// Step1Panel
			// 
			this.Step1Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Step1Panel.Controls.Add(this.PasswordTextBox);
			this.Step1Panel.Controls.Add(this.userNameTextBox);
			this.Step1Panel.Controls.Add(this.SqlAuthCB);
			this.Step1Panel.Controls.Add(this.serverAddressTextBox);
			this.Step1Panel.Controls.Add(this.serverAddressLabel);
			this.Step1Panel.Controls.Add(this.serverRefreshButton);
			this.Step1Panel.Location = new System.Drawing.Point(125, 1);
			this.Step1Panel.Name = "Step1Panel";
			this.Step1Panel.Size = new System.Drawing.Size(463, 266);
			this.Step1Panel.TabIndex = 11;
			// 
			// SqlAuthCB
			// 
			this.SqlAuthCB.AutoSize = true;
			this.SqlAuthCB.Location = new System.Drawing.Point(27, 64);
			this.SqlAuthCB.Name = "SqlAuthCB";
			this.SqlAuthCB.Size = new System.Drawing.Size(91, 21);
			this.SqlAuthCB.TabIndex = 3;
			this.SqlAuthCB.Text = "SQL Auth";
			this.SqlAuthCB.UseVisualStyleBackColor = true;
			// 
			// Step1Label
			// 
			this.Step1Label.AutoSize = true;
			this.Step1Label.Location = new System.Drawing.Point(26, 34);
			this.Step1Label.Name = "Step1Label";
			this.Step1Label.Size = new System.Drawing.Size(53, 17);
			this.Step1Label.TabIndex = 12;
			this.Step1Label.Text = "Step 1:";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.WhereLabel);
			this.panel1.Controls.Add(this.WhereTextBox);
			this.panel1.Controls.Add(this.databaseDDL);
			this.panel1.Controls.Add(this.SchemaDDL);
			this.panel1.Controls.Add(this.TableDDL);
			this.panel1.Controls.Add(this.IdentityInsertCB);
			this.panel1.Controls.Add(this.databaseLabel);
			this.panel1.Controls.Add(this.schemaLabel);
			this.panel1.Controls.Add(this.tableLable);
			this.panel1.Location = new System.Drawing.Point(125, 290);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(463, 267);
			this.panel1.TabIndex = 13;
			// 
			// WhereLabel
			// 
			this.WhereLabel.AutoSize = true;
			this.WhereLabel.Location = new System.Drawing.Point(195, 122);
			this.WhereLabel.Name = "WhereLabel";
			this.WhereLabel.Size = new System.Drawing.Size(58, 17);
			this.WhereLabel.TabIndex = 12;
			this.WhereLabel.Text = "Where :";
			// 
			// WhereTextBox
			// 
			this.WhereTextBox.Location = new System.Drawing.Point(50, 142);
			this.WhereTextBox.MaxLength = 65536;
			this.WhereTextBox.Multiline = true;
			this.WhereTextBox.Name = "WhereTextBox";
			this.WhereTextBox.Size = new System.Drawing.Size(357, 66);
			this.WhereTextBox.TabIndex = 11;
			// 
			// Step2Label
			// 
			this.Step2Label.AutoSize = true;
			this.Step2Label.Location = new System.Drawing.Point(26, 394);
			this.Step2Label.Name = "Step2Label";
			this.Step2Label.Size = new System.Drawing.Size(53, 17);
			this.Step2Label.TabIndex = 14;
			this.Step2Label.Text = "Step 2:";
			// 
			// Step3Label
			// 
			this.Step3Label.AutoSize = true;
			this.Step3Label.Location = new System.Drawing.Point(26, 637);
			this.Step3Label.Name = "Step3Label";
			this.Step3Label.Size = new System.Drawing.Size(53, 17);
			this.Step3Label.TabIndex = 15;
			this.Step3Label.Text = "Step 3:";
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Location = new System.Drawing.Point(171, 64);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(245, 22);
			this.userNameTextBox.TabIndex = 4;
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Location = new System.Drawing.Point(171, 131);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.Size = new System.Drawing.Size(245, 22);
			this.PasswordTextBox.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(589, 751);
			this.Controls.Add(this.Step3Label);
			this.Controls.Add(this.Step2Label);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.Step1Label);
			this.Controls.Add(this.Step1Panel);
			this.Controls.Add(this.GenerateButton);
			this.Name = "MainForm";
			this.Text = "SQL Data Scripter";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Step1Panel.ResumeLayout(false);
			this.Step1Panel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox serverAddressTextBox;
		private System.Windows.Forms.Label serverAddressLabel;
		private System.Windows.Forms.Button serverRefreshButton;
		private System.Windows.Forms.ComboBox databaseDDL;
		private System.Windows.Forms.ComboBox SchemaDDL;
		private System.Windows.Forms.ComboBox TableDDL;
		private System.Windows.Forms.Label databaseLabel;
		private System.Windows.Forms.Label schemaLabel;
		private System.Windows.Forms.Label tableLable;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.CheckBox IdentityInsertCB;
		private System.Windows.Forms.Panel Step1Panel;
		private System.Windows.Forms.Label Step1Label;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label WhereLabel;
		private System.Windows.Forms.TextBox WhereTextBox;
		private System.Windows.Forms.Label Step2Label;
		private System.Windows.Forms.Label Step3Label;
		private System.Windows.Forms.CheckBox SqlAuthCB;
		private System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.TextBox userNameTextBox;
	}
}

