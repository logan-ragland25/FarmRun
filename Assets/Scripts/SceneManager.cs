using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//using Mono.Reflection;

public class SceneManager : MonoBehaviour
{
    //GameManager
    [SerializeField] GameManager gameManager;
    //Headers
    [SerializeField] TextMeshProUGUI chooseYourLevel;
    [SerializeField] TextMeshProUGUI requirementsHeader;

    //Title Screen
    [SerializeField] GameObject levels;
    //[SerializeField] GameObject levelInfo;

    [SerializeField] Button StageOneButton;
    [SerializeField] Button StageTwoButton;
    [SerializeField] Button StageThreeButton;
    [SerializeField] Button StageFourButton;
    [SerializeField] Button StageFiveButton;
    [SerializeField] Button StageSixButton;

    [SerializeField] GameObject stars;
    [SerializeField] GameObject[] levelOneStars = new GameObject[3];
    [SerializeField] GameObject[] levelTwoStars = new GameObject[3];
    [SerializeField] GameObject[] levelThreeStars = new GameObject[3];
    [SerializeField] GameObject[] levelFourStars = new GameObject[3];
    [SerializeField] GameObject[] levelFiveStars = new GameObject[3];
    [SerializeField] GameObject[] levelSixStars = new GameObject[3];

    //Level Info
    [SerializeField] GameObject levelPopUp;

    [SerializeField] TextMeshProUGUI stageOneReq;
    [SerializeField] TextMeshProUGUI stageTwoReq;
    [SerializeField] TextMeshProUGUI stageThreeReq;
    [SerializeField] TextMeshProUGUI stageFourReq;
    [SerializeField] TextMeshProUGUI stageFiveReq;
    [SerializeField] TextMeshProUGUI stageSixReq;

    [SerializeField] GameObject stageOneReqObj;
    [SerializeField] GameObject stageTwoReqObj;
    [SerializeField] GameObject stageThreeReqObj;
    [SerializeField] GameObject stageFourReqObj;
    [SerializeField] GameObject stageFiveReqObj;
    [SerializeField] GameObject stageSixReqObj;

    [SerializeField] Button goBack;
    [SerializeField] Button startLevel;

    [SerializeField] Material gold;

    //Instructions
    [SerializeField] GameObject instructions;
    [SerializeField] Button openInstructions;
    [SerializeField] Button closeInstructions;
    [SerializeField] TextMeshProUGUI instructionsHeader;
    [SerializeField] TextMeshProUGUI instructionsText;

    int activeScene = 0;
    bool opened;

    //SceneManager sceneManager = new SceneManager();

    // Start is called before the first frame update
    void Start()
    {
        LoadMenu();
        opened = false;
    }

    void LoadMenu()
    {
        levels.SetActive(true);
        stars.SetActive(true);
        levelPopUp.SetActive(false);
        stageOneReqObj.SetActive(false);
        stageTwoReqObj.SetActive(false);
        stageThreeReqObj.SetActive(false);
        stageFourReqObj.SetActive(false);
        stageFiveReqObj.SetActive(false);
        stageSixReqObj.SetActive(false);
        instructions.SetActive(false);

        //Set stars to gold if stars are earned
        //Level 1
        if (GameManager.LevelOneStarsEarned >= 1)
        {
            levelOneStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelOneStarsEarned >= 2)
        {
            levelOneStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelOneStarsEarned >= 3)
        {
            levelOneStars[2].GetComponent<MeshRenderer>().material = gold;
        }

        //Level 2
        if (GameManager.LevelTwoStarsEarned >= 1)
        {
            levelTwoStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelTwoStarsEarned >= 2)
        {
            levelTwoStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelTwoStarsEarned >= 3)
        {
            levelTwoStars[2].GetComponent<MeshRenderer>().material = gold;
        }

        //Level 3
        if (GameManager.LevelThreeStarsEarned >= 1)
        {
            levelThreeStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelThreeStarsEarned >= 2)
        {
            levelThreeStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelThreeStarsEarned >= 3)
        {
            levelThreeStars[2].GetComponent<MeshRenderer>().material = gold;
        }

        //Level 4
        if (GameManager.LevelFourStarsEarned >= 1)
        {
            levelFourStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelFourStarsEarned >= 2)
        {
            levelFourStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelFourStarsEarned >= 3)
        {
            levelFourStars[2].GetComponent<MeshRenderer>().material = gold;
        }

        //Level 5
        if (GameManager.LevelFiveStarsEarned >= 1)
        {
            levelFiveStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelFiveStarsEarned >= 2)
        {
            levelFiveStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelFiveStarsEarned >= 3)
        {
            levelFiveStars[2].GetComponent<MeshRenderer>().material = gold;
        }

        //Level 6
        if (GameManager.LevelSixStarsEarned >= 1)
        {
            levelSixStars[0].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelSixStarsEarned >= 2)
        {
            levelSixStars[1].GetComponent<MeshRenderer>().material = gold;
        }
        if (GameManager.LevelSixStarsEarned >= 3)
        {
            levelSixStars[2].GetComponent<MeshRenderer>().material = gold;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StageOneButton.GetComponent<Button>().onClick.AddListener(() => StartGame(1));
        StageTwoButton.GetComponent<Button>().onClick.AddListener(() => StartGame(2));
        StageThreeButton.GetComponent<Button>().onClick.AddListener(() => StartGame(3));
        StageFourButton.GetComponent<Button>().onClick.AddListener(() => StartGame(4));
        StageFiveButton.GetComponent<Button>().onClick.AddListener(() => StartGame(5));
        StageSixButton.GetComponent<Button>().onClick.AddListener(() => StartGame(6));
        goBack.GetComponent<Button>().onClick.AddListener(() => LoadMenu());
        startLevel.GetComponent<Button>().onClick.AddListener(() => OpenScene(activeScene));
        openInstructions.GetComponent<Button>().onClick.AddListener(() => Instructions());
        closeInstructions.GetComponent<Button>().onClick.AddListener(() => LoadMenu());
    }

    public void StartGame(int scene)
    {
        Debug.Log("scene " + scene + " button clicked");
        activeScene = scene;
        levels.SetActive(false);
        stars.SetActive(false);
        levelPopUp.SetActive(true);

        if (scene == 1)
        {
            stageOneReqObj.SetActive(true);
        }
        else if (scene == 2)
        {
            stageTwoReqObj.SetActive(true);
        }
        else if (scene == 3)
        {
            stageThreeReqObj.SetActive(true);
        }
        else if (scene == 4)
        {
            stageFourReqObj.SetActive(true);
        }
        else if (scene == 5)
        {
            stageFiveReqObj.SetActive(true);
        }
        else if (scene == 6)
        {
            stageSixReqObj.SetActive(true);
        }
    }

    public void OpenScene(int scene)
    {
        Debug.Log("opening scene " + scene);
        if (!opened)
        {
           UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level" + scene);
           opened = true;
        }
    }

    public void Instructions ()
    {
        levels.SetActive(false);
        stars.SetActive(false);
        instructions.SetActive(true);
    }
}
