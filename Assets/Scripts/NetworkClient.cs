using System.Runtime.CompilerServices;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;



public class NetworkClient
{
            private const int PORT = 11000;
        private const string IP_ADDRESS = "230.0.0.2";
        private const int BUFFER_SIZE = 6;

        private int port;
        private string ipAddress;
        // private GameThread gt;
        protected byte[] buffer = new byte[5];

        // private Thread t;

    public IPEndPoint endPoint;
    public IPAddress mcastAddress;
    public Socket mcastSocket;

    public NetworkClient()
    {
        setup();
        receive();
    }




        public int GetPort()
        {
            return port;
        }

        public string GetIPAddress()
        {
            return ipAddress;
        }




public void setup()
        {
          
            this.mcastAddress = IPAddress.Parse(IP_ADDRESS);

            this.mcastSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp
                );

            IPAddress localIP = IPAddress.Any;
            EndPoint localEP = (EndPoint)new IPEndPoint(localIP, this.GetPort()); // RECIEVE ENDPOINT
            // RECIEVE STUFF
            MulticastOption mcastOption = new MulticastOption(mcastAddress, localIP);
            mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        mcastOption);
            mcastSocket.Bind(localEP); 


            mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            this.endPoint = new IPEndPoint(this.mcastAddress, this.GetPort()); // SEND ENDPOINT
        }


     public void receive()
    {
        Debug.Log("RECEIVING");
        EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0); // Recieve ENDPOINT

        // while (true)
        // {
            // mcastSocket.ReceiveFrom(buffer, ref remoteEP);

            // Packet packet = new Packet(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4]);

        //     //Console.WriteLine("RECEIVED PACKET: " + packet.ToString());

        //     // gameEngine.notify()
        // }

    }
}