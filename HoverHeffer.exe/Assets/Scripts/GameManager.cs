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
    public GameObject cow;
    public int numCowWanted;
    public GameObject[] cowsToSpawn;

    private void Awake()
    {
        instance = this;

        CowSpawn();
    }

    void Start()
    {
        //cows = GameObject.FindGameObjectsWithTag("reward");
        //foreach (GameObject cow in cows)
        //{
        //    cowCounter++;
        //}
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

    private void CowSpawn()
    {
        for (var i = 1; i <= numCowWanted; i++)
        {
            var position = new Vector3(transform.position.x + Random.Range(-150f, 150f), 5.5f, transform.position.z + Random.Range(-150f, 150f));
            Instantiate(cow, position, transform.rotation);
            cowCounter++;
        }

        //Invoke("cowSpawn", Random(new Vector3(transform.position.x + Random.Range(-150f, 150f), 5.5f, transform.position.z + Random.Range(-150f, 150f));
    }
}
