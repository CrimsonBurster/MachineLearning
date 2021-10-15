using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        NewPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        var vectorToTarget = transform.position - targetLocation;
        vectorToTarget.y = 0;
        float dist = vectorToTarget.magnitude;
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
        
        if (dist < .5f)
        {
            NewPos();
        }
    }

    private void NewPos()
    {
        targetLocation = new Vector3(transform.position.x + Random.Range(-2, 2f), transform.position.y, transform.position.z + Random.Range(-2f, 2f));
    }
}
