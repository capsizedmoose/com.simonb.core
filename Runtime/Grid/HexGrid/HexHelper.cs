using System;
using UnityEngine;

namespace SimonB.Core.Grid.Hex
{
	public class HexHelper
	{
		/// <summary>
		/// Gets the outer radius of the hexagon.
		/// </summary>
		/// <param name="hexSize"></param>
		/// <returns>the outer radius</returns>
		public static float OuterRadius(float hexSize)
		{
			return hexSize;
		}
		/// <summary>
		/// Gets the inner radius og the hexagon.
		/// </summary>
		/// <param name="hexSize"></param>
		/// <returns>the inner radius</returns>
		public static float InnerRadius(float hexSize)
		{
			return hexSize * 0.866025404f;
		}

		/// <summary>
		/// Get All the corners of the hexagon.
		/// </summary>
		/// <param name="hexSize"></param>
		/// <param name="orientation"></param>
		/// <returns>a Vector2 collection of the corners</returns>
		public static Vector2[] Corners(float hexSize, HexOrientation orientation)
		{
			Vector2[] corners = new Vector2[6];
			for (int i = 0; i < 6; i++)
			{
				corners[i] = Corner(hexSize, orientation, i);
			}
			return corners;
		}

		/// <summary>
		/// Get a specific corner of the hexagon ( CCW starting east -> )
		/// </summary>
		/// <param name="hexSize"></param>
		/// <param name="orientation"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static Vector2 Corner(float hexSize, HexOrientation orientation, int index)
		{
			float angle = (orientation == HexOrientation.PointyTop)?30f:0f * index;
			Vector2 corner = new Vector2(
				hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
				hexSize * Mathf.Sin(angle * Mathf.Deg2Rad));
			return corner;
		}

		/// <summary>
		/// Gets the center of the hex
		/// </summary>
		/// <param name="hexSize"></param>
		/// <param name="gridPosition"></param>
		/// <param name="orientation"></param>
		/// <returns></returns>
		public static Vector2 Center(float hexSize, Vector2Int gridPosition, HexOrientation orientation)
		{
			int x = gridPosition.x;
			int y = gridPosition.y;
			return orientation switch
			{
				HexOrientation.FlatTop => new Vector2(
					x * (OuterRadius(hexSize) * 1.5f),
					(y + x * .5f - x / 2) * (InnerRadius(hexSize) * 2f)),
				HexOrientation.PointyTop => new Vector2(
					(x + y * .5f - y / 2) * (InnerRadius(hexSize) * 2f),
					y * (OuterRadius(hexSize) * 1.5f))
			};
		}

		public static Vector3 OffsetToCube(Vector2 offsetCoord, HexOrientation orientation)
		{
			return OffsetToCube((int) offsetCoord.x, (int) offsetCoord.y, orientation);
		}

		public static Vector3 OffsetToCube(int col, int row, HexOrientation orientation)
		{
			if (orientation == HexOrientation.PointyTop)
			{
				return AxialToCube(OffsetToAxialPointy(col, row));
			}
			else
			{
				return AxialToCube(OffsetToAxialFlat(col, row));
			}
		}

		public static Vector3 AxialToCube(float q, float r)
		{
			return new Vector3(q, r, -q - r);
		}

		public static Vector3 AxialToCube(int q, int r)
		{
			return new Vector3(q, r, -q - r);
		}

		public static Vector3 AxialToCube(Vector2 axialCoord)
		{
			return AxialToCube(axialCoord.x, axialCoord.y);
		}


		public static Vector2 CubeToAxial(int q, int r, int s)
		{
			return new Vector2(q, r);
		}
		public static Vector2 CubeToAxial(float q, float r, float s)
		{
			return new Vector2(q, r);
		}
		public static Vector2 CubeToAxial(Vector3 cube)
		{
			return new Vector2(cube.x, cube.y);
		}

