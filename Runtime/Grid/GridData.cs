using UnityEngine;

namespace SimonB.Core.Grid
{
	public class GridData
    {
    	public readonly Vector2Int size;
    	public readonly Vector2 offset;
    	public readonly Vector2 cellSize;
    	protected GridData(Vector2Int size, Vector2 offset, Vector2 cellSize)
    	{
    		this.size = size;
    		this.offset = offset;
    		this.cellSize = cellSize;
	    }
    	
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

	    protected virtual Vector2[] Directions =>  new Vector2[]
	    {
		    Vector2.right,
		    Vector2.up,
		    Vector2.left,
		    Vector2.down
	    };
	    
    }

    public class BasicGridData : GridData
    {
    	public BasicGridData(Vector2Int size, Vector2 offset, float cellSize) : base(size, offset, new Vector2(cellSize,cellSize)) { }
    }

    
}