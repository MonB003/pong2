using System.Runtime.CompilerServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HostPaddle : Paddle
{

    public List<Paddle> users = new List<Paddle>();


    public HostPaddle()
    {
        this.id = 0; // this is a test
    }

    private void Awake()
    {
       users.Add(this);
    }

}