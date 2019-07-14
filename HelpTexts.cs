using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers
{
	public class HelpTexts
	{
		public static string Task { get; private set; } =  
			"用法:\n" +
			"    check:    检查任务\n" +
			"    list:     列出任务";
		public static string Aura { get; private set; } =
			"用法:\n" +
			"    up:    升级\n" +
			"    toexp: 兑换经验\n" +
			"    set <slot> <skill> :设置技能\n" +
			"    list:  查看技能列表\n" +
			"    buy <slot> 获取技能槽位对应的武器\n" +
			"    help <skillName/SkillID> 查看技能说明";
		public static string LevelSystem { get; private set; } =
			"用法:\n" +
			"    up:    升级\n" +
			"    toexp: 兑换经验";
	}
}
