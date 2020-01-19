namespace Starvers.Forms
{
	partial class StarverManagerForm
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
			ManagerControls.Dispose();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StarverManagerForm));
			this.PlayerList = new System.Windows.Forms.ListBox();
			this.Updater = new System.Windows.Forms.Timer(this.components);
			this.PlayerListTip = new System.Windows.Forms.Label();
			this.SLT = new System.Windows.Forms.Label();
			this.SaveButton = new System.Windows.Forms.Button();
			this.PlayerSearch = new System.Windows.Forms.TextBox();
			this.Searcher = new System.Windows.Forms.Button();
			this.Mark_Skill_BT = new System.Windows.Forms.Button();
			this.Mark_Skill = new System.Windows.Forms.TextBox();
			this.Mark_TB_BT = new System.Windows.Forms.Button();
			this.Mark_TB = new System.Windows.Forms.TextBox();
			this.Mark_Level_BT = new System.Windows.Forms.Button();
			this.Mark_Level = new System.Windows.Forms.TextBox();
			this.Mark_Skill_End_BT = new System.Windows.Forms.Button();
			this.Mark_Skill_End = new System.Windows.Forms.TextBox();
			this.More = new System.Windows.Forms.Button();
			this.Kicker = new System.Windows.Forms.Button();
			this.Template = new System.Windows.Forms.Button();
			this.StarverManagerTip = new System.Windows.Forms.Label();
			this.TheBorder = new TOFOUT.Windows.Forms.Border();
			this.TBs = new System.Windows.Forms.Panel();
			this.BLDatasTip = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// PlayerList
			// 
			this.PlayerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(82)))), ((int)(((byte)(72)))));
			this.PlayerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.PlayerList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.PlayerList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.PlayerList.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.PlayerList.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.PlayerList.IntegralHeight = false;
			this.PlayerList.ItemHeight = 16;
			this.PlayerList.Location = new System.Drawing.Point(600, 63);
			this.PlayerList.Name = "PlayerList";
			this.PlayerList.Size = new System.Drawing.Size(177, 337);
			this.PlayerList.TabIndex = 0;
			this.PlayerList.SelectedIndexChanged += new System.EventHandler(this.PlayerList_SelectedIndexChanged);
			// 
			// Updater
			// 
			this.Updater.Interval = 5000;
			this.Updater.Tick += new System.EventHandler(this.Updater_Tick);
			// 
			// PlayerListTip
			// 
			this.PlayerListTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(52)))));
			this.PlayerListTip.Font = new System.Drawing.Font("华文楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.PlayerListTip.ForeColor = System.Drawing.Color.Cyan;
			this.PlayerListTip.Location = new System.Drawing.Point(600, 29);
			this.PlayerListTip.Name = "PlayerListTip";
			this.PlayerListTip.Size = new System.Drawing.Size(177, 34);
			this.PlayerListTip.TabIndex = 3;
			this.PlayerListTip.Text = "在线列表";
			this.PlayerListTip.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// SLT
			// 
			this.SLT.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.SLT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(82)))), ((int)(((byte)(72)))));
			this.SLT.Location = new System.Drawing.Point(600, 63);
			this.SLT.Name = "SLT";
			this.SLT.Size = new System.Drawing.Size(177, 16);
			this.SLT.TabIndex = 4;
			this.SLT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SLT.Visible = false;
			// 
			// SaveButton
			// 
			this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.SaveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
			this.SaveButton.FlatAppearance.BorderSize = 3;
			this.SaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveButton.Location = new System.Drawing.Point(492, 364);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(108, 36);
			this.SaveButton.TabIndex = 5;
			this.SaveButton.Text = "保存";
			this.SaveButton.UseVisualStyleBackColor = false;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// PlayerSearch
			// 
			this.PlayerSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(222)))), ((int)(((byte)(252)))));
			this.PlayerSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.PlayerSearch.Font = new System.Drawing.Font("华文行楷", 23.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.PlayerSearch.Location = new System.Drawing.Point(302, 440);
			this.PlayerSearch.MaxLength = 20;
			this.PlayerSearch.Name = "PlayerSearch";
			this.PlayerSearch.Size = new System.Drawing.Size(113, 34);
			this.PlayerSearch.TabIndex = 6;
			this.PlayerSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.PlayerSearch.Visible = false;
			// 
			// Searcher
			// 
			this.Searcher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Searcher.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.Searcher.FlatAppearance.BorderSize = 3;
			this.Searcher.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Searcher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Searcher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Searcher.Location = new System.Drawing.Point(492, 331);
			this.Searcher.Name = "Searcher";
			this.Searcher.Size = new System.Drawing.Size(108, 36);
			this.Searcher.TabIndex = 5;
			this.Searcher.Text = "管理离线";
			this.Searcher.UseVisualStyleBackColor = false;
			this.Searcher.Click += new System.EventHandler(this.OnClickOffline);
			// 
			// Mark_Skill_BT
			// 
			this.Mark_Skill_BT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Skill_BT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
			this.Mark_Skill_BT.FlatAppearance.BorderSize = 3;
			this.Mark_Skill_BT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Skill_BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Skill_BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Mark_Skill_BT.Location = new System.Drawing.Point(170, 59);
			this.Mark_Skill_BT.Name = "Mark_Skill_BT";
			this.Mark_Skill_BT.Size = new System.Drawing.Size(108, 36);
			this.Mark_Skill_BT.TabIndex = 5;
			this.Mark_Skill_BT.Text = "搜索";
			this.Mark_Skill_BT.UseVisualStyleBackColor = false;
			this.Mark_Skill_BT.Visible = false;
			// 
			// Mark_Skill
			// 
			this.Mark_Skill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Skill.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Mark_Skill.Font = new System.Drawing.Font("微软雅黑", 19.9F);
			this.Mark_Skill.Location = new System.Drawing.Point(42, 59);
			this.Mark_Skill.Name = "Mark_Skill";
			this.Mark_Skill.Size = new System.Drawing.Size(128, 36);
			this.Mark_Skill.TabIndex = 6;
			this.Mark_Skill.Visible = false;
			// 
			// Mark_TB_BT
			// 
			this.Mark_TB_BT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_TB_BT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
			this.Mark_TB_BT.FlatAppearance.BorderSize = 3;
			this.Mark_TB_BT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_TB_BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_TB_BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Mark_TB_BT.Location = new System.Drawing.Point(583, 475);
			this.Mark_TB_BT.Name = "Mark_TB_BT";
			this.Mark_TB_BT.Size = new System.Drawing.Size(108, 36);
			this.Mark_TB_BT.TabIndex = 5;
			this.Mark_TB_BT.Text = "搜索";
			this.Mark_TB_BT.UseVisualStyleBackColor = false;
			this.Mark_TB_BT.Visible = false;
			// 
			// Mark_TB
			// 
			this.Mark_TB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_TB.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Mark_TB.Font = new System.Drawing.Font("微软雅黑", 19.9F);
			this.Mark_TB.Location = new System.Drawing.Point(455, 475);
			this.Mark_TB.Name = "Mark_TB";
			this.Mark_TB.Size = new System.Drawing.Size(128, 36);
			this.Mark_TB.TabIndex = 6;
			this.Mark_TB.Visible = false;
			// 
			// Mark_Level_BT
			// 
			this.Mark_Level_BT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Level_BT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
			this.Mark_Level_BT.FlatAppearance.BorderSize = 3;
			this.Mark_Level_BT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Level_BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Level_BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Mark_Level_BT.Location = new System.Drawing.Point(168, 364);
			this.Mark_Level_BT.Name = "Mark_Level_BT";
			this.Mark_Level_BT.Size = new System.Drawing.Size(108, 36);
			this.Mark_Level_BT.TabIndex = 5;
			this.Mark_Level_BT.Text = "搜索";
			this.Mark_Level_BT.UseVisualStyleBackColor = false;
			this.Mark_Level_BT.Visible = false;
			// 
			// Mark_Level
			// 
			this.Mark_Level.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Level.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Mark_Level.Font = new System.Drawing.Font("微软雅黑", 19.9F);
			this.Mark_Level.Location = new System.Drawing.Point(40, 364);
			this.Mark_Level.Name = "Mark_Level";
			this.Mark_Level.Size = new System.Drawing.Size(128, 36);
			this.Mark_Level.TabIndex = 6;
			this.Mark_Level.Visible = false;
			// 
			// Mark_Skill_End_BT
			// 
			this.Mark_Skill_End_BT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Skill_End_BT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
			this.Mark_Skill_End_BT.FlatAppearance.BorderSize = 3;
			this.Mark_Skill_End_BT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Skill_End_BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Mark_Skill_End_BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Mark_Skill_End_BT.Location = new System.Drawing.Point(167, 227);
			this.Mark_Skill_End_BT.Name = "Mark_Skill_End_BT";
			this.Mark_Skill_End_BT.Size = new System.Drawing.Size(108, 36);
			this.Mark_Skill_End_BT.TabIndex = 5;
			this.Mark_Skill_End_BT.Text = "搜索";
			this.Mark_Skill_End_BT.UseVisualStyleBackColor = false;
			this.Mark_Skill_End_BT.Visible = false;
			// 
			// Mark_Skill_End
			// 
			this.Mark_Skill_End.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Mark_Skill_End.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Mark_Skill_End.Font = new System.Drawing.Font("微软雅黑", 19.9F);
			this.Mark_Skill_End.Location = new System.Drawing.Point(39, 227);
			this.Mark_Skill_End.Name = "Mark_Skill_End";
			this.Mark_Skill_End.Size = new System.Drawing.Size(128, 36);
			this.Mark_Skill_End.TabIndex = 6;
			this.Mark_Skill_End.Visible = false;
			// 
			// More
			// 
			this.More.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.More.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.More.FlatAppearance.BorderSize = 3;
			this.More.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.More.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.More.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.More.Location = new System.Drawing.Point(492, 265);
			this.More.Name = "More";
			this.More.Size = new System.Drawing.Size(108, 36);
			this.More.TabIndex = 5;
			this.More.Text = "高级";
			this.More.UseVisualStyleBackColor = false;
			// 
			// Kicker
			// 
			this.Kicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Kicker.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.Kicker.FlatAppearance.BorderSize = 3;
			this.Kicker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Kicker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Kicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Kicker.Location = new System.Drawing.Point(492, 298);
			this.Kicker.Name = "Kicker";
			this.Kicker.Size = new System.Drawing.Size(108, 36);
			this.Kicker.TabIndex = 5;
			this.Kicker.Text = "踢出";
			this.Kicker.UseVisualStyleBackColor = false;
			this.Kicker.Click += new System.EventHandler(this.Kicker_Click);
			// 
			// Template
			// 
			this.Template.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Template.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.Template.FlatAppearance.BorderSize = 3;
			this.Template.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Template.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Template.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Template.Location = new System.Drawing.Point(147, 440);
			this.Template.Name = "Template";
			this.Template.Size = new System.Drawing.Size(108, 36);
			this.Template.TabIndex = 5;
			this.Template.Text = "高级";
			this.Template.UseVisualStyleBackColor = false;
			this.Template.Visible = false;
			// 
			// StarverManagerTip
			// 
			this.StarverManagerTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(52)))));
			this.StarverManagerTip.Font = new System.Drawing.Font("微软雅黑", 12F);
			this.StarverManagerTip.ForeColor = System.Drawing.Color.SkyBlue;
			this.StarverManagerTip.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.StarverManagerTip.Location = new System.Drawing.Point(219, 11);
			this.StarverManagerTip.Name = "StarverManagerTip";
			this.StarverManagerTip.Size = new System.Drawing.Size(345, 32);
			this.StarverManagerTip.TabIndex = 2;
			this.StarverManagerTip.Text = "Starver  管理器";
			this.StarverManagerTip.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.StarverManagerTip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Boreds_MouseDown);
			// 
			// TheBorder
			// 
			this.TheBorder.Alpha = ((byte)(199));
			this.TheBorder.BackColor = System.Drawing.Color.Transparent;
			this.TheBorder.BorderWidth = 29;
			this.TheBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(52)))));
			this.TheBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TheBorder.Location = new System.Drawing.Point(0, 0);
			this.TheBorder.Name = "TheBorder";
			this.TheBorder.Size = new System.Drawing.Size(801, 426);
			this.TheBorder.TabIndex = 7;
			this.TheBorder.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.TheBorder.Title = "";
			this.TheBorder.TitleColor = System.Drawing.SystemColors.ControlText;
			this.TheBorder.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			// 
			// TBs
			// 
			this.TBs.AutoScroll = true;
			this.TBs.BackColor = System.Drawing.Color.Transparent;
			this.TBs.Location = new System.Drawing.Point(302, 94);
			this.TBs.Name = "TBs";
			this.TBs.Size = new System.Drawing.Size(190, 292);
			this.TBs.TabIndex = 8;
			// 
			// BLDatasTip
			// 
			this.BLDatasTip.BackColor = System.Drawing.Color.Transparent;
			this.BLDatasTip.Font = new System.Drawing.Font("华文新魏", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BLDatasTip.ForeColor = System.Drawing.Color.Cyan;
			this.BLDatasTip.Location = new System.Drawing.Point(302, 45);
			this.BLDatasTip.Name = "BLDatasTip";
			this.BLDatasTip.Size = new System.Drawing.Size(190, 46);
			this.BLDatasTip.TabIndex = 3;
			this.BLDatasTip.Text = "BLDatas";
			this.BLDatasTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// StarverManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(82)))), ((int)(((byte)(72)))));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(801, 426);
			this.Controls.Add(this.TBs);
			this.Controls.Add(this.Mark_Level);
			this.Controls.Add(this.Mark_Level_BT);
			this.Controls.Add(this.Mark_TB);
			this.Controls.Add(this.Mark_TB_BT);
			this.Controls.Add(this.Mark_Skill_End);
			this.Controls.Add(this.Mark_Skill_End_BT);
			this.Controls.Add(this.Mark_Skill);
			this.Controls.Add(this.Mark_Skill_BT);
			this.Controls.Add(this.PlayerSearch);
			this.Controls.Add(this.Kicker);
			this.Controls.Add(this.Template);
			this.Controls.Add(this.More);
			this.Controls.Add(this.Searcher);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.SLT);
			this.Controls.Add(this.BLDatasTip);
			this.Controls.Add(this.PlayerListTip);
			this.Controls.Add(this.PlayerList);
			this.Controls.Add(this.StarverManagerTip);
			this.Controls.Add(this.TheBorder);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "StarverManagerForm";
			this.Text = "Starver管理器";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer Updater;
		internal System.Windows.Forms.ListBox PlayerList;
		private System.Windows.Forms.Label PlayerListTip;
		internal System.Windows.Forms.Label SLT;
		private System.Windows.Forms.Button Searcher;
		internal System.Windows.Forms.Button SaveButton;
		internal System.Windows.Forms.TextBox PlayerSearch;
		private System.Windows.Forms.Button Mark_Skill_BT;
		internal System.Windows.Forms.TextBox Mark_Skill;
		internal System.Windows.Forms.TextBox Mark_TB;
		private System.Windows.Forms.Button Mark_Level_BT;
		internal System.Windows.Forms.TextBox Mark_Level;
		private System.Windows.Forms.Button Mark_Skill_End_BT;
		internal System.Windows.Forms.TextBox Mark_Skill_End;
		private System.Windows.Forms.Button More;
		private System.Windows.Forms.Button Kicker;
		private System.Windows.Forms.Button Template;
		private System.Windows.Forms.Label StarverManagerTip;
		internal TOFOUT.Windows.Forms.Border TheBorder;
		protected internal System.Windows.Forms.Panel TBs;
		private System.Windows.Forms.Button Mark_TB_BT;
		private System.Windows.Forms.Label BLDatasTip;
	}
}