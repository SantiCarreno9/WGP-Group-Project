using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject pauseCanva;

    public void displayPauseCanva()
    {
        GUI.SetActive(false);
        pauseCanva.SetActive(true);
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