		public static Vector2 OffsetToAxial(int x, int z, HexOrientation orientation)
		{
			if (orientation == HexOrientation.PointyTop)
			{
				return OffsetToAxialPointy(x, z);
			}
			else
			{
				return OffsetToAxialFlat(x, z);
			}
		}

		private static Vector2 OffsetToAxialFlat(int col, int row)
		{
			int q = col;
			int r = row - (col - (col & 1)) / 2;
			return new Vector2(q, r);
		}
		private static Vector2 OffsetToAxialPointy(int col, int row)
		{
			int q = col - (row - (row & 1)) / 2;
			int r = row;
			return new Vector2(q, r);
		}

		public static Vector2 CubeToOffset(int x, int y, int z, HexOrientation orientation)
		{
			if (orientation == HexOrientation.PointyTop)
			{
				return CubeToOffsetPointy(x, y, z);
			}
			else
			{
				return CubeToOffsetFlat(x, y, z);
			}
		}

		public static Vector2 CubeToOffset(Vector3 offsetCoord, HexOrientation orientation)
		{
			return CubeToOffset((int)offsetCoord.x, (int)offsetCoord.y, (int)offsetCoord.z, orientation);
		}

		public static Vector2 CubeToOffsetPointy(int x, int y ,int z)
		{
			Vector2 offsetCoordinates = new Vector2(x + (y - (y & 1)) / 2, y);
			return offsetCoordinates;
		}

		public static Vector2 CubeToOffsetFlat(int x, int y, int z)
		{
			Vector2 offsetCoordinates = new Vector2(x,y + (x - (x & 1)) / 2);
			return offsetCoordinates;
		}

		public static Vector3 CubeRound(Vector3 frac)
		{
			Vector3 roundedCoordinates = new Vector3();
			int rx = Mathf.RoundToInt(frac.x);
			int ry = Mathf.RoundToInt(frac.y);
			int rz = Mathf.RoundToInt(frac.z);
			float xDiff = Mathf.Abs(rx - frac.x);
			float yDiff = Mathf.Abs(ry - frac.y);
			float zDiff = Mathf.Abs(rz - frac.z);
			if (xDiff > yDiff && xDiff > zDiff)
			{
				rx = -ry - rz;
			}
			else if (yDiff > zDiff)
			{
				ry = -rx - rz;
			}
			else
			{
				rz = -rx - ry;
			}

			roundedCoordinates.x = rx;
			roundedCoordinates.y = ry;
			roundedCoordinates.z = rz;
			return roundedCoordinates;
		}

		public static Vector2 AxialRound(Vector2 coordinates)
		{
			return CubeToAxial(CubeRound(AxialToCube(coordinates.x, coordinates.y)));
		}

		public static Vector2 CoordinateToAxial(float x, float z, float hexSize, HexOrientation orientation)
		{
			if (orientation == HexOrientation.PointyTop)
			{
				return CoordinateToAxialPointy(x, z, hexSize);
			}
			else
			{
				return CoordinateToAxialFlat(x, z, hexSize);
			}
		}

		public static Vector2 CoordinateToAxialPointy(float x, float z, float hexSize)
		{
			Vector2 pointyHexCoordinates = new Vector2();
			pointyHexCoordinates.x = (Mathf.Sqrt(3) / 3 * x - 1f / 3 * z) / hexSize;
			pointyHexCoordinates.y = (2f / 3 * z) / hexSize;
			return AxialRound(pointyHexCoordinates);
		}
		public static Vector2 CoordinateToAxialFlat(float x, float z, float hexSize)
		{
			Vector2 flatHexCoordinates = new Vector2();
			flatHexCoordinates.x = (2f / 3 * x) / hexSize;
			flatHexCoordinates.y = (- 1f / 3 * x + Mathf.Sqrt(3) / 3 * z ) / hexSize;
			return AxialRound(flatHexCoordinates);
		}

		public static Vector2 CoordinateToOffset(float x, float z, float hexSize, HexOrientation orientation)
		{
			return CubeToOffset(AxialToCube(CoordinateToAxial(x, z, hexSize,orientation)), orientation);
		}

		

	}
}