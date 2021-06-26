using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    
    public int s_points;
    public bool s_canSpawn;
    public int s_bossCatHP;
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}