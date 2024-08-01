using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCardCharacteristics", menuName = "Characteristics/BaseCardCharacteristics")]
public class BaseCharacteristics: ScriptableObject
{
    public float[] HP;
    public Attributes[] Protection;
    public int[] Price;
}