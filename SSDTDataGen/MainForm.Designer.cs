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
			this.SuspendLayout();
			// 
			// serverAddressTextBox
			// 
			this.serverAddressTextBox.Location = new System.Drawing.Point(91, 22);
			this.serverAddressTextBox.Name = "serverAddressTextBox";
			this.serverAddressTextBox.Size = new System.Drawing.Size(208, 22);
			this.serverAddressTextBox.TabIndex = 0;
			// 
			// serverAddressLabel
			// 
			this.serverAddressLabel.AutoSize = true;
			this.serverAddressLabel.Location = new System.Drawing.Point(23, 22);
			this.serverAddressLabel.Name = "serverAddressLabel";
			this.serverAddressLabel.Size = new System.Drawing.Size(54, 17);
			this.serverAddressLabel.TabIndex = 1;
			this.serverAddressLabel.Text = "Server:";
			// 
			// serverRefreshButton
			// 
			this.serverRefreshButton.Location = new System.Drawing.Point(145, 51);
			this.serverRefreshButton.Name = "serverRefreshButton";
			this.serverRefreshButton.Size = new System.Drawing.Size(115, 23);
			this.serverRefreshButton.TabIndex = 2;
			this.serverRefreshButton.Text = "Refresh";
			this.serverRefreshButton.UseVisualStyleBackColor = true;
			this.serverRefreshButton.Click += new System.EventHandler(this.serverRefreshButton_Click);
			// 
			// databaseDDL
			// 
			this.databaseDDL.FormattingEnabled = true;
			this.databaseDDL.Location = new System.Drawing.Point(125, 142);
			this.databaseDDL.Name = "databaseDDL";
			this.databaseDDL.Size = new System.Drawing.Size(161, 24);
			this.databaseDDL.TabIndex = 3;
			this.databaseDDL.SelectedIndexChanged += new System.EventHandler(this.databaseDDL_SelectedIndexChanged);
			// 
			// SchemaDDL
			// 
			this.SchemaDDL.FormattingEnabled = true;
			this.SchemaDDL.Location = new System.Drawing.Point(321, 142);
			this.SchemaDDL.Name = "SchemaDDL";
			this.SchemaDDL.Size = new System.Drawing.Size(161, 24);
			this.SchemaDDL.TabIndex = 4;
			this.SchemaDDL.SelectedIndexChanged += new System.EventHandler(this.SchemaDDL_SelectedIndexChanged);
			// 
			// TableDDL
			// 
			this.TableDDL.FormattingEnabled = true;
			this.TableDDL.Location = new System.Drawing.Point(125, 216);
			this.TableDDL.Name = "TableDDL";
			this.TableDDL.Size = new System.Drawing.Size(357, 24);
			this.TableDDL.TabIndex = 5;
			this.TableDDL.SelectedIndexChanged += new System.EventHandler(this.TableDDL_SelectedIndexChanged);
			// 
			// databaseLabel
			// 
			this.databaseLabel.AutoSize = true;
			this.databaseLabel.Location = new System.Drawing.Point(171, 122);
			this.databaseLabel.Name = "databaseLabel";
			this.databaseLabel.Size = new System.Drawing.Size(69, 17);
			this.databaseLabel.TabIndex = 6;
			this.databaseLabel.Text = "Database";
			// 
			// schemaLabel
			// 
			this.schemaLabel.AutoSize = true;
			this.schemaLabel.Location = new System.Drawing.Point(351, 118);
			this.schemaLabel.Name = "schemaLabel";
			this.schemaLabel.Size = new System.Drawing.Size(59, 17);
			this.schemaLabel.TabIndex = 7;
			this.schemaLabel.Text = "Schema";
			// 
			// tableLable
			// 
			this.tableLable.AutoSize = true;
			this.tableLable.Location = new System.Drawing.Point(267, 196);
			this.tableLable.Name = "tableLable";
			this.tableLable.Size = new System.Drawing.Size(44, 17);
			this.tableLable.TabIndex = 8;
			this.tableLable.Text = "Table";
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(237, 333);
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
			this.IdentityInsertCB.Location = new System.Drawing.Point(26, 333);
			this.IdentityInsertCB.Name = "IdentityInsertCB";
			this.IdentityInsertCB.Size = new System.Drawing.Size(114, 21);
			this.IdentityInsertCB.TabIndex = 10;
			this.IdentityInsertCB.Text = "Identity Insert";
			this.IdentityInsertCB.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(589, 525);
			this.Controls.Add(this.IdentityInsertCB);
			this.Controls.Add(this.GenerateButton);
			this.Controls.Add(this.tableLable);
			this.Controls.Add(this.schemaLabel);
			this.Controls.Add(this.databaseLabel);
			this.Controls.Add(this.TableDDL);
			this.Controls.Add(this.SchemaDDL);
			this.Controls.Add(this.databaseDDL);
			this.Controls.Add(this.serverRefreshButton);
			this.Controls.Add(this.serverAddressLabel);
			this.Controls.Add(this.serverAddressTextBox);
			this.Name = "MainForm";
			this.Text = "SQL Data Scripter";
			this.Load += new System.EventHandler(this.Form1_Load);
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
	}
}

