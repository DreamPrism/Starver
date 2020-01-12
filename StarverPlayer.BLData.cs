using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Starvers
{
    using TaskSystem;
	public partial class StarverPlayer
	{
		// 64 bytes
		public struct BLData
		{
			private Data16 data0;
			private Data16 data1;
			private Data16 data2;
			private Data16 data3;

			public BLFlags AvaiablleBLs
			{
				get => (BLFlags)data0.ByteValue0;
				set => data0.ByteValue0 = (byte)value;
			}
			[IndexerName("Process")]
			public byte this[BLFlags bl]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public static BLData Deserialize(byte[] binary)
			{
				if(binary.Length != 4 * 16)
				{
					throw new ArgumentException("binary.Length != 4 * 16", nameof(binary));
				}
				unsafe void SetValue(int index, Data16* data)
				{
					byte* ptr = &data->ByteValue0;
					for (int i = 0; i < sizeof(Data16); i++)
					{
						ptr[i] = binary[index * sizeof(Data16) + i];
					}
				}
				BLData data;
				unsafe
				{
					SetValue(0, &data.data0);
					SetValue(1, &data.data1);
					SetValue(2, &data.data2);
					SetValue(3, &data.data3);
				}
				return data;
			}
			public static byte[] Serialize(in BLData data)
			{
				byte[] value = new byte[16 * 4];
				void Write(int index, in Data16 data)
				{
					value[index + 0] = data.ByteValue0;
					value[index + 1] = data.ByteValue1;
					value[index + 2] = data.ByteValue2;
					value[index + 3] = data.ByteValue3;
					value[index + 4] = data.ByteValue4;
					value[index + 5] = data.ByteValue5;
					value[index + 6] = data.ByteValue6;
					value[index + 7] = data.ByteValue7;
					value[index + 8] = data.ByteValue8;
					value[index + 9] = data.ByteValue9;
					value[index + 10] = data.ByteValue10;
					value[index + 11] = data.ByteValue11;
					value[index + 12] = data.ByteValue12;
					value[index + 13] = data.ByteValue13;
					value[index + 14] = data.ByteValue14;
					value[index + 15] = data.ByteValue15;
				}
				Write(0, data.data0);
				Write(1, data.data1);
				Write(2, data.data2);
				Write(3, data.data3);
				return value;
			}
		}
	}
}
