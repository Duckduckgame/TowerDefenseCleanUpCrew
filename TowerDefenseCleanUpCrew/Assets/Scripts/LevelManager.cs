using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum GameState {Siege, Clean, Win, Loss }
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
    float cameraSiegeSize;
    [SerializeField]
    int RoundsLeft = 5;

    [SerializeField]
    AudioClip[] musicClips;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        cameraSiegePos = Camera.main.transform.position;
        cameraSiegeSize = Camera.main.orthographicSize;
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

        if (prizesTaken == prizes.Length && crntState != GameState.Loss && crntState != GameState.Win)
        {
            crntState = GameState.Loss;
            LoseGame();
        }
        if(RoundsLeft == 0 && crntState != GameState.Win && crntState != GameState.Loss)
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
        Camera.main.orthographicSize = cameraSiegeSize;
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
        if (RoundsLeft != 0)
        {
            source.clip = musicClips[1];
            source.Play();
            StartCoroutine(UIManager.FlashText(UIManager.cleanStart, 1f));
            StartCoroutine(UIManager.ShowPanel(UIManager.startCleanPanel, 5f));
            StartCoroutine(ResetTimerAfterTime(5));
            timer = 0;
            crntState = GameState.Clean;
            player.SetActive(true);
        }
    }

    void StopClean()
    {
        timer = 0;
        StartSiege();
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        source.Stop();
        source.clip = musicClips[3];
        source.loop = false;
        source.Play();
        UIManager.defeat.text = "Defeat! Rounds Survived: " + (5 - RoundsLeft).ToString();
        StartCoroutine(UIManager.FlashText(UIManager.defeat, 0.5f));
        StartCoroutine(LoadToMenu());
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        source.Stop();
        source.clip = musicClips[2];
        source.loop = false;
        source.Play();
        UIManager.victory.text = "Victory! Corpses Collected: " + playerControler.corpseCount.ToString();
        StartCoroutine(UIManager.FlashText(UIManager.victory, 0.5f));
        StartCoroutine(LoadToMenu());

    }

    public void FlashFixText()
    {
        //StartCoroutine(UIManager.FlashText(UIManager.fixText, 0.1f));
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

    IEnumerator ResetTimerAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        timer = 0;
    }

    IEnumerator LoadToMenu()
    {
      yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1;
      UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
