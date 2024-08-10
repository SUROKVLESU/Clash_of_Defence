using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveGame
{
    private SerializeObject SaveObject;
    private string Path = Application.persistentDataPath+"/save.txt";
    [Serializable]
    class SerializeObject
    {
        public int Gold;
        public KeyValue[] CardsCurentMaxLevel = new KeyValue[0];
    }
    private SerializeObject GetSerializeObject()
    {
        List<BaseCard> cards = GameController.instance.CollectionController.GetAllCards();
        SerializeObject serializeObject = new SerializeObject()
        {
            Gold = GameController.instance.ResourcesController.Gold,
            CardsCurentMaxLevel = new KeyValue[cards.Count]
        };
        for (int i = 0; i < cards.Count; i++)
        {
            serializeObject.CardsCurentMaxLevel[i]=(new KeyValue() {Key= cards[i].Id,Value= cards[i].GetCurrentMaxLevel() });
        }
        return serializeObject;
    }
    public void UploadingDataGame()
    {
        GameController.instance.ResourcesController.Gold=SaveObject.Gold;
        List<BaseCard> cards = GameController.instance.CollectionController.GetAllCards();
        for (int i = 0; i < SaveObject.CardsCurentMaxLevel.Length; i++)
        {
            for (int j = 0; j < cards.Count; j++)
            {
                if (SaveObject.CardsCurentMaxLevel[i].Key == cards[j].Id)
                {
                    cards[j].LoadCurentMaxLevel(SaveObject.CardsCurentMaxLevel[i].Value);
                }
            }
        }
    }
    public void Save()
    {
        using (StreamWriter fs = new StreamWriter(Path))
        {
            SaveObject = GetSerializeObject();
            string str = JsonUtility.ToJson(SaveObject);
            fs.WriteLine(str);
        }
    }
    public void Load()
    {
        using (StreamReader fs = new StreamReader(Path))
        {
            string json = fs.ReadToEnd();
            SaveObject = JsonUtility.FromJson<SerializeObject>(json);
        }
        if (SaveObject == null)
        {
            SaveObject = GetSerializeObject();
            return;
        }
        UploadingDataGame();
    }
}

