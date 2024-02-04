using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject pauseCanva;
    [SerializeField] GameObject inventoryCanva;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip hoverSound;
    private bool isActivated;
    private void Awake()
    {
        GUI.SetActive(true);
        pauseCanva.SetActive(false);
        inventoryCanva.SetActive(false);
    }
    private void Start()
    {
        isActivated = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& isActivated== false)
        {
            PauseGame();
            displayPauseCanvas();
            isActivated = true;
        }else if(Input.GetKeyDown(KeyCode.Escape) && isActivated == true)
        {
            closePauseCanvas();
            //closeInventoryCanvas();
            ResumeGame();
            isActivated = false;
        }
    }
    
    public void displayPauseCanvas()
    {
        GUI.SetActive(false);
        pauseCanva.SetActive(true);
    }
    public void closePauseCanvas()
    {
        pauseCanva.SetActive(false);
        GUI.SetActive(true);
    }
    public void closeInventoryCanvas()
    {
        inventoryCanva.SetActive(false);
        pauseCanva.SetActive(true);
    }
    public void openInventoryCanvas()
    {
        pauseCanva.SetActive(false);
        inventoryCanva.SetActive(true);
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void ClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}