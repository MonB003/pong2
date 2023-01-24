using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NetworkAPI
{
    public class NetworkComm
    {
        public delegate void MsgHandler(string message);
        public event MsgHandler MsgReceived;

        public static Socket mcastSocket;
        public static IPAddress mcastAddress = IPAddress.Parse("224.168.100.2");
        public static int mcastPort;

        public static void JoinMulticastGroup()
        {
            Debug.Log("ADDRESS: " + mcastAddress.ToString());
            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Dgram,
                                         ProtocolType.Udp);

                // Get the local IP address used by the listener and the sender to
                // exchange multicast messages.
                //Console.Write("\nEnter local IPAddress for sending multicast packets: ");
                //IPAddress localIPAddr = IPAddress.Parse(Console.ReadLine());
                Console.Write("Opening Sending Multicast: ");
                IPAddress localIPAddr = IPAddress.Any;
                // Create an IPEndPoint object.
                IPEndPoint IPlocal = new IPEndPoint(localIPAddr, 0);
                Console.WriteLine("Create an IPEndPoint object");
                // Bind this endpoint to the multicast socket.
                mcastSocket.Bind(IPlocal);
                Console.WriteLine("Bind this endpoint to the multicast socket");

                // Define a MulticastOption object specifying the multicast group
                // address and the local IP address.
                // The multicast group address is the same as the address used by the listener.
                MulticastOption mcastOption;
                mcastOption = new MulticastOption(mcastAddress, localIPAddr);

                mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                            SocketOptionName.AddMembership,
                                            mcastOption);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString());
            }

        }
    


        public static void BroadcastMessage(string message)
        {
            IPEndPoint sender;
            Console.WriteLine("Goes here");

            IPEndPoint endPoint;

            try
            {
                EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
                byte[] data;
                //Send multicast packets to the listener.
                endPoint = new IPEndPoint(mcastAddress, mcastPort);
                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(message), endPoint);
                Console.WriteLine("Multicast data sent.....");

                //while (true)
                //{
                //    data = new byte[1024];
                //    int recv = mcastSocket.ReceiveFrom(data, ref remoteEP);

                //    Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                //    mcastSocket.SendTo(data, recv, SocketFlags.None, remoteEP);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString());
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
                Debug.Log("117 \n" + e.ToString());
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
                    Debug.Log("154: " + message);

                    MsgReceived(message);
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
