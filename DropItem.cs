using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starvers
{
	public class DropItem
	{
		#region Ctor
		/// <summary>
		/// emmmmm
		/// </summary>
		/// <param name="ids">
		/// 可能掉落的物品的id
		/// </param>
		/// <param name="MinStack">最小数目(包含)</param>
		/// <param name="MaxStack">最大数目(不包含)</param>
		/// <param name="dropchance">掉落几率,用百分数表示</param>
		/// <param name="forevery">是否全员有份</param>
		public DropItem(int[] ids, int MinStack, int MaxStack, float dropchance = 1f, bool forevery = true)
		{
			IDs = ids.Clone() as int[];
			stacks[0] = MinStack;
			stacks[1] = MaxStack;
			chance = dropchance;
			isforevery = forevery;
		}
		#endregion
		#region Drop
		/// <summary>
		/// 掉落
		/// </summary>
		/// <param name="npc">"掉落"物品的npc</param>
		public void Drop(NPC npc)
		{
			if (Starver.Rand.NextDouble() > chance)
			{
				return;
			}
			Vector2 pos = npc.Center + Starver.Rand.NextVector2(npc.width / 2, npc.height / 2);
			int itemid = Starver.Rand.Next(IDs.Length);
			itemid = IDs[itemid];
			int itemstack = Starver.Rand.Next(stacks[0], stacks[1]);
			if (isforevery)
			{
				npc.DropItemForEveryone(npc.Center, itemid, itemstack);
#if DEBUG
				StarverPlayer.All.SendMessage(string.Format("掉落:{0},{1}:[i/s{1}:{0}]", itemid, itemstack), Color.Blue);
#endif
			}

			else
			{
#if DEBUG
				StarverPlayer.All.SendMessage(string.Format("掉落:{0},{1}:[i/s{1}:{0}]", itemid, itemstack), Color.Blue);
#endif
				int idx = Utils.NewItem(pos, itemid, itemstack);
				NetMessage.SendData((int)PacketTypes.ItemDrop, -1, -1, null, idx);
			}

		}
		#endregion
		#region Fields
		private int[] IDs;
		private int[] stacks = new int[2] { 0, 0 };
		private float chance = 1f;
		private bool isforevery = true;
		#endregion
	}
}
