using UnityEngine;

public class MapBlock
{
    public MapCell[,] ArrayMapCell;
    private const int SizeMap = 5;
    private static int NextId = 0;
    public readonly int Id;
    public Vector2Int Position;
    public MapBlock(Vector2Int position = new Vector2Int())
    {
        Id = NextId++;
        Position = position;
        ArrayMapCell = new MapCell[SizeMap, SizeMap];
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                ArrayMapCell[j, i] = new MapCell(new Vector2Int(position.x + 4 * (-2 + i), position.y + 4 * (-2 + j)));
            }
        }
    }
    public bool IsFreeCell()
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if(ArrayMapCell[i, j].Free)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public MapCell this[int i,int j]
    {
        get 
        { 
            if((i>=0&&i<=(SizeMap-1))&& (j >= 0 && j <= (SizeMap - 1)))//в будущем может уберу эту проверку
            {
                return ArrayMapCell[i,j];
            }
            return null;
        }
    }
    public MapCell SearchFreeCell()
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if (ArrayMapCell[i, j].Free)
                {
                    return ArrayMapCell[i, j];
                }
            }
        }
        return null;
    }
    public bool IsPositionCell(Vector2Int position)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if (ArrayMapCell[i, j].Position == position && ArrayMapCell[i,j].Free)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void SetFreeCell(Vector2Int position,bool free)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if (ArrayMapCell[i, j].Position == position)
                {
                    ArrayMapCell[i, j].Free = free;
                }
            }
        }
    }
}

