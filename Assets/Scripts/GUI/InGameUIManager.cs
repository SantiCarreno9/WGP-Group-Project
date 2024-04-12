using Character.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsView GUI;
    [SerializeField] GameObject pauseCanva;
    [SerializeField] GameObject inventoryCanva;
    [SerializeField] GameObject saveGameCanva;
    [SerializeField] GameObject loadGameCanva;
    [SerializeField] GameObject quests_AchievementsCanva;
    [SerializeField] GameObject questsCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip hoverSound;
    
    //Acttivate the GUI at the beggining and desactivate the pause & inventory canvas
    private void Awake()
    {
        GUI.ShowStats();
        pauseCanva.SetActive(false);
        inventoryCanva.SetActive(false);
        saveGameCanva.SetActive(false);
        loadGameCanva.SetActive(false);
        quests_AchievementsCanva.SetActive(false);
        questsCanvas.SetActive(false);
        achievementsCanvas.SetActive(true);
    }    
    //Display the Pause Canvas
    public void displayPauseCanvas()
    {
        GUI.HideStats();
        pauseCanva.SetActive(true);
    }
    //Close the Pause Canvas
    public void closePauseCanvas()
    {
        pauseCanva.SetActive(false);
        GUI.ShowStats();
    }
    //Close the Inventory Canvas
    public void closeInventoryCanvas()
    {
        inventoryCanva.SetActive(false);
    }
    //Open the Inventory Canvas
    public void openInventoryCanvas()
    {
        inventoryCanva.SetActive(true);
    }
    //Close the SaveGame Canvas
    public void closeSaveGameCanvas()
    {
        saveGameCanva.SetActive(false);
        pauseCanva.SetActive(true);
    }
    //Open the SaveGame Canvas
    public void openSaveGameCanvas()
    {
        pauseCanva.SetActive(false);
        saveGameCanva.SetActive(true);
    }
    //Close the LoadGame Canvas
    public void closeLoadGameCanvas()
    {
        loadGameCanva.SetActive(false);
        pauseCanva.SetActive(true);
    }
    //Open the LoadGame Canvas
    public void openLoadGameCanvas()
    {
        pauseCanva.SetActive(false);
        loadGameCanva.SetActive(true);
    }
    public void openQuests_AchievementsCanvas()
    {
        quests_AchievementsCanva.SetActive(true);
    }
    public void closeQuests_AchievementsCanvas()
    {
        quests_AchievementsCanva.SetActive(false);
    }
    public void displayQuestsCanvas()
    {
        questsCanvas.SetActive(true);
        achievementsCanvas.SetActive(false);
    }
    public void displayAchievementsCanvas()
    {
        achievementsCanvas.SetActive(true);
        questsCanvas.SetActive(false );
    }
    //Sends to Main Menu
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    //Make click Sound
    public void ClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }    
}
