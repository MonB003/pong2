using UnityEngine;
using P = pong2.Packet;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server = pong2.MulticastListener.MulticastListener;
using N = NetworkAPI.NetworkComm;

namespace A2.Events
{
    public class EventHandler : MonoBehaviour
    {
        private static IPAddress mcastAddress = IPAddress.Parse("224.168.100.2");
        private static int mcastPort = 11000;
        private static Socket mcastSocket;
        private static MulticastOption mcastOption;
     //   private N network;
        private Server server;
        private GameManager manager;

        public EventHandler()
        {
            Debug.Log("EventHandler Created...");
        }


        private void Awake()
        {
            //manager = GameObject.Find("GameManager").GetComponent<GameManager>();
           // server = GameObject.Find("Network").GetComponent<Server>();
            //  StartMulticast();
        }

        private void Update()
        {
            ReceiveFromNetwork();
        }

        public void SendToManager()
        {

        }


        public void SendToNetwork(string message)
        {
            Debug.Log("Sending to Network: " + message);
            this.sendMessage(message);
        }

        public void ReceiveFromNetwork()
        {

        }

        public void sendMessage(P information)
        {

            IPAddress mcastAddress;
            int mcastPort;
            Socket mcastSocket = null;
            mcastAddress = IPAddress.Parse("224.168.100.2");
            mcastPort = 11000;
            IPEndPoint endPoint;

            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                               SocketType.Dgram,
                               ProtocolType.Udp);

                //Send multicast packets to the listener.
                endPoint = new IPEndPoint(mcastAddress, mcastPort);

                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes("Starting data transfer...\n"), endPoint);

                mcastSocket.SendTo(information.GetBuffer(), endPoint);

                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes("\n\0"), endPoint);

                Debug.Log("Message Sent");
            }
            catch (Exception e)
            {
                Debug.Log("\n" + e.ToString());
            }
            mcastSocket.Close();
        }
        public void sendMessage(String message)
        {
            IPAddress mcastAddress;
            int mcastPort;
            Socket mcastSocket = null;
            mcastAddress = IPAddress.Parse("224.168.100.2");
            mcastPort = 11000;
            IPEndPoint endPoint;

            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                               SocketType.Dgram,
                               ProtocolType.Udp);

                //Send multicast packets to the listener.
                endPoint = new IPEndPoint(mcastAddress, mcastPort);
                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(message), endPoint);
                Debug.Log("Message Sent");
            }
            catch (Exception e)
            {
                Debug.Log("\n" + e.ToString());
            }
            mcastSocket.Close();
        }


        public void ReceiveMessages()
        {
            IPAddress mcastAddress;
            int mcastPort;
            Socket mcastSocket = null;
            MulticastOption mcastOption = null;
            mcastAddress = IPAddress.Parse("224.168.100.2");
            mcastPort = 11000;
            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Dgram,
                                         ProtocolType.Udp);
                IPAddress localIP = IPAddress.Any;
                EndPoint localEP = (EndPoint)new IPEndPoint(localIP, mcastPort);
                mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                mcastSocket.Bind(localEP);
                mcastOption = new MulticastOption(mcastAddress, localIP);
                mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                            SocketOptionName.AddMembership,
                                            mcastOption);


                bool done = false;
                byte[] bytes = new Byte[100];
                IPEndPoint groupEP = new IPEndPoint(mcastAddress, mcastPort);
                EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

                while (!done)
                {
                    mcastSocket.ReceiveFrom(bytes, ref remoteEP);
                    String message = "Received broadcast from: " + remoteEP.ToString() + "  " +
                      Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Debug.Log(message);

                    //MsgReceived(message);
                }

                mcastSocket.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}