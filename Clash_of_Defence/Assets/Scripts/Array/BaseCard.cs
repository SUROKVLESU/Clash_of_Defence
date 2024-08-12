using System;
using UnityEngine;

[CreateAssetMenu(fileName ="BaseCard",menuName ="Card/BaseCard")]
[Serializable]
public class BaseCard: BaseEssenceObject
{
    public GameObject CardCover;
    public Resources[] PriceBuilding;
    public int[] PriceCard;
    public SizeMapCell SizeMapCell;
}

