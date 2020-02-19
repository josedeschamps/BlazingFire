using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    public int _score
    {
        get { return score; }
        set { score = value; }
    }




    [Header("Game Currency & Game Time")]
    [Tooltip("Attribute Setting of the Game")]
    public float currency = 0;
    public int score = 0;
    public int user = 0;


    [Header("Asset Holder")]
    [Tooltip("This hold all the assets in the game")]
    public GameObject[] allgameAssetsPrefab;


    [Header("PlayerSetting")]
    [Tooltip("Holds All The Player Info and Objects")]
    public Transform Player;
    public float duckSpeed;


    [Header("Wave Setting & Holder")]
    [Tooltip("Controls & asset holder for waves and Prefabs")]
    public int totalWaveAmount = 100;
    [HideInInspector]
    public List<GameObject> startWave;
    public Transform spawnLocation;

    [Tooltip("This hold all the UI Elements of the game")]
    [Header("HUD Setting")]
    public Button startButton;
    private bool hasBeenClicked = false;
    public Slider slideCountDownTime;
    public Image sliderColorBar;
    private bool resetGameTimer = true;
    public TMP_Text scoreText;
    public float _gameTime
    {
        get { return gameTime; }
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            else if (value >= 25)
            {
                value = 25;
            }
            else
            {
                gameTime = value;
            }



        }
    }
    [Range(0,25)]
    public float gameTime = 25f;

    [Header("Game Over Setting")]
    public GameObject gameoverDisplay;
    public TMP_Text killscoreText;
    public int totalKillScore
    {
        get { return _killScore; }
        set
        {
            _killScore = value;
            killscoreText.text = _killScore.ToString();
        }

    }
    private int _killScore;


    [HideInInspector]
    public bool buttonPressed;
    public bool pauseWave = false;

    //Touch input
    private Touch touch;
    private Vector2 beginTouchPostion, endTouchPosition;

 



    private void Start()
    {
        startWave = new List<GameObject>();
        scoreText.text = _score.ToString();
        LoadStartWave();
        DOTween.Init();



    }
    private void Update()
    {
        TouchInputSystem();
        GameTimeDisplaySystem();
    }



    //game mode setting
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
    //Game countdown display
    private void GameTimeDisplaySystem()
    {

        if (!resetGameTimer)
        {
            gameTime -= Time.deltaTime;

            if (gameTime < 0)
            {
                GameOver();
                ResetGame();
            }

            slideCountDownTime.value = gameTime;

            if (slideCountDownTime.value < 5)
            {
                sliderColorBar.color = Color.red;
            }
            else
            {

                sliderColorBar.color = new Color(0f, 198f, 255f);
            }

        }

    }
    //Mobile input setting
    private void TouchInputSystem()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    beginTouchPostion = touch.position;
                    //if (touch.position.x < Screen.width / 2)
                    //{
                    //    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    //    {
                    //        beginTouchPostion = touch.position;
                    //    }
                    //}

                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    if (beginTouchPostion != endTouchPosition)
                    {
                        PlayerDuckAction();
                    }
                    break;
            }
        }
    }
    private void PlayerDuckAction()
    {
        PerformAbility();

    }
    void PerformAbility()
    {

        Player.DOLocalMoveY(-5f, 0.25f).SetEase(Ease.OutQuad).OnComplete(ResetAbility);
    }
    void ResetAbility()
    {
        Player.DOLocalMoveY(-3f, 0.25f).SetEase(Ease.InQuad);
    }

    //Spawn system
    private void SpawnWave()
    {
        StartCoroutine(SpawningWave());
    }
    IEnumerator SpawningWave()
    {
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
            GameObject obj = allgameAssetsPrefab[Random.Range(0, 5)];
            startWave.Add(obj);
        }

    }

    //Display score
    public void ScoreDisplay()
    {
        scoreText.text = _score.ToString();
        scoreText.DOFade(255f, 0.25f).SetEase(Ease.InQuad);
        scoreText.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.25f).SetEase(Ease.InElastic).OnComplete(ResetScoreDisplayAnimation);
    }
    void ResetScoreDisplayAnimation()
    {
        scoreText.transform.DOScale(new Vector3(1f, 1f, 1), 0.25f).SetEase(Ease.OutElastic);
        scoreText.DOFade(0f, 0.25f).SetEase(Ease.OutQuad);
    }


    //Starting the game setting
    public void StartGame()
    {
        if (!hasBeenClicked)
        {
            StartCoroutine(LoadGame());
            resetGameTimer = false;
            Handheld.Vibrate();
            hasBeenClicked = true;

            if (gameoverDisplay.activeSelf == true)
            {
               
                gameoverDisplay.SetActive(false);
            }
        }
    }
    IEnumerator LoadGame()
    {
        startButton.transform.DOLocalMoveY(1000f, 0.25f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1.5f);
        GameSystem(GameModeSetting.BasicMode);
    }


    public void GameOver()
    {
        gameoverDisplay.SetActive(true);
        StopAllCoroutines();
    }

    private void ResetGame()
    {
        resetGameTimer = true;
        gameTime = 25f;
        startWave.Clear();
        LoadStartWave();
        hasBeenClicked = false;
    }

}
