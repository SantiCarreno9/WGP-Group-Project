using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class SaveManager
{
    private const int numberOfSaves = 5;
    public static GameData[] GetAllSaves()
    {
        GameData[] saves = new GameData[numberOfSaves];
        for (int i = 0; i < numberOfSaves; i++)
        {
            string key = GetKey(i);
            if (PlayerPrefs.HasKey(key))
            {
                string dataAsString = PlayerPrefs.GetString(key);
                saves[i] = JsonUtility.FromJson<GameData>(dataAsString);
            }
        }
        return saves;
    }
    public static void SaveGameData(GameData gameData, int slotNumber)
    {
        string dataAsString = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(GetKey(slotNumber), dataAsString);
    }
    public static GameData LoadGameData(int slotNumber)
    {
        string dataAsString = PlayerPrefs.GetString(GetKey(slotNumber));
        return JsonUtility.FromJson<GameData>(dataAsString);
    }
    public static GameData GetNewGameData()
    {
        return new GameData() { levelNumber = 1, playerHealth = 100 };
    }

    static string GetKey(int slotNumber)
    {
        return $"GameData{slotNumber}";
    }
}
