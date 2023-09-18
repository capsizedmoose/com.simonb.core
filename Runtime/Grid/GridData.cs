using UnityEngine;

namespace SimonB.Core.Grid
{
	public class GridData : IGridData
    {
    	private readonly Vector2Int size;
    	private readonly Vector2 offset;
    	private readonly Vector2 cellSize;
	    
	    private static readonly Vector2[] neighborDirections = {
		    Vector2.right,
		    Vector2.up,
		    Vector2.left,
		    Vector2.down
	    };
	    
    	protected GridData(Vector2Int size, Vector2 offset, Vector2 cellSize)
    	{
    		this.size = size;
    		this.offset = offset;
    		this.cellSize = cellSize;
		    
	    }

	    public Vector2Int GetGridSize() => size;
	    public Vector2 GetGridOffset() => offset;
	    public Vector2 GetCellSize() => cellSize;

	    public virtual Vector2 GetWorldPosition(Vector2Int gridPosition)
    	{
    		return gridPosition * cellSize + offset;
    	}

    	public virtual Vector2Int GetGridPosition(Vector2 worldPosition)
    	{
    		return new Vector2Int(
    			Mathf.FloorToInt((worldPosition.x / cellSize.x) + offset.x),
    			Mathf.FloorToInt((worldPosition.y / cellSize.y) + offset.y)
    			);
    	}

	    public Vector2[] GetNeighborDirections() => neighborDirections;

	    public virtual Vector2Int[] GetNeighborIndexes(Vector2Int index)
	    {
		    return new[]
		    {
			    index + new Vector2Int(1, 0),
			    index + new Vector2Int(0, 1),
			    index + new Vector2Int(-1, 0),
			    index + new Vector2Int(0, -1),
		    };
	    }
	    
    }

    
}