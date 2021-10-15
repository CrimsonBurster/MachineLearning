using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text cowText, alienText;
    public int cowCounter, alienCounter;

    public GameObject gameOver, winScreen;
    public GameObject cow;
    public int numCowWanted;
    public const float AreaDiameter =  300f;
    public List<Cow> Cows { get; private set; }

    private void Awake()
    {
        instance = this;
        Cows = new List<Cow>();
       
        
    }

    void Start()
    {
         CowSpawn();
        var aliens = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject alien in aliens)
        {
            alienCounter++;
        }
        //cows = GameObject.FindGameObjectsWithTag("reward");
        //foreach (GameObject cow in cows)
        //{
        //    cowCounter++;
        //}
    }

    
    private void LateUpdate()
    {
        cowText.text = "COWS LEFT: " + cowCounter.ToString();
        alienText.text = "ALIENS LEFT: " + alienCounter.ToString();
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

    public void AlienLost()
    {
        alienCounter--;
        if(alienCounter <= 0)
        {
            WonGame();
        }
        
    }

    private void WonGame()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void CowSpawn()
    {
        for (var i = 1; i <= numCowWanted; i++)
        {
            var position = new Vector3(transform.position.x + Random.Range(-150f, 150f), 5.5f, transform.position.z + Random.Range(-150f, 150f));
            var newCow = Instantiate(cow, position, transform.rotation);
            newCow.transform.parent = gameObject.transform;
            Cow madeCow = newCow.GetComponent<Cow>();
            Cows.Add(madeCow);
            cowCounter++;
            //Debug.Log("I made a cow" + cowCounter);
           
        }

        //Invoke("cowSpawn", Random(new Vector3(transform.position.x + Random.Range(-150f, 150f), 5.5f, transform.position.z + Random.Range(-150f, 150f));
    }
}
