using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject pauseCanva;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip hoverSound;
    private bool isActivated;
    private void Awake()
    {
        GUI.SetActive(true);
        pauseCanva.SetActive(false);
    }
    private void Start()
    {
        isActivated = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& isActivated== false)
        {
            
            displayPauseCanvas();
            isActivated = true;
        }else if(Input.GetKeyDown(KeyCode.Escape) && isActivated == true)
        {
            closePauseCanvas();
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
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
