using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Define the different states of the game
    public static GameManager instance;

    public int index;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }
    // Store the current state of the game;
    public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultScreen;
    public GameObject levelUpScreen;

    [Header("Current Stat Display")]
    public Text currentHealtDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;
    // LVL Display
    public Text currentLevelDisplay;


    [Header("Result Stats Display")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;

    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveUI = new List<Image>(6);

    private float timeSurvived;

    public bool isOver = false;
    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                timeSurvived = timeSurvived + Time.deltaTime;
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isOver)
                {
                    isOver = true;
                    //Time.timeScale = 0f; //Stpo the game entirely 
                    //Debug.Log("Game is over");
                    //StartCoroutine(_DisplayResults());
                }
                break;
            default:
                Debug.LogWarning("State does not exist");
                break;

        }

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Extra" + this + "deleted");
            Destroy(gameObject);
        }
        DisableScreens();
    }

    public void Start()
    {
        timeSurvived = 0f;
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;

            ChangeState(GameState.Paused);
            pauseScreen.SetActive(true);
            Time.timeScale = 0f; // stop the game
            Debug.Log("Games is paused");
        }
    }
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            // Debug.Log("Game is resumed");
        }
        if(currentState == GameState.LevelUp)
        {
            ChangeState(GameState.Gameplay);
            levelUpScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LevelUp()
    {
        // Debug.Log("Level up");
        ChangeState(GameState.LevelUp);
        levelUpScreen.SetActive(true);
        Time.timeScale = 0f;
    }


    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }
    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
    //IEnumerator _DisplayResults()
    //{
    //    yield return new WaitForSeconds(1f);
    //    resultScreen.SetActive(true);

    //}
    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {

        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }
    public void AssignLevelReachedUI(int levell)
    {
        levelReachedDisplay.text = levell.ToString();
    }
    public void AssignTimeSurvived()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeSurvived);
        timeSurvivedDisplay.text = timeSpan.ToString(@"mm\:ss");
    }

    public void AssignChosenWeaponAndPassiveItemUI(List<Image> chosenWeaponData, List<Image> chosenPassiveItemData)
    {
        if (chosenWeaponData.Count != chosenWeaponsUI.Count || chosenPassiveItemData.Count != chosenPassiveUI.Count)
        {
            Debug.Log("Chosen weapons and passive items data list have diferent lengths");
            return;
        }
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }
        for (int i = 0; i < chosenPassiveUI.Count; i++)
        {
            if (chosenPassiveItemData[i].sprite)
            {
                chosenPassiveUI[i].enabled = true;
                chosenPassiveUI[i].sprite = chosenPassiveItemData[i].sprite;
            }
            else
            {
                chosenPassiveUI[i].enabled = false;
            }
        }
    }
    public GameObject getResultScreen()
    {
        return resultScreen;
    }
    public void StopTime()
    {
        Time.timeScale = 0f;
    }
}
