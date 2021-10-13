using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text cowText;
    public int cowCounter;

    public GameObject gameOver;

    private void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        GameObject[] cows;
        cows = GameObject.FindGameObjectsWithTag("reward");
        foreach(GameObject cow in cows)
        {
            cowCounter++;
        }
    }

    
    private void LateUpdate()
    {
        cowText.text = "COWS LEFT: " + cowCounter.ToString();
    }

    public void CowLost()
    {
        cowCounter--;
        if(cowCounter <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
        //SceneManager.LoadSceneAsync("Main");
    }
}
