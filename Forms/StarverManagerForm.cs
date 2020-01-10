using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starvers.Forms
{
	public partial class StarverManagerForm : Form
	{
		#region F&P&E
		public class PSChangeArgs : EventArgs
		{
			public StarverPlayer player;
			public StarverManagerForm Form;
		}
		public event EventHandler<PSChangeArgs> PSChange;
		public static StarverPlayer PlayerSelected { get; internal set; }
		internal ManagerControls ManagerControls;
		private int SLTX;
		private int SLTY;
		private Graphics Painter;
		private int LastSelected = 0;
		private SolidBrush Brush_Font;
		private SolidBrush Brush_Selected;
		private Rectangle Rec_Border;
		private Pen Border_Pen = new Pen(Color.FromArgb(30, 30, 30), 88);
		private readonly StringFormat PlyListFmt = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		#endregion
		#region Ctor OnClosed
		public StarverManagerForm()
		{
			InitializeComponent();
			TheBorder.SendToBack();
			Brush_Selected = new SolidBrush(Color.DarkGray);
			Brush_Font = new SolidBrush(PlayerList.ForeColor);
			Paint += OnPaint;
			PlayerList.DrawItem += OnDrawList;
			PSChange += UpdateInfosInUI;
			Rec_Border = new Rectangle(Location, Size);
			Painter = CreateGraphics();
			SLTX = PlayerList.Location.X;
			SLTY = PlayerList.Location.Y;
			string str;
			for (int i = 0; i < 21; i++)
			{
				try
				{
					str = Starver.Players[i].Name;
				}
				catch
				{
					str = string.Empty;
				}
				PlayerList.Items.Insert(i, str);
			}
			ManagerControls = new ManagerControls(this);
		}
		#endregion
		#region Events
		#region OnClickOffline
		private void OnClickOffline(object sender,EventArgs args)
		{
			string Name = TOFOUT.Windows.Forms.InputBox.Show("输入玩家名称", "请输入要寻找的玩家名称");
			StarverPlayer.TryGetTempPlayer(Name, out StarverPlayer player);
			if(player is null)
			{
				MessageBox.Show("不存在的玩家", "提示");
			}
			else
			{
				PlayerSelected = player;
				PSChange(this, new PSChangeArgs() { player = PlayerSelected, Form = this });
			}
		}
		#endregion
		#region OnPaint
		private void OnPaint(object sender, PaintEventArgs args)
		{
			//args.Graphics.DrawRectangle(Border_Pen, Rec_Border);
		}
		private void OnDrawList(object sender, DrawItemEventArgs args)
		{
			args.Graphics.DrawString(PlayerList.Items[args.Index] as string, PlayerList.Font, Brush_Font, args.Bounds, PlyListFmt);
		}
		#endregion
		#region OnSelect
		private void PlayerList_SelectedIndexChanged(object sender, EventArgs args)
		{
			if (PlayerList.SelectedIndex == -1)
			{
				SLT.Visible = false;
				PlayerSelected = null;
				PSChange(this, new PSChangeArgs() { player = PlayerSelected, Form = this });
				return;
			}
			else if((PlayerList.Items[PlayerList.SelectedIndex] as string).Length == 0)
			{
				SLT.Visible = true;
				SLT.Location = new Point(SLTX, SLTY + PlayerList.ItemHeight * PlayerList.SelectedIndex);
				SLT.Text = string.Empty;
				PlayerSelected = null;
			}
			try
			{
				PlayerSelected = Starver.Players[PlayerList.SelectedIndex];
				SLT.Visible = true;
				SLT.Location = new Point(SLTX, SLTY + PlayerList.ItemHeight * PlayerList.SelectedIndex);
				SLT.Text = PlayerSelected.Name;
				LastSelected = PlayerList.SelectedIndex;
#if DEBUG
				Console.WriteLine("Selected:{0}", PlayerList.SelectedItem);
#endif
				PSChange(this, new PSChangeArgs() { player = PlayerSelected, Form = this });
				return;
			}
			catch(Exception)
			{
				//Console.WriteLine(e);
			}
		}
		#endregion
		#region UPdateAll
		private void UpdateAll()
		{
			PlayerList.Items.Clear();
			for (int i = 0; i < 15; i++)
			{
				PlayerList.Items.Insert(i, Starver.Players[i].Name ?? string.Empty);
			}
		}
		#endregion
		#region Timer
		private void Updater_Tick(object sender, EventArgs e)
		{
			UpdateInfosInUI(this, new PSChangeArgs() { player = PlayerSelected, Form = this });
		}
		#endregion
		#region Exiting
		private void Exciting_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
		#region OnMoused
		#region Down
		private void Boreds_MouseDown(object sender, MouseEventArgs e)
		{
			//LastMouse = e.Location;
			ReleaseCapture();
			SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
		}
		#endregion
		#endregion
		#region Save
		private void SaveButton_Click(object sender, EventArgs args)
		{
			if (PlayerList.SelectedIndex == -1 || PlayerSelected == null)
			{
				return;
			}
			ManagerControls.SetDatas();
		}
		#endregion
		#region Kick
		private unsafe void Kicker_Click(object sender, EventArgs e)
		{
			try
			{
				if (PlayerSelected.Skills != null)
				{
					PlayerSelected.Kick("");
				}
			}
			catch
			{

			}
		}
		#endregion
		#region Load
		private void OnLoad(object sender,EventArgs args)
		{
			Refresh();
		}
		#endregion
		#endregion
		#region Method
		#region UpdateUI
		private void UpdateInfosInUI(object sender, PSChangeArgs args)
		{
			ManagerControls.Player = args.player;
		}
		#endregion
		#region DrawListLineColor
		private void DrawListLineColor(int line)
		{
			Painter.DrawString(PlayerList.Items[line] as string, PlayerList.Font, Brush_Selected, PlayerList.GetItemRectangle(line), PlyListFmt);
			line = LastSelected;
			Painter.DrawString(PlayerList.Items[line] as string, PlayerList.Font, Brush_Font, PlayerList.GetItemRectangle(line), PlyListFmt);
		}
		#endregion
		#region WndProc
		/*
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x4e:
				case 0xd:
				case 0xe:
				case 0x14:
					base.WndProc(ref m);
					break;
				case 0x84://鼠标点任意位置后可以拖动窗体
					DefWndProc(ref m);
					if (m.Result.ToInt32() == 0x01)
					{
						m.Result = new IntPtr(0x02);
					}
					break;
				case 0xA3://禁止双击最大化
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
		*/
		#endregion
		#region SendMessage
		[DllImport("user32.dll")]
		private extern static bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
		[DllImport("user32.dll")]
		private extern static bool ReleaseCapture();
		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MOVE = 0xF010;
		private const int HTCAPTION = 0x0002;
		#endregion
		#endregion
	}
}
