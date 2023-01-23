using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace pong2.MulticastListener
{
   
    public class MulticastListener : MonoBehaviour
    {
        private static IPAddress mcastAddress;
        private static int mcastPort;
        private static Socket mcastSocket;
        private static MulticastOption mcastOption;
        // Start is called before the first frame update
        public void Start()
        {
            mcastAddress = IPAddress.Parse("224.168.100.2");
            mcastPort = 11000;
            // Start a multicast group.
            StartMulticast();
            // Display MulticastOption properties.
            MulticastOptionProperties();
        }

        // Update is called once per frame
        public void Update()
        {
            // Receive broadcast messages.
            ReceiveBroadcastMessages();
        }




        private static void MulticastOptionProperties()
        {
            Debug.Log("Current multicast group is: " + mcastOption.Group);
            Debug.Log("Current multicast local address is: " + mcastOption.LocalAddress);
        }


        private static void StartMulticast()
        {

            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Dgram,
                                         ProtocolType.Udp);

                //Console.Write("Enter the local IP address: ");

                //IPAddress localIPAddr = IPAddress.Parse(Console.ReadLine());
                Console.Write("Joining Multicast: ");
                IPAddress localIPAddr = IPAddress.Any;
                EndPoint localEP = (EndPoint)new IPEndPoint(localIPAddr, mcastPort);

                mcastSocket.Bind(localEP);

                // Define a MulticastOption object specifying the multicast group
                // address and the local IPAddress.
                // The multicast group address is the same as the address used by the server.
                mcastOption = new MulticastOption(mcastAddress, localIPAddr);

                mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                            SocketOptionName.AddMembership,
                                            mcastOption);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveBroadcastMessages()
        {
            bool done = false;
            //byte[] bytes = new Byte[100];
            IPEndPoint groupEP = new IPEndPoint(mcastAddress, mcastPort);
            EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine(remoteEP.ToString());
            byte[] data = new byte[1024];
            string input, stringData;

            try
            {
                while (!done)
                {
                    Debug.Log("Waiting for multicast packets.......");
                    Debug.Log("Enter ^C to terminate.");

                    mcastSocket.ReceiveFrom(data, ref remoteEP);

                    Debug.Log("Received broadcast from : " +
                      groupEP.ToString() + "" +
                      Encoding.ASCII.GetString(data, 0, data.Length));

                    //Console.WriteLine("Write your message here. Type 'exit' to stop sending messages");
                    while (true)
                    {
                        //input = Console.ReadLine();
                        //if (input == "exit")
                        //    break;
                        //mcastSocket.SendTo(Encoding.ASCII.GetBytes(input), remoteEP);
                        data = new byte[1024];
                        int recv = mcastSocket.ReceiveFrom(data, ref remoteEP);
                        stringData = Encoding.ASCII.GetString(data, 0, recv);
                        Debug.Log(stringData);
                    }

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
