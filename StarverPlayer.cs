using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using TShockAPI;
using MySql;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria.ID;
using System.Runtime.InteropServices;

namespace Starvers
{
	using Starvers.DB;
	using Terraria.Localization;
	using BigInt = System.Numerics.BigInteger;
	using Vector = TOFOUT.Terraria.Server.Vector2;
	public class StarverPlayer : IDisposable
	{
		#region FromTS
		#region Heal
		public void Heal(int health = -1)
		{
			if (health == -1)
			{
				health = TPlayer.statLifeMax2;
			}
			TSPlayer.Heal(health);
		}
		#endregion
		#region GodMode
		public bool GodMode
		{
			get
			{
				return TSPlayer.GodMode;
			}
			set
			{
				TSPlayer.GodMode = value;
			}
		}
		#endregion
		#region SetBuff
		public void SetBuff(int type,int time = 3600,bool bypass = false)
		{
			TSPlayer.SetBuff(type, time, bypass);
		}
		#endregion
		#region Sends
		/// <summary>
		/// 发送悬浮文字
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="color"></param>
		public void SendCombatMSsg(string msg, Color color)
		{
			if (Index == -2)
			{
				Console.WriteLine(msg);
			}
			else if (Index >= 0)
			{
				TPlayer.SendCombatMsg(msg, color);
			}
		}
		public void SendMessage(string msg,byte R,byte G,byte B)
		{
			if (Index == -2)
			{
				Console.WriteLine(msg);
			}
			else if (Index >= -1)
			{
				if (msg.Contains("\n"))
				{
					foreach (string msg2 in msg.Split(new char[]
					{
					'\n'
					}))
					{
						SendMessage(msg2, R, G, B);
					}
					return;
				}
				this.SendData(PacketTypes.SmartTextMessage, msg, 255, R, G, B, -1);
			}
			else
			{
				TSPlayer.SendInfoMessage(msg);
			}
		}
		public void SendDeBugMessage(string msg)
		{
			SendMessage(msg, Color.Blue);
		}
		public void SendMessage(string msg,Color color)
		{
			if (Index == -2)
			{
				Console.WriteLine(msg);
			}
			else if (Index >= -1)
			{
				SendMessage(msg, color.R, color.G, color.B);
			}
			else
			{
				TSPlayer.SendMessage(msg,color);
			}
		}
		public void SendInfoMessage(string msg)
		{
			if (Index == -2)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(msg);
				Console.ResetColor();
			}
			else if (Index >= -1)
			{
				SendMessage(msg, Color.Yellow);
			}
			else
			{
				TSPlayer.SendInfoMessage(msg);
			}
		}
		public void SendInfoMessage(string msg, params object[] args)
		{
			if (Index == -2)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(msg, args);
				Console.ResetColor();
			}
			else if (Index >= -1)
			{
				SendInfoMessage(string.Format(msg, args));
			}
			else
			{
				TSPlayer.SendInfoMessage(msg, args);
			}
		}
		public void SendData(PacketTypes msgType, string text = "", int number = 0, float number2 = 0, float number3 = 0, float number4 = 0, int number5 = 0)
		{
			NetMessage.SendData((int)msgType, Index, -1, NetworkText.FromLiteral(text), number, number2, number3, number4, number5);
		}
		public void SendStatusMSG(string msg)
		{
			for (int index = 1; index <= 19; ++index)
				msg = "\n" + msg;
			for (int index = 1; index <= 20; ++index)
				msg += "\n";
			SendData(PacketTypes.Status, msg, 0, 0.0f, 0.0f, 0.0f, 0);
		}
		#endregion
		#region HasPerm
		public bool HasPerm(string perm)
		{
			return TSPlayer.HasPermission(perm);
		}
		#endregion
		#endregion
		#region FromTPlayer
		#region Center
		public Vector2 Center
		{
			get
			{
				return TPlayer.Center;
			}
			set
			{
				TPlayer.Center = value;
			}
		}
		#endregion
		#region Position
		public Vector2 Position
		{
			get
			{
				return TPlayer.position;
			}
			set
			{
				TPlayer.position = value;
			}
		}
		#endregion
		#region Velocity
		public Vector2 Velocity
		{
			get
			{
				return TPlayer.velocity;
			}
			set
			{
				TPlayer.velocity = value;
			}
		}
		#endregion
		#endregion
		#region Methods
		#region GetBiomes
		public NPCSystem.BiomeType GetBiomes()
		{
			NPCSystem.BiomeType biome = default;
			bool Grass = true;
			#region Zones
			if (TPlayer.ZoneDesert)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Dessert;
			}
			if(TPlayer.ZoneHoly)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Holy;
			}
			if(TPlayer.ZoneCorrupt)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Corrupt;
			}
			if(TPlayer.ZoneCrimson)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Crimson;
			}
			if(TPlayer.ZoneRockLayerHeight || TPlayer.ZoneDirtLayerHeight)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.UnderGround;
			}
			if(TPlayer.ZoneJungle)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Jungle;
			}
			if(TPlayer.ZoneSnow)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Icy;
			}
			if(TPlayer.ZoneMeteor)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Metor;
			}
			if(TPlayer.ZoneRain)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Rain;
			}
			if(TPlayer.ZoneUnderworldHeight)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.Hell;
			}
			if(TPlayer.ZoneTowerSolar)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.TowerSolar;
			}
			if(TPlayer.ZoneTowerNebula)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.TowerNebula;
			}
			if(TPlayer.ZoneTowerStardust)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.TowerStardust;
			}
			if(TPlayer.ZoneTowerVortex)
			{
				Grass = false;
				biome |= NPCSystem.BiomeType.TowerVortex;
			}
			if(TPlayer.ZoneSkyHeight)
			{
				biome |= NPCSystem.BiomeType.Sky;
			}
			if(TPlayer.ZoneBeach)
			{
				biome |= NPCSystem.BiomeType.Beach;
			}
			if(Grass)
			{
				biome |= NPCSystem.BiomeType.Grass;
			}
			#endregion
			return biome;
		}
		#endregion
		#region GetConditions
		public NPCSystem.SpawnConditions GetConditions()
		{
			NPCSystem.SpawnConditions condition = default;
			if(Main.dayTime)
			{
				condition |= NPCSystem.SpawnConditions.Day;
				if(Main.eclipse)
				{
					condition |= NPCSystem.SpawnConditions.Eclipse;
				}
			}
			else
			{
				condition |= NPCSystem.SpawnConditions.Night;
				if(Main.bloodMoon)
				{
					condition |= NPCSystem.SpawnConditions.BloodMoon;
				}
			}
			return condition;
		}
		#endregion
		#region GetSpawnChecker
		public NPCSystem.SpawnChecker GetSpawnChecker()
		{
			return new NPCSystem.SpawnChecker() { Biome = GetBiomes(), Condition = GetConditions() };
		}
		#endregion
		#region EatItems
		/// <summary>
		/// 吃掉玩家背包里从begin起不包括end的物品
		/// </summary>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		public void EatItems(int begin,int end)
		{
			for(;begin < end;begin++)
			{
				TPlayer.inventory[begin].netDefaults(0);
				NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, null, Index, begin);
			}
		}
		#endregion
		#region HasWeapon
		public bool HasWeapon(WeaponSystem.Weapons.Weapon weapon)
		{
			return Weapon[weapon.Career, weapon.Index] > 0;
		}
		#endregion
		#region UpdateMoon
		public void UpdateMoon()
		{
			if(Dead)
			{
				if (MoonIndex > 0)
				{
					Main.npc[MoonIndex].active = false;
				}
				return;
			}
			if(MoonIndex < 0 ) 
			{
				MoonIndex = NewMoon();
			}
			else if(
				Main.npc[MoonIndex].type == NPCID.MoonLordCore &&
				Main.npc[MoonIndex].active == false)
			{
				Main.npc[MoonIndex].active = true;
				Main.npc[MoonIndex].SetDefaults(NPCID.MoonLordCore);
			}
			Main.npc[MoonIndex].type = NPCID.MoonLordCore;
			Main.npc[MoonIndex].aiStyle = -1;
			Main.npc[MoonIndex].Center = Center;
			NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, MoonIndex);
		}
		public void UpdateMoonClear()
		{
			if (MoonIndex < 0)
			{
				return;
			}
			else if (
				Main.npc[MoonIndex].type == NPCID.MoonLordCore &&
				Main.npc[MoonIndex].active != false &&
				Main.npc[MoonIndex].aiStyle == -1)
			{
				Main.npc[MoonIndex].active = false;
			}
			Main.npc[MoonIndex].type = 0;
			Main.npc[MoonIndex].aiStyle = -1;
			Main.npc[MoonIndex].Center = Center;
			NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, MoonIndex);
		}
		#endregion
		#region UPGrade
		/// <summary>
		/// 消耗一定经验升级
		/// </summary>
		/// <param name="ExpGet"></param>
		public void UPGrade(BigInt ExpGet)
		{
			int lvl = level;
			ExpGet += exp;
			int need = AuraSystem.StarverAuraManager.UpGradeExp(lvl);
			if (HasPerm(Perms.VIP.LessCost))
			{
				need /= 3;
			}
			while (ExpGet > need)
			{
				ExpGet -= need;
				++lvl;
				need = AuraSystem.StarverAuraManager.UpGradeExp(lvl);
				if (HasPerm(Perms.VIP.LessCost))
				{
					need /= 3;
				}
			}
			Level = lvl;
			exp = (int)ExpGet;
		}
		#endregion
		#region DataOperations
		#region Read
		/// <summary>
		/// 读取数据(仅限MySQL)
		/// </summary>
		/// <param name="UserID">玩家ID</param>
		/// <returns></returns>
		public unsafe static StarverPlayer Read(int UserID)
		{
			StarverPlayer player = new StarverPlayer(UserID);
			if (SaveMode == SaveModes.MySQL)
			{
				MySqlDataReader result = db.QueryReader("SELECT * FROM Starver WHERE UserID=@0;", UserID);
				if (result.Read())
				{
					player.Weapon = JsonConvert.DeserializeObject<byte[,]>(result.GetString("Weapons"));
					buffer = (byte[])result.GetValue(result.GetOrdinal("Skills"));
					player.BufferToSkill();
					player.TBCodes = JsonConvert.DeserializeObject<int[]>(result.GetString("TBCodes"));
					player.level = result.GetInt32("Level");
					player.Exp = result.GetInt32("Exp");
				}
				else
				{
					TSPlayer.Server.SendInfoMessage("StarverPlugins: 玩家{0}不存在,已新建", TShock.Users.GetUserByID(UserID).Name);
					AddNewUser(player);
				}
				result.Dispose();
			}
			else
			{
				throw new Exception("非MySQL存储模式下不能通过UserID读取玩家数据");
			}
			player.MP = (player.MaxMP = player.level / 3 + 100) / 2;
			return player;
		}
		public static StarverPlayer Read(string Name, bool suffixed = false)
		{
			StarverPlayer player = new StarverPlayer();
			string tmp = SavePath + "//" + Name;
			if (!suffixed)
			{
				tmp += ".json";
			}
			if (File.Exists(tmp))
			{
				player = JsonConvert.DeserializeObject<StarverPlayer>(File.ReadAllText(tmp));
#if DEBUG
				TSPlayer.Server.SendInfoMessage("Name:{0}", player.Name);
#endif
			}
			else
			{
				player.Name = Name;
			}
			if (!suffixed)
			{
				player.Name = Name;
			}
			if (player.Buffer != null)
			{
				buffer = Encoding.Default.GetBytes(player.Buffer);
				player.BufferToSkill();
			}
			else
			{
				player.InitializeSkills();
			}
			player.MP = (player.MaxMP = player.level / 3 + 100) / 2;
			return player;
		}
		#endregion
		#region Add
		/// <summary>
		/// 添加用户
		/// </summary>
		/// <param name="player"></param>
		public unsafe static void AddNewUser(StarverPlayer player)
		{
			int UserID = player.UserID;
			int Level = player.Level;
			int Exp = player.Exp;
			string Weapon = JsonConvert.SerializeObject(player.Weapon);
			int* begin = player.Skills;
			int* end = begin + 5;
			while(begin != end)
			{
				*begin++ = 0;
			}
			player.SkillToBuffer();
			string TBCodes = JsonConvert.SerializeObject(player.TBCodes);
			db.Excute("INSERT INTO Starver (UserId, Weapons,Skills,Level,Exp,TBCodes) VALUES ( @0 ,@1, @2, @3, @4, @5);", UserID, Weapon, buffer, Level, Exp, TBCodes);
		}
		#endregion
		#region Save
		/// <summary>
		/// 保存
		/// </summary>
		public void Save()
		{
			if (UserID == -1)
			{
				return;
			}
			SkillToBuffer();
			if (SaveMode == SaveModes.MySQL)
			{
				string _Weapon = JsonConvert.SerializeObject(Weapon);
				string _TBCodes = JsonConvert.SerializeObject(TBCodes);
				db.Excute("UPDATE Starver SET Weapons=@0 WHERE UserID=@1;", _Weapon, UserID);
				db.Excute("UPDATE Starver SET Skills=@0 WHERE UserID=@1;", buffer, UserID);
				db.Excute("UPDATE Starver SET TBCodes=@0 WHERE UserID=@1;", _TBCodes, UserID);
				db.Excute("UPDATE Starver SET Level=@0 WHERE UserID=@1;", level, UserID);
				db.Excute("UPDATE Starver SET Exp=@0 WHERE UserID=@1;", Exp, UserID);
			}
			else
			{
				Buffer = Encoding.Default.GetString(buffer);
				File.WriteAllText(SavePath + "//" + Name + ".json", JsonConvert.SerializeObject(this, Formatting.Indented));
			}
		}
		#endregion
		#region Reload
		/// <summary>
		/// 重新加载
		/// </summary>
		public void Reload()
		{
			StarverPlayer tempplayer;
			if (SaveMode == SaveModes.MySQL)
			{
				tempplayer = Read(UserID);
				tempplayer.SkillToBuffer();
			}
			else
			{
				tempplayer = Read(Name);
			}
			level = tempplayer.level;
			BufferToSkill();
			Exp = tempplayer.Exp;
			TBCodes = tempplayer.TBCodes;
			Weapon = tempplayer.Weapon;
			tempplayer.Dispose();
			Save()
		}
		#endregion
		#endregion
		#region SetLifeMax
		public void SetLifeMax()
		{
			if (TPlayer.statLifeMax2 < 500)
			{
				return;
			}
			int Life = 500 + Utils.CalculateLife(level);
			TPlayer.SetLife(Life);
		}
		#endregion
		#region Projs
		#region NewByPolar
		/// <summary>
		/// 极坐标获取角度
		/// </summary>
		/// <param name="rad">所需角度(弧度)</param>
		/// <param name="length"></param>
		/// <returns></returns>
		public Vector NewByPolar(double rad,float length)
		{
			return Vector.NewByPolar(rad, length);
		}
		#endregion
		#region NewProj
		/// <summary>
		/// 生成弹幕
		/// </summary>
		/// <param name="position"></param>
		/// <param name="velocity"></param>
		/// <param name="Type"></param>
		/// <param name="Damage"></param>
		/// <param name="KnockBack"></param>
		/// <param name="ai0"></param>
		/// <param name="ai1"></param>
		/// <returns></returns>
		public int NewProj(Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, float ai0 = 0, float ai1 = 0)
		{
			Damage = Math.Max(1, Math.Min(30000, Damage));
			int idx = Projectile.NewProjectile(position, velocity, Type, Damage, KnockBack, Index, ai0, ai1);
			if(Type == ProjectileID.ToxicCloud)
			{
				Main.projectile[idx].timeLeft = 60 * 10;
			}
			NetMessage.SendData((int)PacketTypes.ProjectileNew, -1, -1, null, idx);
			return idx;
		}
		#endregion
		#region ProjCircle
		/// <summary>
		/// 弹幕圆
		/// </summary>
		/// <param name="Center"></param>
		/// <param name="r"></param>
		/// <param name="Vel">速率</param>
		/// <param name="Type"></param>
		/// <param name="number">弹幕总数</param>
		/// <param name="direction">0:不动 1:向内 2:向外</param>
		public void ProjCircle(Vector2 Center, int r, float Vel, int Type, int number, int Damage, byte direction = 1, float ai0 = 0, float ai1 = 0)
		{
			switch (direction)
			{
				case 0:
					Vel = 0;
					break;
				case 1:
					Vel *= 1;
					break;
				case 2:
					Vel *= -1;
					break;
			}
			double averagerad = Math.PI * 2 / number;
			for (int i = 0; i < number; i++)
			{
				NewProj(Center + NewByPolar(averagerad * i, r) , NewByPolar(averagerad * i, -Vel) , Type, Damage, 4f, ai0, ai1);
			}
		}
		#endregion
		#region ProjSector
		/// <summary>
		/// 扇形弹幕
		/// </summary>
		/// <param name="Center">圆心</param>
		/// <param name="Vel">速率</param>
		/// <param name="r">半径</param>
		/// <param name="interrad">中心半径的方向</param>
		/// <param name="rad">张角</param>
		/// <param name="Damage">伤害(带加成)</param>
		/// <param name="Type"></param>
		/// <param name="num">数量</param>
		/// <param name="direction">0:不动 1:向内 2:向外</param>
		/// <param name="ai0"></param>
		/// <param name="ai1"></param>
		public void ProjSector(Vector2 Center, float Vel, float r, double interrad, double rad, int Damage, int Type, int num, byte direction = 2, float ai0 = 0, float ai1 = 0)
		{
			double start = interrad - rad / 2;
			double average = rad / num;
			switch (direction)
			{
				case 0:
					Vel *= 0;
					break;
				case 1:
					Vel *= -1;
					break;
				case 2:
					Vel *= 1;
					break;
			}
			for (int i = 0; i < num; i++)
			{
				NewProj(Center + NewByPolar(start + i * average, r) , NewByPolar(start + i * average, Vel) , Type, Damage, 4f, ai0, ai1);
			}
		}
		#endregion
		#region ProjLine
		/// <summary>
		/// 制造速度平行的弹幕直线
		/// </summary>
		/// <param name="Begin">起点</param>
		/// <param name="End">终点</param>
		/// <param name="Vel">速度</param>
		/// <param name="num">数量</param>
		/// <param name="Damage"></param>
		/// <param name="type"></param>
		/// <param name="ai0"></param>
		/// <param name="ai1"></param>
		public void ProjLine(Vector2 Begin, Vector2 End, Vector2 Vel, int num, int Damage, int type, float ai0 = 0, float ai1 = 0)
		{
			Vector2 average = End - Begin;
			average /= num;
			for (int i = 0; i < num; i++)
			{
				NewProj(Begin + average * i, Vel, type, Damage, 3f, ai0, ai1);
			}
		}
		#endregion
		#endregion
		#region Operator
		public static implicit operator TSPlayer(StarverPlayer player)
		{
			return player.TSPlayer;
		}
		public static implicit operator Player(StarverPlayer player)
		{
			return player.TPlayer;
		}
		public static implicit operator int(StarverPlayer player)
		{
			return player.Index;
		}
		#endregion
		#region SetSkill
		/// <summary>
		/// 设置技能
		/// </summary>
		/// <param name="name">技能名称(可以只取前几位字母)</param>
		/// <param name="slot">槽位</param>
		/// <param name="ServerDoThis">是否无视等级设置</param>
		public unsafe void SetSkill(string name, int slot,bool ServerDoThis = false)
		{
			slot -= 1;
			if (slot < 0 || slot > AuraSystem.SkillManager.Skills.Length)
			{
				TSPlayer.SendErrorMessage("槽位错误!");
			}
			else
			{
				name = name.ToLower();
				foreach (var skill in AuraSystem.SkillManager.Skills)
				{
					if (skill.Name.ToLower().IndexOf(name) == 0)
					{
						if (level < skill.Lvl && !ServerDoThis)
						{
							TSPlayer.SendErrorMessage("你的等级不足");
						}
						else
						{
							Skills[slot] = skill.Index;
							if(!ServerDoThis)
							{
								TSPlayer.SendSuccessMessage("设置成功");
							}
						}
						if(!ServerDoThis)
						{
							TSPlayer.SendInfoMessage(skill.Text);
						}
						return;
					}
				}
				if (!ServerDoThis)
				{
					TSPlayer.SendErrorMessage("错误的技能名");
					TSPlayer.SendErrorMessage("list:    查看技能列表");
				}
			}
		}
		#endregion
		#region Damage
		public void Damage(int damage)
		{
			NetMessage.SendPlayerHurt(Index, PlayerDeathReason.LegacyDefault(), damage, Index, false, false, 0);
		}
		#endregion
		#region ShowInfos
		/// <summary>
		/// 调试使用
		/// </summary>
		/// <param name="ToWho">展示信息给谁</param>
		public void ShowInfos(TSPlayer ToWho = null)
		{
			ToWho = ToWho == null ? TSPlayer.Server : ToWho;
			PropertyInfo[] infos = GetType().GetProperties();
			foreach (var info in infos)
			{
				try
				{
					ToWho.SendInfoMessage("\"{0}\" :{1}", info.Name, JsonConvert.SerializeObject(info.GetValue(this)));
				}
				catch
				{

				}
			}
		}
		#endregion
		#region Kick
		public void Kick(string reason, bool silence = false)
		{
			TShock.Utils.Kick(this, reason, true, silence);
		}
		#endregion
		#endregion
		#region Datas
		public string Name { get; set; }
		/// <summary>
		/// 上一次捕获到释放技能
		/// </summary>
		public DateTime LastHandle = DateTime.Now;
		/// <summary>
		/// 玩家存活且在线
		/// </summary>
		public bool Active => TPlayer.active && !TPlayer.dead;
		public bool Dead => TPlayer.dead;
		public bool IgnoreCD { get; set; }
		/// <summary>
		/// 在MySql中的UserID
		/// <para> -1代表All</para>
		/// <para>-2代表服务器</para>
		/// -3为Guest(未注册)
		/// </summary>
		public int UserID { get; private set; }
		/// <summary>
		/// 玩家索引
		/// </summary>
		public int Index { get; internal set; }
		/// <summary>
		/// 当前等级
		/// 更改后会自动改变血量以及MP上限
		/// </summary>
		public int Level
		{
			get
			{
				return level;
			}
			set
			{
				level = value;
				SetLifeMax();
				MaxMP = 100 + (level / 3);
			}
		}
		/// <summary>
		/// 当前经验
		/// </summary>
		public int Exp
		{
			get
			{
				return exp;
			}
			set
			{
				int tmpExp = exp;
				exp = value;
				if (exp < 0)
				{
					exp = tmpExp;
				}
			}
		}
		/// <summary>
		/// 当前升级所需经验
		/// </summary>
		public int UpGradeExp
		{
			get
			{
				return AuraSystem.StarverAuraManager.UpGradeExp(level);
			}
		}
		public int MaxMP { get; set; }
		public byte[,] Weapon { get; set; } = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
		//以下4个属性均为支线任务(未启用)使用
		public int FishCode
		{
			get
			{
				return TBCodes[0];
			}
			set
			{
				TBCodes[0] = value;
			}
		}
		public int MineCode
		{
			get
			{
				return TBCodes[1];
			}
			set
			{
				TBCodes[1] = value;
			}
		}
		public int CollectCode
		{
			get
			{
				return TBCodes[2];
			}
			set
			{
				TBCodes[2] = value;
			}
		}
		public int HunterCode
		{
			get
			{
				return TBCodes[3];
			}
			set
			{
				TBCodes[3] = value;
			}
		}
		public string Buffer;
		/// <summary>
		/// 上一次释放技能时间
		/// </summary>
		internal DateTime[] LastHandles = new DateTime[5] { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now };
		/// <summary>
		/// 数据库连接
		/// </summary>
		internal static MySqlConnection DB { get { return db; } set { db = value; } }
		/// <summary>
		/// 代表服务器
		/// </summary>
		internal static StarverPlayer Server => server;
		/// <summary>
		/// 代表all(仅用于SendMessage和SendData)
		/// </summary>
		internal static StarverPlayer All => all;
		/// <summary>
		/// 用于未注册用户(退出后消失)
		/// </summary>
		internal static StarverPlayer Guest { get { return new StarverPlayer() { Name = "Guest" ,level = 0, UserID = -3,Index = -3 }; } }
		/// <summary>
		/// 上一次使用的技能
		/// </summary>
		internal int LastSkill;
		internal int MP;
		internal int exp;
		/// <summary>
		/// 获取对应Terraria.Player
		/// </summary>
		internal Player TPlayer { get { return Name == "Server" ? TSPlayer.Server.TPlayer : Main.player[Index]; } }
		/// <summary>
		/// 获取对应TSPlayer
		/// </summary>
		internal TSPlayer TSPlayer { get { return Name == "Server" ? TSPlayer.Server : TShock.Players[Index]; } }
		/// <summary>
		/// 技能ID列表
		/// </summary>
		internal unsafe int* Skills;
		/// <summary>
		/// 支线任务使用,暂时没用
		/// </summary>
		internal int[] TBCodes = { 0, 0, 0, 0 };
		private int level = StarverConfig.Config.DefaultLevel;
		#endregion
		#region Privates
		#region Fields
		private int MoonIndex = -1;
		private bool disposed;
		private static StarverPlayer all = new StarverPlayer() { Name = "All", level = int.MaxValue, Index = -1, UserID = -1 };
		private static StarverPlayer server = new StarverPlayer() { Name = "Server", level = int.MaxValue, Index = -2, UserID = -2 };
		private static string SavePath { get { return Starver.SavePathPlayers; } }
		private static SaveModes SaveMode = StarverConfig.Config.SaveMode;
		private static MySqlConnection db;
		private static BinaryWriter writer;
		private static BinaryReader reader;
		private static MemoryStream stream;
		private static byte[] buffer = new byte[30];
		#endregion
		#region ctor
		private unsafe StarverPlayer()
		{
			Skills = (int*)Marshal.AllocHGlobal(sizeof(int) * 5).ToPointer();
		}
		private StarverPlayer(int userID) : this()
		{
			UserID = userID;
			Name = TShock.Users.GetUserByID(UserID).Name;
			for (int i = 0; i < 40; i++)
			{
				if (TShock.Players[i] == null || TShock.Players[i].Active == false || TShock.Players[i].Name != Name)
				{
					continue;
				}
				Index = i;
				break;
			}
		}
		#endregion
		#region dtor
		~StarverPlayer()
		{
			Dispose(false);
		}
		#endregion
		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}
		protected unsafe virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				TBCodes = null;
				Weapon = null;
				GC.SuppressFinalize(this);
			}
			Marshal.FreeHGlobal(new IntPtr(Skills));
		}
		#endregion
		#region NewMoon
		private static int NewMoon()
		{
			int i;
			for (i = 0; i < 200; i++)
			{
				if (!Main.npc[i].active)
				{
					Main.npc[i] = new NPC();
					Main.npc[i].SetDefaults(NPCID.MoonLordCore);
					Main.npc[i].aiStyle = -1;
					NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, i);
					break;
				}
			}
			return i;
		}
		#endregion
		#region NewConnection
		private static MySqlConnection NewConnection()
		{
			string[] dbHost = TShock.Config.MySqlHost.Split(':');
			return new MySqlConnection()
			{
				ConnectionString = string.Format("Server={0}; Port={1}; Database={2}; UserName={3}; Password={4}; Allow User Variables=True;",
					dbHost[0],
					dbHost[1],
					TShock.Config.MySqlDbName,
					TShock.Config.MySqlUsername,
					TShock.Config.MySqlPassword)
			};
		}
		#endregion
		#region SkillToBuffer
		private unsafe void SkillToBuffer()
		{
			ClearBuffer();
			int* begin = Skills;
			int* end = begin + 5;
			int* iterator = begin;
			using (stream = new MemoryStream(buffer, true))
			{
				using (writer = new BinaryWriter(stream))
				{
					while (iterator != end)
					{
						writer.Write(*iterator++);
					}
				}
			}
		}
		#endregion
		#region BufferToSkill
		private unsafe void BufferToSkill()
		{
			int* begin = Skills;
			int* end = begin + 5;
			int* iterator = begin;
			using (stream = new MemoryStream(buffer, true))
			{
				using (reader = new BinaryReader(stream))
				{
					while (iterator != end)
					{
						*iterator++ = reader.ReadInt32();
					}
				}
			}
		}
		#endregion
		#region clearbuffer
		private unsafe static void ClearBuffer()
		{
			fixed (byte* begin = &buffer[0])
			{
				byte* end = begin + buffer.Length;
				byte* iterator = begin;
				while (iterator != end)
				{
					*iterator++ = 0;
				}
			}
		}
		#endregion
		#region InitializeSkills
		private unsafe void InitializeSkills()
		{
			int* begin = Skills;
			int* end = Skills + 5;
			while (begin != end)
			{
				*begin++ = 0;
			}
		}
		#endregion
		#region cctor
		static StarverPlayer()
		{
			//StarverConfig.Config = StarverConfig.Read();
			switch (StarverConfig.Config.SaveMode)
			{
				case SaveModes.MySQL:
					db = NewConnection();
					var db2 = new MySqlConnection(db.ConnectionString);
					db2.Open();
					var creator = new TableCreator(db2);
					var Table = new SQLTable("Starver",
						new SQLColumn { Name = "UserID", DataType = MySqlDbType.Int32, Length = 4 },
						new SQLColumn { Name = "Level", DataType = MySqlDbType.Int32, Length = 4 },
						new SQLColumn { Name = "Exp", DataType = MySqlDbType.Int32, Length = 4 },
						new SQLColumn { Name = "TBCodes", DataType = MySqlDbType.Text, Length = 20 },
						new SQLColumn { Name = "Skills", DataType = MySqlDbType.VarBinary, Length = 30 },
						new SQLColumn { Name = "Weapons", DataType = MySqlDbType.Text, Length = 80 }
						);
					creator.CreateTable(Table);
					break;
				case SaveModes.Json:
					SaveMode = SaveModes.Json;
					break;
			}
			TSPlayer.Server.SendInfoMessage("Config.SaveMode:{0}", StarverConfig.Config.SaveMode);
			TSPlayer.Server.SendInfoMessage("SaveMode:{0}", SaveMode);
		}
		#endregion
		#endregion
	}
}
