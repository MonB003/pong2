using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength;

    // called every time 2 objects collide
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        
        if(ball != null)
        {
            Vector2 normal = collision.GetContact(0).normal; // index 0
            ball.AddForce(-normal * this.bounceStrength);
            bounceStrength+=1.5f;
            ball.speed += 10.0f;
            Debug.Log("ball speed: " + ball.speed);

        }
    }
}
