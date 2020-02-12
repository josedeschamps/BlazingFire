using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }


    enum GameModeSetting
    {
        BasicMode,
        MediumMode,
        HardMode
    }


    public float _currency
    {
        get { return currency; }
        set { currency = value; }
    }




    [Header("Game Currency & Game Time")]
    public float currency;

    [Tooltip("This hold all the assets in the game")]
    [Header("Asset Holder")]
    public GameObject[] basicEnemyPrefab;
    public GameObject[] shootingEnemyPrefab;
    public GameObject[] civilianPrefab;

    [Tooltip("Controls & asset holder for waves and Prefabs")]
    [Header("Wave Setting & Holder")]
    public int totalWaveAmount = 100;
    [HideInInspector]
    public List<GameObject> startWave;
    public Transform spawnLocation;

    [Tooltip("This hold all the UI Elements of the game")]
    [Header("HUD Setting")]
    public Slider slideCountDownTime;
    public float _gameTime
    {
        get { return gameTime; }
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            else if (value >= 50)
            {
                value = 50;
            }
            else
            {
                gameTime = value;
            }



        }
    }
    public float gameTime = 100f;

    //[HideInInspector]
    public bool buttonPressed;
    public bool pauseWave = false;


    private void Start()
    {
        startWave = new List<GameObject>();
        DOTween.Init();
        LoadStartWave();
        GameSystem(GameModeSetting.BasicMode);


    }
    private void Update()
    {

        GameTimeDisplaySystem();
    }

    private void GameSystem(GameModeSetting setting)
    {

        switch (setting)
        {
            case GameModeSetting.BasicMode:
                SpawnWave();
                break;
            case GameModeSetting.MediumMode:
                break;
            case GameModeSetting.HardMode:
                break;
            default:
                break;
        }

    }
    private void GameTimeDisplaySystem()
    {
        gameTime -= Time.deltaTime;

        if (gameTime < 0)
        {
            //do something;
        }

        slideCountDownTime.value = gameTime;

    }

    private void SpawnWave()
    {
        StartCoroutine(SpawningWave());
    }
    IEnumerator SpawningWave()
    {
        yield return new WaitForSeconds(2f);

        WaitForSeconds wait = new WaitForSeconds(1f);
        for (int i = 0; i < totalWaveAmount; i++)
        {
            while (pauseWave)
            {
                yield return null;
            }
            Instantiate(startWave[i], spawnLocation.position, spawnLocation.rotation);
            yield return wait;
        }

    
           

    }

    private void LoadStartWave()
    {

        for (int i = 0; i < totalWaveAmount; i++)
        {
            GameObject obj = basicEnemyPrefab[Random.Range(0, 3)];
            startWave.Add(obj);
        }

    }



}
