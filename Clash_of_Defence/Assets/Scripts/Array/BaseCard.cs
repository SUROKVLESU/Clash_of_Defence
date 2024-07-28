using UnityEngine;

[CreateAssetMenu(fileName ="BaseCard",menuName ="Card/BaseCard")]
public class BaseCard:ScriptableObject
{
    public int Id;
    public string Name;
    public GameObject CardCover;
    public BuildingFromCard BuildingFromCard;
}

