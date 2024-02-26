using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    static Dictionary<int, int> buildIndexToLevelNumberMapping = new Dictionary<int, int>()
    {
        { 1, 1 },
        { 2, 2 }
    };
    public static GameData CurrentGameData { get; private set; } = GetNewGameData();
    private const int numberOfSaves = 3;
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
    public static GameData GetGameData(int slotNumber)
    {
        if (!PlayerPrefs.HasKey(GetKey(slotNumber)))
        {
            return null;
        }
        string dataAsString = PlayerPrefs.GetString(GetKey(slotNumber));
        return JsonUtility.FromJson<GameData>(dataAsString);
    }
    public static GameData GetNewGameData()
    {
        return new GameData() { levelNumber = 1, playerHealth = 200, hasPosition = false };
    }
    public static void UpdateCurrentGameData(GameData gameData)
    {
        CurrentGameData = gameData;
    }

    static string GetKey(int slotNumber)
    {
        return $"GameData{slotNumber}";
    }

    public static int GetLevelNumberFromBuildIndex(int buildIndex)
    {
        return buildIndexToLevelNumberMapping[buildIndex];
    }
    public static int GetBuildIndexFromLevelNumber(int levelNumber)
    {
        return buildIndexToLevelNumberMapping.First((pair) =>
        {
            return pair.Value == levelNumber;
        }).Key;
    }
}
