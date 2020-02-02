using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum GameState {Siege, Clean, Win }
    public GameState crntState;


    GameObject player;
    PlayerControler playerControler;
    EnemySpawn[] enemySpawns;
    FixingHandler[] fixableObjects;
    PrizeHandler[] prizes;
    public int prizesTaken;
    public float timer;
    UIManager UIManager;
    int roundCount = 0;
    Vector3 cameraSiegePos;
    [SerializeField]
    int RoundsLeft = 5;

    [SerializeField]
    AudioClip[] musicClips;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        cameraSiegePos = Camera.main.transform.position;
        prizes = FindObjectsOfType<PrizeHandler>();
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

        UIManager.timer.text =  Mathf.Floor(timer*-1+20).ToString();
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

        if (prizesTaken == prizes.Length)
            LoseGame();

        if(RoundsLeft == 0 && crntState != GameState.Win)
        {
            crntState = GameState.Win;
            WinGame();
        }
    }

    void StartSiege()
    {
        source.clip = musicClips[0];
        source.Play();
        Camera.main.transform.position = cameraSiegePos;
        timer = 0;
        UIManager.siegeStart.text = "The Siege Starts. Rounds left: " + RoundsLeft.ToString();
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
        RoundsLeft--;
        StartClean();
    }

    void StartClean()
    {
        source.clip = musicClips[1];
        source.Play();
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

    public void WinGame()
    {
        Time.timeScale = 0;
        //StartCoroutine(WinGameVisuals());
        source.Stop();
        source.clip = musicClips[2];
        source.loop = false;
        source.Play();
        //Time.timeScale = 0;
        StartCoroutine(UIManager.FlashText(UIManager.victory, 0.5f));
        //yield return new WaitForSecondsRealtime(source.clip.length);
        

    }

    public void FlashFixText()
    {
        StartCoroutine(UIManager.FlashText(UIManager.fixText, 0.1f));
    }

    IEnumerator WinGameVisuals()
    {
        source.clip = musicClips[2];
        source.Play();
        //Time.timeScale = 0;
        StartCoroutine(UIManager.FlashText(UIManager.victory, 2f));
        //yield return new WaitForSecondsRealtime(source.clip.length);
        Time.timeScale = 1;
        yield return null;
    }
}
