using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;

    private Rigidbody rigidBody;
    public float moveSpeed;
    private float timer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    //start of simulation5
    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, 4.5f, 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float moveZ = vectorAction[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //Lets player test controls
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "reward")
        {
            timer += Time.deltaTime;
            if(timer >= 3f)
            {
                SetReward(+1f);
                Debug.Log("reward collected");
                timer = 0f;
            }
        }
        if(other.gameObject.tag == "Edge")
        {
            SetReward(-1f);
            Debug.Log("wall touched");

            EndEpisode();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "reward")
        {
            timer = 0;
        }
    }
}
