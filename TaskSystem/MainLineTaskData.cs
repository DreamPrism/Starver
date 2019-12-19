using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvers.TaskSystem
{
	public class MainLineTaskData
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Story { get; set; }
		public Dictionary<TaskDifficulty, TaskItem[]> Needs { get; set; }
		public Dictionary<TaskDifficulty, TaskItem[]> Rewards { get; set; }
		public int LevelReward { get; set; }
		public static readonly string ReadMe = @"现在可以在json中编辑任务了
编辑完后重启或者/task reload来使修改生效
任务数量必须是43个, 而且要按顺序
每个任务格式如下: 
{
	ID,
	Needs,
	Rewards,
	LevelReward
	Name,
	Story
}
""ID""为任务序号, 从1到43, 只是为了方便编辑任务的人, 插件会将其忽略

""Needs""为任务所要求的物品, 分为四个难度, 格式如下:
{
	""Easy"": 
	[
		//所需物品谢在这里, 用英文逗号分开每一项
	],
	""Normal"": 
	[
		
	],
	""Hard"": 
	[
		
	],
	""Hell"": 
	[
		
	]
}
物品也有特殊的写法, 像这样: {""ID"": ""XXX"", ""Stack"": ""XXX"", ""Prefix"": ""XXX"" }
ID可以直接写数字
或者写用英文双引号括起来的字符串, 里面写对应物品的 ""标识符"" (见)""ItemIDs.txt""
Stack写物品堆叠数目, 不写则为0, Prefix为前缀ID, 不写则为0(没有前缀)
Easy, Normal, Hard, Hell分别对应任务四个难度, 在StarverConfig.json中设置

""Rewards""为任务的物品奖励, 格式和Needs一样

""LevelReward""为任务的等级奖励

""Name""为任务名称

""Story""里是各种中二的任务介绍
";
		public static string ItemIDs { get; internal set; }
	}
}
