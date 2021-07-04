using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // points
    public int s_points;
    // boss
    public bool s_canSpawn;
    public int s_bossCatHP;
    
    // items
    // Cat House 
    public int s_idleRateM = 1;
    public int s_idleRateMTracker;

    // Cat Food 
    public int s_idleRateA = 1;
    public int s_idleRateATracker;

    // Human Gloves 
    public int s_manualCollect = 1;
    public int s_manualCollectTracker;

    // Cat Toy
    public int s_increaseDamage = 10;
    public int s_increaseDamageTracker;

    // Cat Clothes
    public int s_critDamage = 10;
    public int s_critDamageTracker;

    // Cat Shoes
    public int s_critChance = 10;
    public int s_critChanceTracker;

    // Time
    public string s_date;
    public int s_hours;
    public int s_minutes;

    // First time start game
    public bool s_isFirst = true;
    
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