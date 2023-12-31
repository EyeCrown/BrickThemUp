using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;


    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        
        playerInputManager.EnableJoining();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (Globals.gameState != Globals.GameState.OnMenu) return; // Re-activated player on respawn triggers OnPlayerJoined

        Debug.Log("Player " + playerInput.playerIndex + " joined");

        if (playerInputManager.playerCount == playerInputManager.maxPlayerCount)
            ChangeScene();
    }

    public void ChangeScene()
    {
        playerInputManager.DisableJoining();
        Debug.Log("Change scene");
        string gameScene = "PlayerTestScene";
        SceneManager.LoadScene(gameScene);
    }
}
