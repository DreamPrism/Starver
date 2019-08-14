using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starvers.Forms
{
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
			foreach(var control in TBCodes)
			{
				control.Dispose();
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
			#region ctor
			public ManagerControl(ManagerControls ManagerControls, Point Loc, string Name, int index = 0, bool numonly = false)
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
				if (numonly)
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
				if(player != null && player.Temp)
				{
					player.Dispose();
				}
				player = value;
				if (player == null)
				{
					for (int i = 0; i < 5; i++)
					{
						Skills[i].Text = string.Empty;
						if (i < 4)
						{
							TBCodes[i].Text = string.Empty;
						}
					}
					Level.Text = string.Empty;
					Exp.Text = string.Empty;
				}
				else
				{
					unsafe
					{
						for (int i = 0; i < 5; i++)
						{
							Skills[i].Text = AuraSystem.SkillManager.Skills[SkillList[i]].Name;
							if (i < 4)
							{
								TBCodes[i].Text = TBStates[i].ToString();
							}
						}
					}
					Level.Text = player.Level.ToString();
					Exp.Text = player.Exp.ToString();
					TaskNow.Text = StarverConfig.Config.TaskNow.ToString();
				}
			}
		}
		public unsafe int* SkillList
		{
			get
			{
				return player.Skills;
			}
		}
		public int[] TBStates
		{
			get
			{
				return player.TBCodes;
			}
		}
		#endregion
		#region Methods
		#region ctor
		public ManagerControls(StarverManagerForm manager)
		{
			Manager = manager;
			int i;
			for (i = 0; i < 5; i++)
			{
				Skills[i] = new ManagerControl(this, Manager.Mark_Skill.Location, "技能" + (i + 1), i);
			}
			for (i = 0; i < 4; i++)
			{
				TBCodes[i] = new ManagerControl(this, Manager.Mark_TB.Location, TBNames[i], i, true);
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
			SetTB();
			SetSkill();
			SetTask();
			SetOthers();
			player.Save();
		}
		#endregion
		#region SetOthers
		public void SetOthers()
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
		public void SetTask()
		{
			int now = TaskNow.Value;
			if (now < 0 || now > TaskSystem.StarverTask.MAINLINE)
			{
				now = 0;
			}
			StarverConfig.Config.TaskNow = now;
			//StarverPlayer.All.SendMessage($"不可抗拒力量把当前已完成任务设置为{now}", Microsoft.Xna.Framework.Color.Blue);
		}
		#endregion
		#region SetTB
		public void SetTB()
		{
			int value;
			for (int i = 0; i < 4; i++)
			{
				value = TBCodes[i].Value;
				if (value != -1)
				{
					TBStates[i] = value;
				}
			}
		}
		#endregion
		#region SetSkill
		public void SetSkill()
		{
			for (int i = 0; i < 5; i++)
			{
				player.SetSkill(Skills[i].Text, i + 1, true);
			}
		}
		#endregion
		#endregion
		#endregion
		#region Fake_ctor
		private ManagerControls()
		{

		}
		#endregion
		#region Privates
		private static string[] TBNames =
		{
			"钓鱼任务",
			"挖矿任务",
			"收集任务",
			"战斗任务"
		};
		private StarverPlayer player;
		private ManagerControl[] TBCodes = new ManagerControl[4];
		private ManagerControl TaskNow;
		private ManagerControl Exp;
		private ManagerControl Level;
		private ManagerControl[,] Weapons = new ManagerControl[5, 4];
		private ManagerControl[] Skills = new ManagerControl[5];
		private StarverManagerForm Manager;
		#endregion
	}
}
