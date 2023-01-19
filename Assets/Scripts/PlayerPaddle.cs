
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;
   

    // Start is called before the first frame update
    void Start()
    {
        y = _direction.y;
        x = _direction.x;
        z = 0;

        p = Packetize();
        
    }


    // Update is called once per frame
    void Update()
    {   

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        } 
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        } 
        else 
        {
            _direction = Vector2.zero;
        }

        // update locations
        y = _direction.y;
        x = _direction.x;
        z = 0;

        p = Packetize();
       // Debug.Log("PlayerPaddle: " + p.ToString());
        //network.send(Packetize());
    }



    public void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            body.AddForce(_direction * speed);
        }
    }

      public override string ToString(){
        return "Paddle: " + id + " " + x + " " + y + " " + z;
    }


}
