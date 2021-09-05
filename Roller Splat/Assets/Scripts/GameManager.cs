using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Properties...
    private GroundPiece[] allGroundPieces;
    public static GameManager singleton;
    public bool isFinished;
    public Text youWinText;
    private void Awake()
    {
        //SceneManager.LoadScene(0);
        allGroundPieces = FindObjectsOfType<GroundPiece>();
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

    private void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void CheckComplete()
    {
        //Debug.Log("Check complete function is being called");
        //isFinished = true;
        for(int i = 0; i < allGroundPieces.Length ; i++)
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
        if(isFinished)
        {
            Debug.Log("You Win!!!");
            youWinText.gameObject.SetActive(true);
            LoadNextScene();
        }
    }
}
