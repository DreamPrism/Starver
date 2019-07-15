using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.WeaponSystem.Weapons
{
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public abstract class Weapon
	{
		#region Var
		/// <summary>
		/// 对应物品
		/// </summary>
		protected int ItemID;
		/// <summary>
		/// 检测是否为该武器的ID(检测弹幕)
		/// </summary>
		protected int CatchID;
		/// <summary>
		/// 增益ID
		/// </summary>
		protected int ProjID;
		protected int Damage;
		/// <summary>
		/// 职业
		/// </summary>
		public int Career { get; protected set; }
		public int Index { get; private set; }
		#endregion
		#region ctor
		public Weapon(int index,int ItemType,int ProjType,int WhichCareer,int damage)
		{
			Index = index;
			ItemID = ItemType;
			ProjID = ProjType;
			Career = WhichCareer;
			Damage = damage;
		}
		#endregion
		#region Methods
		#region UPGrade
		public void UPGrade(StarverPlayer player)
		{
			if(player.TPlayer.inventory[0].type != ItemID)
			{
				player.SendMessage($"请将[i:{ItemID}]放在背包第一格", Color.Red);
				return;
			}
			ref byte lvl = ref player.Weapon[Career, Index];
			int Need = GetShardNeed(lvl);
			int Finded = 0;
			int i;
			for (i = 1; i < player.TPlayer.inventory.Length; i++)
			{
				if(player.TPlayer.inventory[i].type != Currency.Shards[Career])
				{
					break;
				}
				else
				{
					Finded += player.TPlayer.inventory[i].stack;
				}
			}
			if (Finded < Need)
			{
				player.SendMessage($"请将[i:{Currency.Shards[Career]}] * {Need}排放在第二格开始的格中", Color.Red);
			}
			else
			{
				lvl++;
				player.Save();
				player.SendMessage($"强化成功\n当前等级:{lvl}", Color.DarkGreen); ;
				player.EatItems(1, i + 1);
			}
					

		}
		#endregion
		#region DamageIndex
		protected virtual int CalcDamage(int lvl)
		{
			return (int)(Damage * (1 + 0.1 * lvl));
		}
		#endregion
		#region Check
		public virtual bool Check(TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
		{
			return args.Type == CatchID;
		}
		#endregion
		#region UseWeapon
		public virtual void UseWeapon(StarverPlayer player,Vector Velocity,int lvl, TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
		{
			player.NewProj(player.Center, Velocity, ProjID, Damage, 10f);
		}
		#endregion
		#region GetShardNeed
		public int GetShardNeed(int lvl)
		{
			return lvl * lvl / 3 + 2;
		}
		#endregion
		#region ToString
		public virtual string ToString(int lvl)
		{
			return string.Format("[i:{0}]:{1}\n合成:[i:{0}](当前级别)+[i/p{2}:{3}]=>[i:{0}](下一级)", ItemID, GetType().Name, GetShardNeed(lvl), Currency.Shards[Career]);
		}
		#endregion
		#endregion
	}
}
