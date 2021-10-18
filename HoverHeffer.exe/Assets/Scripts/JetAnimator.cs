using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetAnimator : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Called to make the animations move with the mouse
    /// </summary>
    void Update()
    {
        float mouseSpeed = Input.GetAxis("Mouse X");

        if(mouseSpeed != 0f)
        {
            anim.SetFloat("xRotation", mouseSpeed);
        }
        else
        {
            anim.SetFloat("xRotation", Move_Rotation.instance.z);
        }
        
        
    }
}
