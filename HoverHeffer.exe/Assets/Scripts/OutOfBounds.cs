using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfBounds : MonoBehaviour
{
    public GameObject outPanel;
    public Text outTimeText;

    public GameObject player;
    private Vector3 mapCenter;

    private float outTimer;
    public bool outOfBounds;
    // Start is called before the first frame update
    void Start()
    {
        mapCenter = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(outPanel.activeSelf == true)
        {
            outTimer += Time.deltaTime;
            outTimeText.text = outTimer.ToString("F2");

            if(outTimer >= 3f)
            {
                player.transform.position = mapCenter;
                outOfBounds = false;
                outPanel.SetActive(false);
                outTimer = 0f;
            }
        }
        else
        {
            outTimer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outPanel.SetActive(false);
        }
        //if(other.gameObject.tag == "Player" && !outOfBounds)
        //{
        //    Debug.Log("player touched");
        //    outPanel.SetActive(true);
        //    outOfBounds = true;
        //}

        //else if (other.gameObject.tag == "Player" && outOfBounds)
        //{
        //    outPanel.SetActive(false);
        //    outOfBounds = false;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            outPanel.SetActive(true);
        }
    }
}
