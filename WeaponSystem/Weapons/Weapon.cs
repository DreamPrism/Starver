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
		public virtual bool IsThis(int idx)
		{
			Terraria.Projectile proj = Terraria.Main.projectile[idx];
			return proj.type == CatchID;
		}
		public virtual void UseWeapon(StarverPlayer player,Vector Velocity)
		{
			player.NewProj(player.Center, Velocity, ProjID, Damage, 10f);
		}
		#endregion
	}
}
