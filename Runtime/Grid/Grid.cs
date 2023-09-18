using System;
using SimonB.Core.Utility;
using UnityEngine;

namespace SimonB.Core.Grid
{
	public class Grid<TGridObject,TGridData> where TGridData : IGridData 
	{
		private readonly TGridObject[,] grid;
		private readonly TGridData data;

		public TGridData Data => data;
		
		public Vector2Int Size => data.GetGridSize();
		public Vector2 Offset => data.GetGridOffset();
		public Vector2 CellSize => data.GetCellSize();
		
		public delegate TGridObject GridObjectInit(Grid<TGridObject,TGridData> grid,Vector2Int gridPos);
		public event Action<Vector2Int> OnGridObjectUpdated;

		public void TriggerGridObjectUpdated(Vector2Int gridPosition)
		{
			OnGridObjectUpdated?.Invoke(gridPosition);
		}
		
		public Grid(TGridData data, GridObjectInit gridObjectInit)
		{
			this.data = data;
			grid = new TGridObject[data.GetGridSize().x, data.GetGridSize().y];
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					grid[x, y] = gridObjectInit(this, new Vector2Int(x, y));
				}
			}
		}

		public bool TryGetGridObject(Vector2 worldPosition, out TGridObject gridObject) 
			=> TryGetGridObject(data.GetGridPosition(worldPosition), out gridObject);
		
		public bool TryGetGridObject(Vector2Int gridPosition, out TGridObject gridObject)
		{
			if(gridPosition.IsOutside(data.GetGridSize().Shrink(1)))
			{
				gridObject = default;
				return false;
			}
			gridObject = grid[gridPosition.x,gridPosition.y];
			return true;
		}
		
		public bool TrySetGridObject(Vector2 worldPosition, TGridObject newGridObject)
			=> TrySetGridObject(data.GetGridPosition(worldPosition), newGridObject);
		
		public bool TrySetGridObject(Vector2Int gridPosition, TGridObject newGridObject)
		{
			if(gridPosition.IsOutside(data.GetGridSize().Shrink(1)))
				return false;
			grid[gridPosition.x, gridPosition.y] = newGridObject;
			OnGridObjectUpdated?.Invoke(gridPosition);
			return true;
		}

		public TGridObject[] GetAllNeighbors(Vector2Int gridPosition)
		{
			var neighborIndexes = data.GetNeighborIndexes(gridPosition);
			var length = neighborIndexes.Length;
			TGridObject[] neighbors = new TGridObject[length];
			for (int i = 0; i < length; i++)
			{
				TryGetGridObject(neighborIndexes[i], out neighbors[i]);
			}
			return neighbors;
		}
		
		
		

	} 
}
