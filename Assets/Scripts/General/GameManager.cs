/* GameManager.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script saves the reference of the player and displays the Game Over screen
 * 
 */
using Character;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerController _playerController;
    public PlayerController Player => _playerController;

    public UnityAction OnLevelFinished;

    public bool IsGamePaused() => Time.timeScale == 0;    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        _playerController.HealthModule.OnDie += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        _playerController.HealthModule.OnDie -= ShowGameOverScreen;
    }

    public void ShowGameOverScreen()
    {
        Invoke("GameOver", 3);
    }

    public void GoToLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public GameData GetCurrentGameData()
    {
        return new GameData() 
        { 
            levelNumber = SaveManager.GetLevelNumberFromBuildIndex(SceneManager.GetActiveScene().buildIndex),
            playerHealth = _playerController.HealthModule.HealthPoints,
            hasPosition = true,
            playerPosition = Player.transform.position,
        };
    }
    public static void LoadLevel(int levelNumber)
    {
        int buildIndex = SaveManager.GetBuildIndexFromLevelNumber(levelNumber);
        SceneManager.LoadScene(buildIndex);
    }
}
