using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;

    private Animator anim;
    private Rigidbody rigidBody;
    public float moveSpeed;
    private float timer;
    private Cow nearestCow;
    public GameObject Alien;
    public AudioClip beam;
    public AudioSource audio;
   /// <summary>
   /// Find all components for the Machine
   /// </summary>
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        FindNewCow();
    }

    //start of simulation5
    public override void OnEpisodeBegin()
    {
      
       //Respawns alien at this location
        transform.position = new Vector3(120f, 4.5f, -8f);
        //rigidBody.velocity = Vector3.zero;
        FindNewCow();
    }
    /// <summary>
    /// Find the cow's position that it is going after and heads that way
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        
        float moveX = vectorAction[0];
        float moveZ = vectorAction[1];
        Debug.Log("Finding Speed");
        if(Vector3.Distance(transform.position, targetTransform.position) > 1)
        {
            Debug.Log(targetTransform.position + "this is the current ");
            Vector3 forward = transform.forward;
            forward.y = 0;
            transform.position += forward * Time.deltaTime * moveSpeed;
            //new Vector3(moveX, 0, moveZ)
        }
       
    }

    public override void CollectObservations(VectorSensor sensor)
    {
    
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
   
    }

   

    //Lets player test controls
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
    }
    /// <summary>
    /// Collects cows if the alien runs over them
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //If the object is a cow
        if(other.gameObject.tag == "reward")
        {
            GameObject cow = this.gameObject;
            Cow newCow = cow.GetComponent<Cow>();
            anim.SetTrigger("tractorOn");
           
            audio.clip = beam;
            audio.Play();
                GameManager.instance.CowLost();
                other.gameObject.SetActive(false);
                other.gameObject.tag = "claimed";
                Debug.Log("Got Here");
                SetReward(+1f);
                Debug.Log("reward collected");
                
                GameManager.instance.Cows.Remove(newCow);
                FindNewCow();
            
        }
        //if the object is the boundary
        if(other.gameObject.tag == "Edge")
        {
            SetReward(-1f);
            Debug.Log("wall touched");

            EndEpisode();
        }
        
    }
    /// <summary>
    /// Unused logic, was meant to turn a tractor beam on to draw a cow into the ship
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "reward")
        {
            anim.ResetTrigger("tractorOn");
            timer = 0;
        }
    }
    /// <summary>
    /// Logic to find a new cow to go after once one is destroyed
    /// </summary>
    private void FindNewCow()
    {
        targetTransform = GameObject.FindWithTag("reward").transform;
        transform.LookAt(targetTransform);
        foreach(Cow cow in GameManager.instance.Cows)
        {
            if(nearestCow == null)
            {
                nearestCow = cow;
            }
            else 
            {
                float distanceToCow = Vector3.Distance(cow.transform.position, transform.position);
                float distanceToCurrentNearestCow = Vector3.Distance(cow.transform.position, transform.position);

                if(distanceToCow < distanceToCurrentNearestCow)
                {
                    nearestCow = cow;
                }
            }
        }
    }
}
