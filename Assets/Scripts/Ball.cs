
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong2;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D body;
    public float speed = 200.0f;
    private Packet packet;
    public int id = -1;
    private UnityEngine.Vector3 position;
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
        position = this.transform.position; // gets the current vector of the ball
        
        packet  = Packetize();
       // Vector2 force = new Vector2()
       // body.AddForce(force);
    }

     public Packet Packetize()
    {
        Packet packet = new Packet(id, 0, position.x, position.y, position.z);
        return packet;
    }

    public Packet GetPacket()
    {
        return packet;
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f? -1.0f : 1.0f;
        float y = Random.value < 0.5f? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        body.AddForce(new UnityEngine.Vector2(x,y) * speed);
    }

    public void AddForce(UnityEngine.Vector2 force)
    {
        body.AddForce(force);
    }

    public void Reset()
    {
        body.velocity = UnityEngine.Vector2.zero;
        transform.position = UnityEngine.Vector2.zero;
        this.speed = 200.0f;
        AddStartingForce();
    }
}
