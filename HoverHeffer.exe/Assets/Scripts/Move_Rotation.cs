using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move_Rotation : MonoBehaviour
{
    public static Move_Rotation instance;
    public Vector2 turn;
    public float sensitivity = .5f;
    public float moveSpeed, normalSpeed, strafeSpeed, boostSpeed;
    public float x, z;

    public GameObject bullet;
    public Transform[] firePoints;
    private float shootTimer, boostTimer;
    public Slider boostMeter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //locks cursor in middle of screen and makes invisible
        Cursor.lockState = CursorLockMode.Locked;
        shootTimer = 1f;
        moveSpeed = normalSpeed;
        boostTimer = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate ship on Y axis, with left/right mouse movement(the mouse's X axis)
        turn.y += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.y, 0);

        z = Input.GetAxisRaw("Horizontal");
        x = Input.GetAxisRaw("Vertical");
        transform.Translate(x * moveSpeed, 0, -z * strafeSpeed);

        if (Input.GetMouseButton(0))
        {
            if(shootTimer >= .25f)
            {
                foreach (Transform point in firePoints)
                {
                    Instantiate(bullet, point.position, point.rotation);
                }
                shootTimer = 0f;
            }
        }
        //Boost management
        if (Input.GetKey(KeyCode.LeftShift) && boostTimer >= 0)
        {
            moveSpeed = boostSpeed;
            boostTimer -= Time.deltaTime * 10f;
            boostMeter.value = boostTimer;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if(shootTimer <= .25f)
        {
            shootTimer += Time.deltaTime;
        }
        
        if(boostTimer <= 100f)
        {
            boostTimer += Time.deltaTime * 0.5f;
            boostMeter.value = boostTimer;

            if(boostTimer <= 0)
            {
                boostTimer = 0f;
            }
        }
    }
}
