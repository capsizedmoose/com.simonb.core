using UnityEngine;

namespace SimonB.Core.Utility
{
	public static class ExtraMath
	{
		public static int Modulo(this int value, int mod)
		{
			int r = value%mod;
			return r<0 ? r+mod: r;
		}

		public static bool IsOdd(this int value)
		{
			return value % 2 == 1;
		}
		public static bool IsEven(this int value)
		{
			return value % 2 == 0;
		}

		public static Vector3 ToVector3(this Color color)
		{
			return new Vector3(color.r, color.g, color.b);
		}
		public static Color ToColor(this Vector3 v)
		{
			return new Color(v.x, v.y, v.z);
		}
	}
}