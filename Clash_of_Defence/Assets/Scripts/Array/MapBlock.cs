using System;
using UnityEngine;

public class MapBlock
{
    public MapCell[,] ArrayMapCell;
    private const int SizeMap = 5;
    private const int SizeCell = 4;
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
    public bool IsFreeCell(SizeMapCell sizeMapCell)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if(ArrayMapCell[i, j].Free && IsFreeSizeMapCell(i, j, sizeMapCell))
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
    public MapCell SearchFreeCell(SizeMapCell sizeMapCell)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if (ArrayMapCell[i, j].Free&& IsFreeSizeMapCell(i, j, sizeMapCell))
                {
                    return ArrayMapCell[i, j];
                }
            }
        }
        return null;
    }
    public MapCell SearchFreeCell(Vector2Int position)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if ((ArrayMapCell[i, j].Position - position).sqrMagnitude < 1)
                {
                    return ArrayMapCell[i, j];
                }
            }
        }
        return null;
    }
    public bool IsPositionCell(Vector2Int position,SizeMapCell sizeMapCell)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if ((ArrayMapCell[i, j].Position - position).sqrMagnitude < 1 && ArrayMapCell[i,j].Free
                    && IsFreeSizeMapCell(i, j, sizeMapCell))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsPositionCell(Vector2Int position)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if ((ArrayMapCell[i, j].Position - position).sqrMagnitude < 1 && ArrayMapCell[i, j].Free)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsPositionSizeMapCell(Vector2Int position, SizeMapCell sizeMapCell)
    {
        for (int k = 0; k < sizeMapCell.Sizes.Length; k++)
        {
            for (int l = 0; l < sizeMapCell.Sizes[k].line.Length; l++)
            {
                /*if (sizeMapCell.Sizes[k].line[l]
                    &&IsPositionCell(new Vector2Int(position.x+SizeCell*l, position.y + SizeCell * k)))
                {
                    continue;
                }
                else return false;*/
                if (sizeMapCell.Sizes[k].line[l])
                {
                    if (IsPositionCell(new Vector2Int(position.x + SizeCell * l, position.y + SizeCell * k)))
                    {
                        continue;
                    }
                    else return false;
                }
                else
                {
                    continue;
                }
            }
        }
        return true;
    }
    public void SetFreeCell(Vector2Int position,SizeMapCell sizeMapCell,bool free)
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                if ((ArrayMapCell[i, j].Position - position).sqrMagnitude<1)
                {
                    for (int k = 0; k < sizeMapCell.Sizes.Length; k++)
                    {
                        for (int l = 0; l < sizeMapCell.Sizes[k].line.Length; l++)
                        {
                            if (sizeMapCell.Sizes[k].line[l])
                            {
                                ArrayMapCell[i + k, j + l].Free = free;
                            }
                        }
                    }
                }
            }
        }
    }
    private bool IsFreeSizeMapCell(int indexX, int indexY, SizeMapCell sizeMapCell)
    {
        if (indexY + sizeMapCell.Sizes[0].line.Length  > SizeMap) return false;
        if (indexX + sizeMapCell.Sizes.Length  > SizeMap) return false;
        for (int k = 0; k < sizeMapCell.Sizes.Length; k++)
        { 
            for (int l = 0; l < sizeMapCell.Sizes[k].line.Length; l++)
            {
                if (sizeMapCell.Sizes[k].line[l])
                {
                    if (!ArrayMapCell[indexX + k, indexY + l].Free)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void ResetFreeMapCell()
    {
        for (int i = 0; i < SizeMap; i++)
        {
            for (int j = 0; j < SizeMap; j++)
            {
                ArrayMapCell[i, j].Free=true;
            }
        }
    }
}

