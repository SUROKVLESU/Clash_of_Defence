using System;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseCardCharacteristics", menuName = "Characteristics/AttackingBuildingCharacteristics")]
public class AttackingBuildingCharacteristics:BaseCharacteristics
{
    public Attributes[] Attack;
    public int[] AttackSpeed;
}

