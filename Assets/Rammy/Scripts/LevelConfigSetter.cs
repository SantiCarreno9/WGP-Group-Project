using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfigSetter : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private void Start()
    {
        //quick fix
        Time.timeScale = 1.0f;
        if(playerController != null)
        {
            var gameData = SaveManager.CurrentGameData;
            playerController.HealthModule.SetHealth(gameData.playerHealth);
            if (gameData.hasPosition)
            {
                var player = GameManager.Instance.Player;
                var characterController = player.GetComponent<CharacterController>();
                characterController.enabled = false;
                player.transform.position = gameData.playerPosition;
                characterController.enabled = true;
            }
        }
    }
}
