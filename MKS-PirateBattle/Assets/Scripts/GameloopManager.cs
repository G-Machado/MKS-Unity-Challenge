using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameloopManager : MonoBehaviour
{
    public static GameloopManager instance;
    private void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        MENU,
        PLAY,
        WIN,
        LOSE
    }
    public GameState currentState;
    public IslandsManager islandsManager;

    [Header("Session Variables")]
    public float sessionDuration = 60;
    private float sessionStartTime;
    public Transform canvas;

    [Header("Enemy Session Variables")]
    public float enemySpawnInterval;
    private float lastSpawnTime;
    public GameObject[] possibleEnemies;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.MENU;
        enemySpawnInterval = PlayerPrefs.GetFloat("enemySpawnInterval", 5f);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GameState.PLAY)
        {
            if(Time.time - lastSpawnTime > enemySpawnInterval)
                SpawnRandomEnemy();

            if(Time.time - sessionStartTime > sessionDuration)
            {
                GameWin();
            }
        }
    }

    public void SpawnRandomEnemy()
    {
        lastSpawnTime = Time.time;
        Vector3 randomPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        GameObject enemyClone = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Length)], randomPosition, Quaternion.identity);
    }

    public void StartGame()
    {
        currentState = GameState.PLAY;
        sessionStartTime = Time.time;

        islandsManager.SpawnRandomGroup();
    }

    public void GameWin()
    {
        currentState = GameState.WIN;
        // Trigger Menu win 
    }

    public void GameLose()
    {
        currentState = GameState.LOSE;
        // Trigger menu lose
    }

    public void StartMenu()
    {
        currentState = GameState.MENU;
        // Trigger main menu
    }
}
