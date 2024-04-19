using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor.SearchService;
using System;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Button startGameText;
    [SerializeField] Button goBack;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI TimeRemaining;
    [SerializeField] Camera titleCamera;
    [SerializeField] Camera gameCamera;

    //public GameObject pauseScreen;
    [SerializeField] GameManager gameManager;
    //[SerializeField] GameObject sceneManager;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject endScreen;
    public bool isGameActive;
    private int score;
    private int lives;
    private bool paused;
    public GameObject player;
    private Rigidbody playerRb;
    public List<GameObject> targets;
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] GameObject mountain;

    [SerializeField] Vector3 newGravity;

    public static int LevelOneBestScore = 0;
    public static int LevelOneStarsEarned = 0;

    public static int LevelTwoBestScore = 0;
    public static int LevelTwoStarsEarned = 0;

    public static int LevelThreeBestScore = 0;
    public static int LevelThreeStarsEarned = 0;

    public static int LevelFourBestScore = 0;
    public static int LevelFourStarsEarned = 0;

    public static int LevelFiveBestScore = 0;
    public static int LevelFiveStarsEarned = 0;

    public static int LevelSixBestScore = 0;
    public static int LevelSixStarsEarned = 0;

    public static int HorsesHit = 0;

    public int level;

    private bool opened;

    private void Awake()
    {
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (levelName.Equals("Start") == false)
        {
            level = int.Parse(levelName.Substring(5));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        if (level != 0)
        {
            player.transform.position = new Vector3(0, 0.1f, 1.75f);
            isGameActive = false;
            titleCamera.enabled = true;
            gameCamera.enabled = false;
            SetLives(3);
            UpdateScore(0);
            playerRb = player.GetComponent<Rigidbody>();
            playerRb.freezeRotation = true;
            endScreen.SetActive(false);
            goBack.gameObject.SetActive(true);
            if (level == 1)
            {

            }
            else if (level == 2)
            {
                mountain.SetActive(true);
            }
            else if (level == 3)
            {

            }
            else if (level == 4)
            {

            }
            else if (level == 5)
            {

            }
            else if (level == 6)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (level != 0)
        {
            startGameText.onClick.AddListener(StartGame);
            restartButton.onClick.AddListener(RestartGame);
            goBack.onClick.AddListener(GoBack);

        }
    }
    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        SetHorse();

        titleCamera.enabled = false;
        gameCamera.enabled = true;

        endScreen.SetActive(false);
        titleScreen.SetActive(false);

        goBack.gameObject.SetActive(false);

        playerRb.transform.rotation = new Quaternion(0, 0, 0, 0);

        titleCamera.GetComponent<AudioListener>().enabled = !titleCamera.GetComponent<AudioListener>().enabled;
        gameCamera.GetComponent<AudioListener>().enabled = !gameCamera.GetComponent<AudioListener>().enabled;
        titleCamera.GetComponent<AudioSource>().enabled = !titleCamera.GetComponent<AudioSource>().enabled;
        gameCamera.GetComponent<AudioSource>().enabled = !gameCamera.GetComponent<AudioSource>().enabled;

        if (level == 2)
        {
            mountain.SetActive(false);
        }
        Physics.gravity = newGravity;
    }
    public void SetScore(int scoreAmount)
    {
        score = scoreAmount;
        scoreText.text = "Score: " + score;
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void SetLives(int livesToChange)
    {
        lives = livesToChange;
        livesText.text = "Lives: " + lives;
    }
    public void AddLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
    }

    public void SubtractLives(int livesToChange)
    {
        if (isInvincible)
        {
            return;
        }
        lives -= livesToChange;
        if (lives < 0)
        {
            lives = 0;
        }
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
        StartCoroutine(BecomeTemporarilyInvincible());
    }
    public void SetHorse()
    {
        HorsesHit = 0;
    }
    public void UpdateHorse()
    {
        HorsesHit += 1;
    }

    public void GameOver()
    {
        SaveScore();
        endScreen.SetActive(true);
        isGameActive = false;
        goBack.gameObject.SetActive(true);
        titleCamera.GetComponent<AudioListener>().enabled = !titleCamera.GetComponent<AudioListener>().enabled;
        gameCamera.GetComponent<AudioListener>().enabled = !gameCamera.GetComponent<AudioListener>().enabled;
        titleCamera.GetComponent<AudioSource>().enabled = !titleCamera.GetComponent<AudioSource>().enabled;
        gameCamera.GetComponent<AudioSource>().enabled = !gameCamera.GetComponent<AudioSource>().enabled;
    }

    public void RestartGame()
    {
        isGameActive = false;
        titleCamera.enabled = true;
        gameCamera.enabled = false;
        endScreen.SetActive(false);
        titleScreen.SetActive(true);

        playerRb.velocity = new Vector3(0, 0, 0);
        player.transform.position = new Vector3(0, 0.1f, 1.75f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        SetLives(3);
        SetScore(0);

        if (level == 1)
        {

        }
        if (level == 2)
        {
            mountain.SetActive(true);
        }
        if (level == 3)
        {

        }
        if (level == 4)
        {

        }
        if (level == 5)
        {

        }
        if (level == 6)
        {

        }
    }
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;
    }

    public void SaveScore()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
        {
            if (score > LevelOneBestScore)
            {
                LevelOneBestScore = score;
            }
            if (score >= 500 && LevelOneStarsEarned < 1)
            {
                LevelOneStarsEarned = 1;
                Debug.Log(LevelOneStarsEarned + "a");
            }
            if (score >= 1500 && LevelOneStarsEarned < 2)
            {
                LevelOneStarsEarned = 2;
                Debug.Log(LevelOneStarsEarned + "b");
            }
            if (score >= 2500 && LevelOneStarsEarned < 3)
            {
                LevelOneStarsEarned = 3;
                Debug.Log(LevelOneStarsEarned + "c");
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level2")
        {
            if (score > LevelTwoBestScore)
            {
                LevelTwoBestScore = score;
            }
            if (score >= 1000 && LevelTwoStarsEarned < 1)
            {
                LevelTwoStarsEarned = 1;
                Debug.Log(LevelTwoStarsEarned + "aa");
            }
            if (score >= 2000 && LevelTwoStarsEarned < 2)
            {
                LevelTwoStarsEarned = 2;
                Debug.Log(LevelTwoStarsEarned + "bb");
            }
            if (score >= 3000 && LevelTwoStarsEarned < 3/* && lives >= 2*/)
            {
                LevelTwoStarsEarned = 3;
                Debug.Log(LevelTwoStarsEarned + "cc");
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level3")
        {
            if (score > LevelThreeBestScore)
            {
                LevelThreeBestScore = score;
            }
            if (score >= 2000 && LevelThreeStarsEarned < 1)
            {
                LevelThreeStarsEarned = 1;
                Debug.Log(LevelThreeStarsEarned + "aaa");
            }
            if (score >= 2000 && LevelThreeStarsEarned < 2)
            {
                LevelThreeStarsEarned = 2;
                Debug.Log(LevelThreeStarsEarned + "bbb");
            }
            if (score >= 3000 && LevelThreeStarsEarned < 3/* && lives >= 2*/)
            {
                LevelThreeStarsEarned = 3;
                Debug.Log(LevelThreeStarsEarned + "ccc");
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level4")
        {
            if (score > LevelFourBestScore)
            {
                LevelFourBestScore = score;
            }
            if (score >= 3000 && LevelFourStarsEarned < 1)
            {
                LevelFourStarsEarned = 1;
                Debug.Log(LevelFourStarsEarned + "aaaa");
            }
            if (score >= 6000 && LevelFourStarsEarned < 2)
            {
                LevelFourStarsEarned = 2;
                Debug.Log(LevelFourStarsEarned + "bbbb");
            }
            if (score >= 9000 && LevelFourStarsEarned < 3/* && lives >= 3*/)
            {
                LevelFourStarsEarned = 3;
                Debug.Log(LevelFourStarsEarned + "cccc");
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level5")
        {
            if (score > LevelFiveBestScore)
            {
                LevelFiveBestScore = score;
            }
            if (score >= 4000 && LevelFiveStarsEarned < 1)
            {
                LevelFiveStarsEarned = 1;
                Debug.Log(LevelFiveStarsEarned + "aaaaa");
            }
            if (score >= 8000 && LevelFiveStarsEarned < 2)
            {
                LevelFiveStarsEarned = 2;
                Debug.Log(LevelFiveStarsEarned + "bbbbb");
            }
            if (score >= 12000 && LevelFiveStarsEarned < 3 && HorsesHit > 15/* && lives >= 4*/)
            {
                LevelFiveStarsEarned = 3;
                Debug.Log(LevelFiveStarsEarned + "ccccc");
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level6")
        {
            if (score > LevelSixBestScore)
            {
                LevelSixBestScore = score;
            }
            if (score >= 5000 && LevelSixStarsEarned < 1)
            {
                LevelSixStarsEarned = 1;
                Debug.Log(LevelSixStarsEarned + "aaaaaa");
            }
            if (score >= 10000 && LevelSixStarsEarned < 2)
            {
                LevelSixStarsEarned = 2;
                Debug.Log(LevelSixStarsEarned + "bbbbbb");
            }
            if (score >= 15000 && LevelSixStarsEarned < 3 && HorsesHit > 30)
            {
                LevelSixStarsEarned = 3;
                Debug.Log(LevelSixStarsEarned + "cccccc");
            }
        }
    }

    public void GoBack()
    {
        Debug.Log("Loading Start");
        while (!opened)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Start");
            opened = true;
        }
    }

}
