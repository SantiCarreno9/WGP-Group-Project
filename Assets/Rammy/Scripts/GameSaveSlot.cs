using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveSlot : MonoBehaviour
{
    [SerializeField] TMP_Text textDisplay;
    [SerializeField] int slotNumber;
    public static event Action newDataSaved;

    GameData gameData;
    private void Awake()
    {
        UpdateData();
    }
    private void OnEnable()
    {
        newDataSaved += UpdateData;
    }
    private void OnDisable()
    {
        newDataSaved -= UpdateData;
    }
    void UpdateData()
    {
        gameData = SaveManager.GetGameData(slotNumber);
        if (gameData == null)
        {
            return;
        }
        textDisplay.text = $"Level: {gameData.levelNumber}, Health: {gameData.playerHealth}";
    }

    public void LoadGame()
    {
        if(gameData == null)
        {
            return;
        }

        SaveManager.UpdateCurrentGameData(gameData);
        GameManager.LoadLevel(gameData.levelNumber);
    }
    public void SaveGame()
    {
        gameData = GameManager.Instance.GetCurrentGameData();
        SaveManager.SaveGameData(gameData, slotNumber);
        textDisplay.text = $"Level: {gameData.levelNumber}, Health: {gameData.playerHealth}";
        newDataSaved?.Invoke();
    }
}
