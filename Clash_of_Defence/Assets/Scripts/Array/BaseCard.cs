using UnityEngine;

[CreateAssetMenu(fileName ="BaseCard",menuName ="Card/BaseCard")]
public class BaseCard:ScriptableObject
{
    public int Id;
    private static int NextId = 0;
    public string Name;
    public int LevelCard = 1;
    public int MaxLevelCard {  get { return CardGameObjects.Length; } }
    public bool Unlocked;
    public int Power;
    public float ProbabilityCardFalling;
    public GameObject CardCover;
    public GameObject[] CardGameObjects;
    public void SetLevelCard(int levelCard)
    {
        if (levelCard >0&&levelCard<=MaxLevelCard)
        {
            this.LevelCard = levelCard;
        }
    }
    public int GetLevelCard()
    {
        return LevelCard-1;
    }public void ResetLevel()
    {
        LevelCard = 1;
    }
    public BaseCard()
    {
        //Id = NextId++;
    }
}

