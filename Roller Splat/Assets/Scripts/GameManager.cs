using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Properties...
    private GroundPiece[] allGroundPieces;//Array containing all ground pieces present in a scene
    public static GameManager singleton;
    public bool isFinished;
    public Text youWinText;
    public Image MainMenuUI;///
    public Image GoBackUI;///

    private void Awake()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();//Get and store all ground pieces present in the scene...
        if (!singleton)
        {
            singleton = this;
            if(singleton != this)
            {
                Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Load next scene when current scene has been solved...
    private void LoadNextScene()
    {
        if (!isFinished)
            return;
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            youWinText.gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //GoBackUI.gameObject.SetActive(true);
            LoadGoBackUI(true);
            youWinText.gameObject.SetActive(false);
        }

    }
    //NB : Called in GroundPiece.cs...
    public void CheckComplete()
    {
        //Check if all ground pieces have been solved...
        for (int i = 0; i < allGroundPieces.Length ; i++)
        {
            if(allGroundPieces[i].isColored == false)
            {
                Debug.Log("Not all pieces are colored");
                isFinished = false;
                break;
            }
            else
            {
                isFinished = true;
            }
        }
        //If isFinished is true...
        if(isFinished)
        {
            Debug.Log("You Win!!!");
            youWinText.gameObject.SetActive(true);
            LoadNextScene(); //Call Load Next Scene function...
        }
    }

    public void LoadMainMenu(bool canLoad)///
    {
        MainMenuUI.gameObject.SetActive(canLoad);
    }
    public void LoadGoBackUI(bool canLoad)///
    {
        GoBackUI.gameObject.SetActive(canLoad);
        if (!canLoad)
        {
            SceneManager.LoadScene(0);
            //LoadMainMenu(true);
        }
    }
    public void QuitGame()///
    {
        Application.Quit();
    }
}
