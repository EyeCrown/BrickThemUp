using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int score { get; private set; }
    public int maxHealth = 3;
    public int multiplier = 1;

    private TextMeshProUGUI scoreText;

    [SerializeField] private List<Transform> spawnPositions;

    #region EVENTS
    public UnityEvent<int> ScoreChange;
    public UnityEvent<int> PlayerDie;
    #endregion

    public GameObject[] players { get; private set; }

    private PlayerInputManager playerInputManager;

    private string gameScene = "PlayerTestScene";

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        ScoreChange.AddListener(ScoreChangeHandler);
        PlayerDie.AddListener(PlayerDieHandler);
        players = new GameObject[2];
    }

    void Start()
    {
        scoreText = GameObject.Find("UI_ScoreText").GetComponent<TextMeshProUGUI>();
        StartGame();

        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.EnableJoining();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (Globals.gameState != Globals.GameState.OnMenu) return; // Re-activated player on respawn triggers OnPlayerJoined

        Debug.Log("Player " + playerInput.playerIndex + " joined");
        players[playerInput.playerIndex] = playerInput.gameObject;
        players[playerInput.playerIndex].transform.position = spawnPositions[playerInput.playerIndex].position;
        players[playerInput.playerIndex].GetComponent<PlayerController>().Initialize(playerInput.playerIndex);

        if (playerInputManager.playerCount == playerInputManager.maxPlayerCount)
            ChangeScene();
    }

    public void ChangeScene()
    {
        playerInputManager.DisableJoining();
        Debug.Log("Change scene");

        SceneManager.LoadScene(gameScene);
    }


    public GameObject GetOtherPlayer(int playerId)
    {
        if (players.Length <= 1)
            return null;
        else
            return players[GetOtherPlayerId(playerId)];
    }

    private int GetOtherPlayerId(int myId)
    {
        return myId == 0 ? 1 : 0;
    }

    
    private void StartGame()
    {
        Debug.Log("___GAME START___");
        score = 0;
        scoreText.text = score.ToString();
    }

    private void GameOver()
    {
        Debug.Log("___GAME OVER___");
        Debug.Log("Score: " + score);
        //TODO: Make game over
    }



    #region EVENT HANDLERS
    private void ScoreChangeHandler(int points)
    {
        Debug.Log("GameManager: Score += " + points);
        score += points;
        scoreText.text = score.ToString();
    }

    private void PlayerDieHandler(int idPlayer)
    {
        if (!players[idPlayer].GetComponent<PlayerController>().isAlive 
            && !players[GetOtherPlayerId(idPlayer)].GetComponent<PlayerController>().isAlive)
        {
            GameOver();
        }
    }
    #endregion
}
