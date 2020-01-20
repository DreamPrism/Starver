using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.BossSystem.Bosses
{
	public enum BossMode
	{
		WaitForMode,
		Explosive,
		Rush,
		Gazing,
		/// <summary>
		/// projID中选这个
		/// </summary>
		DemonSickle,
		Fire,
		SummonFollows,
		FlamingScythe,
		Present,
		Sharknado,
		CraShoot1,
		CraShoot2,
		CraVortexSphere,
		/// <summary>
		/// ai[0]需要设置为负无穷
		/// ai[1]设置为1正常，0不动
		/// </summary>
		Mist,
		/// <summary>
		/// 四处乱射
		/// </summary>
		CraShoot3,
		Crashoot4,
		CraFireBall,
		/// <summary>
		/// 仅限临界等级+可用
		/// </summary>
		WitherSaucerLaser,
		WitherBolt,
		WitherSphere,
		WitherInvincible,
		WitherLostSoul,
		QueenBeeStingerRain,
		/// <summary>
		/// 毒刺阵
		/// </summary>
		QueenBeeStingerArray,
		QueenBeeStingerRound,
		DarkMageDrakinShot,
		DarkMageSelfShot,
		DarkMageSigil,
		RedDevilTrident,
		/// <summary>
		/// desertspiritflame
		/// </summary>
		RedDevilFlame,
		RedDevilLaser,
		PigronArrow,
		PigronFeather,
		FrostBlast,
		PigronWeb,
		IceQueenWave,
		IceQueenShard,
		IceQueenLaser,
		DeathsTwinkle,
		DeathsFlowInvaderShot,
		DeathsLaser,
		DeathsSummonFollows,
		SpazmatismBetsyFireBall,
		SpazmatismFlaming,
		SpazmatismFlameNormal,
		RetinazerLaserBlue,
		RetinazerDeathLaser,
		RetinazerSaucerLaser,
		RetinazerLaserWalker,
		PlanteraThornBall,
		PlanteraSeedRound,
		PlanteraSeed,
		PlanteraSpore,
		CultistFireBall,
		CultistLightning,
		CultistShadowFireball,
		ThrowingBone,
		LineBone,
		BonePullBack,
		PrimeExLaser,
		PrimeExMissile,
		PrimeExRocket,
		WormLaser,
		WormStorm
	}
}
