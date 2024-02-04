using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject pauseCanva;
    [SerializeField] GameObject inventoryCanva;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip hoverSound;
    
    //Acttivate the GUI at the beggining and desactivate the pause & inventory canvas
    private void Awake()
    {
        GUI.SetActive(true);
        pauseCanva.SetActive(false);
        inventoryCanva.SetActive(false);
    }    
    //Display the Pause Canvas
    public void displayPauseCanvas()
    {
        GUI.SetActive(false);
        pauseCanva.SetActive(true);
    }
    //Close the Pause Canvas
    public void closePauseCanvas()
    {
        pauseCanva.SetActive(false);
        GUI.SetActive(true);
    }
    //Close the Inventory Canvas
    public void closeInventoryCanvas()
    {
        inventoryCanva.SetActive(false);
        pauseCanva.SetActive(true);
    }
    //Open the Inventory Canvas
    public void openInventoryCanvas()
    {
        pauseCanva.SetActive(false);
        inventoryCanva.SetActive(true);
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
