using System.Runtime.CompilerServices;
using System.Globalization;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class HostPaddle : PlayerPaddle
{

    List<int> userIDs = new List<int>();
    public List<Paddle> users = new List<Paddle>();

    public HostPaddle()
    {
        // this is a test
    }

    private void Awake()
    {
       users.Add(this);
        body = GetComponent<Rigidbody2D>();
    }

    // public override void SetID(int ID)
    // {
    //     this.id = ID;
    //     userIDs.Add(ID);
    // }

    public void AddUsers(Paddle paddle)
    {
        
        users.Add(paddle);
    }

    void Update(){
         if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
    }

    public int GenerateIDForPaddle(){

        System.Random r = new System.Random();
        int id = r.Next(int.MinValue,int.MaxValue);

        while(userIDs.Contains(id)){
            id = r.Next(int.MinValue,int.MaxValue);
        }
        
        userIDs.Add(id);
        return id;

    }
}