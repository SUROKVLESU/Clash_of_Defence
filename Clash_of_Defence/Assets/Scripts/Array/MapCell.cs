using UnityEngine;
public class MapCell
{
    public bool Free;
    public Vector2Int Position;
    public MapCell(Vector2Int position)
    {
        Free = true;
        Position = position;
    }
}

