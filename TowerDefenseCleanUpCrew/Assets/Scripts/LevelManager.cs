using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum GameState {Siege, Clean }
    public GameState crntState;


    GameObject player;
    PlayerControler playerControler;
    EnemySpawn[] enemySpawns;
    FixingHandler[] fixableObjects;
    public float timer;
    UIManager UIManager;
    int roundCount = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControler = player.GetComponent<PlayerControler>();
        enemySpawns = FindObjectsOfType<EnemySpawn>();
        fixableObjects = FindObjectsOfType<FixingHandler>();
        UIManager = FindObjectOfType<UIManager>();
        StartSiege();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        UIManager.timer.text = Mathf.Floor(timer).ToString();
        if(timer > 20)
        {
            if (crntState == GameState.Siege)
            {
                StopSiege();
            }
            else if (crntState == GameState.Clean)
            {
                StopClean();
            }
        }

        if(crntState == GameState.Clean)
        {
            UIManager.corpseCount.text = playerControler.corpseCount.ToString();
        }
    }

    void StartSiege()
    {
        timer = 0;
        StartCoroutine(UIManager.ShowText(UIManager.siegeStart, 2f));
        crntState = GameState.Siege;
        player.SetActive(false);
        foreach (EnemySpawn spawner in enemySpawns)
        {
            spawner.StartSiegeSpawn();
        }
    }

    void StopSiege()
    {
        timer = 0;
        foreach (FixingHandler obj in fixableObjects)
        {
            obj.attackingUnits.Clear();
        }
        roundCount++;
        StartClean();
    }

    void StartClean()
    {
        StartCoroutine(UIManager.FlashText(UIManager.cleanStart, 0.5f));
        timer = 0;
        crntState = GameState.Clean;
        player.SetActive(true);
    }

    void StopClean()
    {
        timer = 0;
        StartSiege();
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        Debug.LogError("You Lost.");
    }
}
