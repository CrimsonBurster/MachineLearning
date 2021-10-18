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
    public GameObject Alien;
    public List<Cow> Cows { get; private set; }
    /// <summary>
    /// Establishes that the instance is this script, creates a new list of cows to store the newly created gameobjects, and creates the enenmy. Is launched when the game is first started.
    /// </summary>
    private void Awake()
    {
        instance = this;
        Cows = new List<Cow>();
        Instantiate(Alien);
        
    }

    void Start()
    {
        //Spawns the cows across the map
         CowSpawn();
        var aliens = GameObject.FindGameObjectsWithTag("Enemy");
        //Finds how many aliens are alive, mostly unused in our aspect of the game
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

    /// <summary>
    /// Used to update the text to show how many cows/aliens are left alive
    /// </summary>
    private void LateUpdate()
    {
        cowText.text = "COWS LEFT: " + cowCounter.ToString();
        alienText.text = "ALIENS LEFT: " + alienCounter.ToString();
    }
    /// <summary>
    /// Shoots the code to a lost condition if there are no cows remaining
    /// </summary>
    public void CowLost()
    {
        cowCounter--;
        if(cowCounter <= 0)
        {
            GameOver();
        }
    }
    /// <summary>
    /// Loss conditions for new screen
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// Shoots the code to a win condition if there are no enemies remaining
    /// </summary>
    public void AlienLost()
    {
        alienCounter--;
        if(alienCounter <= 0)
        {
            WonGame();
        }
        
    }
    /// <summary>
    /// Win conditions for new screen
    /// </summary>
    private void WonGame()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// Allows player to play the game again
    /// </summary>
    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// Force close game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Used to spawn cows across the map, using a for loop to create a certain number of cows specified as well as find a random location in a set range to spawn them
    /// </summary>
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
