using SimonB.Core.Utility;
using UnityEngine;

namespace SimonB.Core.Grid.Hex
{
	public class HexGridData : IGridData
	{
		public readonly HexOrientation orientation;
		private readonly Vector2Int size;
		private readonly Vector2 offset;
		private readonly float hexSize;


		private static readonly Vector2[] flatDirections = {
			330f.ToNormalVector(),
			30f.ToNormalVector(),
			90f.ToNormalVector(),
			150f.ToNormalVector(),
			210f.ToNormalVector(),
			270f.ToNormalVector()
		};
		private static readonly Vector2[] pointyDirections = {
			0f.ToNormalVector(),
			60f.ToNormalVector(),
			120f.ToNormalVector(),
			180f.ToNormalVector(),
			240f.ToNormalVector(),
			300f.ToNormalVector()
		};
		
		public HexGridData(Vector2Int size, Vector2 offset, float hexSize, HexOrientation orientation)
		{
			this.orientation = orientation;
			this.size = size;
			this.offset = offset;

		}

		public Vector2Int GetGridPosition(Vector2 worldPosition)
		{
			return Vector2Int.RoundToInt(HexHelper.CoordinateToAxial(worldPosition.x, worldPosition.y, hexSize, orientation));
		}
		
		public Vector2[] GetNeighborDirections() => orientation == HexOrientation.FlatTop ? flatDirections : pointyDirections;
		public Vector2Int GetGridSize() => size;
		public Vector2 GetGridOffset() => offset;
		public Vector2 GetCellSize() => new Vector2(hexSize, hexSize);
		public Vector2 GetWorldPosition(Vector2Int gridPosition)
		{
			var hexPosition = Vector2Int.RoundToInt(HexHelper.CoordinateToAxial(gridPosition.x, gridPosition.y, hexSize, orientation));
			Vector2 center = HexHelper.Center(hexSize, hexPosition, orientation);
			return center;
		}
		public Vector2Int[] GetNeighborIndexes(Vector2Int index)
		{
			var cube = HexHelper.OffsetToCube(index, orientation);
			return HexHelper.GetCubeNeighbors(cube,orientation);
		}
	}
}