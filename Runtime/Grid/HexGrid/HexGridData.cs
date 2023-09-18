using SimonB.Core.Utility;
using UnityEngine;

namespace SimonB.Core.Grid.Hex
{
	public class HexGridData : GridData
	{
		public readonly HexOrientation orientation;
    	
		
		public HexGridData(Vector2Int size, Vector2 offset, float cellSize, HexOrientation orientation) : base(size, offset, new Vector2(cellSize,cellSize))
		{
			this.orientation = orientation;
			
		}

		protected override Vector2[] Directions => 
			orientation == HexOrientation.FlatTop
				? new[]
				{
					30f.ToNormalVector(),
					90f.ToNormalVector(),
					150f.ToNormalVector(),
					210f.ToNormalVector(),
					270f.ToNormalVector(),
					330f.ToNormalVector()
				}
				: new[]
				{
					0f.ToNormalVector(),
					60f.ToNormalVector(),
					120f.ToNormalVector(),
					180f.ToNormalVector(),
					240f.ToNormalVector(),
					300f.ToNormalVector()
				};

		public override Vector2Int GetGridPosition(Vector2 worldPosition)
		{
			return Vector2Int.RoundToInt(HexHelper.CoordinateToAxial(worldPosition.x, worldPosition.y, cellSize.x, orientation));
		}

		public override Vector2 GetWorldPosition(Vector2Int gridPosition)
		{
			var hexPosition = Vector2Int.RoundToInt(HexHelper.CoordinateToAxial(gridPosition.x, gridPosition.y, cellSize.x, orientation));
			Vector2 center = HexHelper.Center(cellSize.x, hexPosition, orientation);
			return center;
		}
	}
}