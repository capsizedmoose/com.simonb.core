using UnityEngine;

namespace SimonB.Core.Grid
{
	public interface IGridData
	{
		public Vector2Int GetGridSize(); 
		public Vector2 GetGridOffset(); 
		public Vector2 GetCellSize(); 
		public Vector2 GetWorldPosition(Vector2Int gridPosition);
		public Vector2Int GetGridPosition(Vector2 worldPosition);
		public Vector2[] GetNeighborDirections();
		public Vector2Int[] GetNeighborIndexes(Vector2Int index);
		
	}
}