/* GameManager.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script saves the reference of the player and displays the Game Over screen
 * 
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    public GameObject Player => _player;

    public bool IsGamePaused() => Time.timeScale == 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowGameOverScreen()
    {
        Invoke("GameOver", 3);
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
