using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starvers.Forms
{
	using AuraSystem.Skills.Base;
	public class ManagerControls : IDisposable
	{
		#region Interfece
		public void Dispose()
		{
			Dispose(true);
		}
		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{

			}
			foreach(var control in Skills)
			{
				control.Dispose();
			}
			Exp.Dispose();
			Level.Dispose();
			TaskNow.Dispose();
		}
		#endregion
		#region Unit
		private class ManagerControl : IDisposable
		{
			#region Interface
			public void Dispose()
			{
				Dispose(true);
			}
			protected virtual void Dispose(bool disposing)
			{
				if (disposing)
				{

				}
				_TextBox.Dispose();
				_Label.Dispose();
			}
			#endregion
			#region Ctor
			public ManagerControl(ManagerControls ManagerControls, Point Loc, string Name, int index = 0, bool numberOnly = false)
			{
				Manager = ManagerControls.Manager;
				int X = Loc.X;
				int Y = Loc.Y;
				_Label = new Label();
				{
					_Label.AutoSize = false;
					_Label.Visible = true;
					_Label.BackColor = Manager.SaveButton.BackColor;
					_Label.Font = Manager.SaveButton.Font;
					_Label.ForeColor = Manager.SaveButton.ForeColor;
					_Label.Size = Manager.SaveButton.Size;
					_Label.TextAlign = ContentAlignment.MiddleCenter;
					_Label.Text = Name;
					if (index == 0)
					{
						_Label.Location = new Point(X, Y + index * Manager.SaveButton.Size.Height);
					}
					else
					{
						_Label.Location = new Point(X, Y + index * Manager.SaveButton.Size.Height + 3 * index);
					}
					_Label.BorderStyle = BorderStyle.Fixed3D;
				}
				_TextBox = new TextBox();
				{
					_TextBox.Multiline = true;
					_TextBox.Visible = true;
					_TextBox.MaxLength = Manager.PlayerSearch.MaxLength;
					_TextBox.Font = Manager.PlayerSearch.Font;
					_TextBox.ForeColor = Manager.PlayerSearch.ForeColor;
					_TextBox.BackColor = Manager.PlayerSearch.BackColor;
					_TextBox.Size = Manager.PlayerSearch.Size;
					_TextBox.TextAlign = HorizontalAlignment.Center;
					_TextBox.BorderStyle = Manager.PlayerSearch.BorderStyle;
					_TextBox.Font = _Label.Font;
					if (index == 0)
					{
						_TextBox.Location = new Point(X + _Label.Width, Y + index * Manager.PlayerSearch.Size.Height);
					}
					else
					{
						_TextBox.Location = new Point(X + _Label.Width, Y + index * Manager.PlayerSearch.Size.Height + 3 * index);
					}
				}
				Index = index;
				if (numberOnly)
				{
					_TextBox.MaxLength = 11;
					_TextBox.KeyPress += OnPress;
				}
				Manager.Controls.Add(_Label);
				Manager.Controls.Add(_TextBox);
				_Label.BringToFront();
				_TextBox.BringToFront();
			}
			#endregion
			#region Properties
			public string Text
			{
				get
				{
					return _TextBox.Text;
				}
				set
				{
					_TextBox.Text = value;
				}
			}
			public int Value
			{
				get
				{
					if (int.TryParse(_TextBox.Text, out int result))
					{
						return result;
					}
					else
					{
						return -1;
					}
				}
			}
			#endregion
			#region Methods
			public void AddToPanel(Panel panel)
			{
				panel.Controls.Add(_Label);
				panel.Controls.Add(_TextBox);
				_Label.Width /= 2;
				_Label.Location = new Point(0, 0 + Index * Manager.SaveButton.Size.Height + 3 * Index);
				_TextBox.Location = new Point(0 + _Label.Width, 0 + Index * Manager.PlayerSearch.Size.Height + 3 * Index);
			}
			#endregion
			#region privates
			private ManagerControl()
			{

			}
			private void OnPress(object sender, KeyPressEventArgs args)
			{
				if(args.KeyChar == 8)
				{
					return;
				}
				if ((args.KeyChar != '-' || _TextBox.Text.Length > 0) && (args.KeyChar < '0' || args.KeyChar > '9'))
				{
					args.Handled = true;
				}
			}
			private int Index;
			private Label _Label;
			private TextBox _TextBox;
			private StarverManagerForm Manager;
			#endregion
		}
		#endregion
		#region Properties
		public StarverPlayer Player
		{
			get
			{
				return player;
			}
			set
			{
				player = value;
				if (player == null)
				{
					for (int i = 0; i < Skills.Length; i++)
					{
						Skills[i].Text = string.Empty;
					}
					for (int i = 0; i < BLDatas.Length; i++)
					{
						BLDatas[i].Text = string.Empty;
					}
					Level.Text = string.Empty;
					Exp.Text = string.Empty;
				}
				else
				{
					for (int i = 0; i < Skills.Length; i++)
					{
						Skills[i].Text = player.GetSkill(i).Name;
					}
					unsafe
					{
						for (int i = 0; i < BLDatas.Length; i++)
						{
							BLDatas[i].Text = player.bldata.buffer[i].ToString();
						}
					}
					Level.Text = player.Level.ToString();
					Exp.Text = player.Exp.ToString();
					TaskNow.Text = StarverConfig.Config.TaskNow.ToString();
				}
			}
		}
		public int[] SkillList
		{
			get
			{
				return player.Skills;
			}
		}
		#endregion
		#region Methods
		#region Ctor
		public ManagerControls(StarverManagerForm manager)
		{
			Manager = manager;
			for (int i = 0; i < Skills.Length; i++)
			{
				Skills[i] = new ManagerControl(this, Manager.Mark_Skill.Location, "技能" + (i + 1), i);
			}
			for (int i = 0; i < BLDatas.Length; i++)
			{
				BLDatas[i] = new ManagerControl(this, manager.Mark_TB.Location, i.ToString(), i, true);
				BLDatas[i].AddToPanel(manager.TBs);
			}
			Level = new ManagerControl(this, manager.Mark_Level.Location, "等级", 0, true);
			Exp = new ManagerControl(this, manager.Mark_Level.Location, "经验", -1, true);
			TaskNow = new ManagerControl(this, manager.Mark_Level.Location, "当前任务", -2, true);
		}
		#endregion
		#region SetDatas
		#region SetDatas
		public void SetDatas()
		{
			SetSkill();
			SetTask();
			SetTB();
			SetOthers();
			player.Save();
		}
		#endregion
		#region SetOthers
		private void SetOthers()
		{
			int value = Level.Value;
			if (value != -1)
			{
				player.Level = value;
			}
			value = Exp.Value;
			if (value != -1)
			{
				player.Exp = value;
			}
		}
		#endregion
		#region SetTask
		private void SetTask()
		{
			int now = TaskNow.Value;
			if (now < 0 || now > TaskSystem.StarverTaskManager.MainLineCount)
			{
				now = 0;
			}
			StarverConfig.Config.TaskNow = now;
			//StarverPlayer.All.SendMessage($"不可抗拒力量把当前已完成任务设置为{now}", Microsoft.Xna.Framework.Color.Blue);
		}
		#endregion
		#region SetSkill
		private void SetSkill()
		{
			for (int i = 0; i < 5; i++)
			{
				player.SetSkill(Skills[i].Text, i + 1, true);
			}
		}
		#endregion
		#region SetTB
		private unsafe void SetTB()
		{
			for (int i = 0; i < BLDatas.Length; i++)
			{
				if (!byte.TryParse(BLDatas[i].Text, out player.bldata.buffer[i]))
				{
					MessageBox.Show($"BLData[{i}] error");
				}
			}
		}
		#endregion
		#endregion
		#endregion
		#region Privates
		private StarverPlayer player;
		private ManagerControl TaskNow;
		private ManagerControl Exp;
		private ManagerControl Level;
		private ManagerControl[] BLDatas = new ManagerControl[StarverPlayer.BLData.Size];
		private ManagerControl[] Skills = new ManagerControl[Skill.MaxSlots];
		private StarverManagerForm Manager;
		#endregion
	}
}
