using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D body;
    public float speed = 200.0f;
    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        //Reset();
        AddStartingForce();
    }

    public Rigidbody2D getBody(){
        return this.body;
    }
    // Update is called once per frame
    void Update()
    {
       // Vector2 force = new Vector2()
       // body.AddForce(force);
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f? -1.0f : 1.0f;
        float y = Random.value < 0.5f? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        body.AddForce(new Vector2(x,y) * speed);
    }

    public void AddForce(Vector2 force)
    {
        body.AddForce(force);
    }

    public void Reset()
    {
        body.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        this.speed = 200.0f;
        AddStartingForce();
    }
}
