using UnityEngine;

[CreateAssetMenu(fileName ="BaseCard",menuName ="Card/BaseCard")]
public class BaseCard:ScriptableObject
{
    public int Id;
    public string Name;
    private int LevelCard = 1;
    public bool IsUnlocked;
    public int Power;
    public GameObject CardCover;
    public GameObject[] CardGameObjects = new GameObject[10];
    public void SetLevelCard(int levelCard)
    {
        if (levelCard >0&&levelCard<11)
        {
            this.LevelCard = levelCard;
        }
    }
    public int GetLevelCard()
    {
        return LevelCard-1;
    }
}

