using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody theRB;
    public int power;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Creating the speed of the bullet
        theRB.velocity = transform.right * speed * Move_Rotation.instance.moveSpeed * 50;

        timer += Time.deltaTime;
        //Delaying the time until the bullet destroys if not hitting anything, so it does not travel forever
        if(timer >= 4f)
        {
            DestroyBullet();
        }
    }
    /// <summary>
    /// Used to destroy the bullet and apply damage if it collides with the enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyHealth.instance.TakeDamage(power);
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
