﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
// using GameThread = pong2.Threads.GameThread;
 using P = pong2.Packet;


namespace pong2.Network
{
    public abstract class Network
    {
        private const int PORT = 11000;
        private const string IP_ADDRESS = "224.168.100.2";
        protected const int BUFFER_SIZE = 8;

        private int port;
        private string ipAddress;
        private GameManager gt;
        protected float[] buffer;

        private Thread t;

        public Network(GameManager gt)
        {
            this.port = PORT;
            this.ipAddress = IP_ADDRESS;
            this.buffer = new float[BUFFER_SIZE];
            this.gt = gt;
            setup();
        }

        public Network(int port, string ipAddress, GameManager gt)
        {
            this.port = port;
            this.ipAddress = ipAddress;
            this.gt = gt;
            this.buffer = new float[BUFFER_SIZE];
            setup();
        }

        public Network(int port, string ipAddress, GameManager gt, int bufferSize)
        {
            this.port = port;
            this.ipAddress = ipAddress;
            this.gt = gt;
            this.buffer = new float[bufferSize];
            setup();
        }

        public int GetPort()
        {
            return port;
        }

        public string GetIPAddress()
        {
            return ipAddress;
        }

          public float[] GetBuffer()
        {
            return buffer;
        }


        public virtual void execute()
        {
            if (t != null)
            {
                throw new Exception("Error: thread already started.");
            }

            t = new Thread(receive);
            Console.WriteLine("EXECUTE");
            t.Start();
        }
        public abstract void setup();

        protected abstract void receive();

        public abstract void send(P packet);

    }
}
