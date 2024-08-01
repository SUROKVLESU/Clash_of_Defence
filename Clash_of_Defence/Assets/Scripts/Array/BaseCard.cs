using UnityEngine;

[CreateAssetMenu(fileName ="BaseCard",menuName ="Card/BaseCard")]
public class BaseCard:ScriptableObject
{
    public int Id;
    //private static int NextId = 0;
    public string Name;
    [HideInInspector] public int LevelCard = 1;
    public int MaxLevelCard {  get 
        { 
            if (CardGameObjects.Length <= Characteristics.HP.Length){ return CardGameObjects.Length;}
            else { return Characteristics.HP.Length; }
        } }
    /*[HideInInspector]*/
    [SerializeField] int CurrentMaxLevelCard;
    //public bool Unlocked;
    public int Power;
    public float ProbabilityCardFalling;
    public GameObject CardCover;
    public GameObject[] CardGameObjects;
    [Header("Characteristics")]
    public BaseCharacteristics Characteristics;
    public void LevelUp()
    {
        LevelCard++;
        if (LevelCard > CurrentMaxLevelCard)
        {
            LevelCard = CurrentMaxLevelCard;
        }
    }
    public void MaxLevelUp()
    {
        CurrentMaxLevelCard++;
        if (CurrentMaxLevelCard > MaxLevelCard)
        {
            CurrentMaxLevelCard = MaxLevelCard;
        }
    }
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
    }
    public int GetCurrentMaxLevelCard()
    {
        return CurrentMaxLevelCard;
    }
    public void SetCurrentMaxLevelCard(int currentMaxLevelCard)
    {
        if (currentMaxLevelCard >= 0 && currentMaxLevelCard <= MaxLevelCard)
        {
            this.CurrentMaxLevelCard = currentMaxLevelCard;
        }
    }
    public void ResetLevel()
    {
        LevelCard = 1;
    }
    public bool IsMaxLevelCard()
    {
        if (LevelCard + 1 > CurrentMaxLevelCard) return true;
        return false;
    }
    public BaseCard()
    {

    }
}

