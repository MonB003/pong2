//using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//using pong2.Threads;
//using GameThread = pong2.Threads.GameThread;
using P = pong2.Packet;

namespace pong2.Network
{
    public class MulticastNetwork : Network
    {

        public IPEndPoint endPoint;
        public IPAddress mcastAddress;
        public Socket mcastSocket;

        public Packet InstructionPacket;

        public GameManager manager;

        public Boolean ack = false;

        public MulticastNetwork(GameManager gt) : base(gt)
        {
            this.manager = gt;
        }

        public MulticastNetwork(int port, string ipAddress, GameManager gt) : base(port, ipAddress, gt) { }


        public MulticastNetwork(int port, string ipAddress, GameManager gt, int bufferSize) : base(port, ipAddress, gt, bufferSize) { }


        public override void send(P packet)
        {
            EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0); // Recieve ENDPOINT
            Debug.Log("SENDING: " + packet.GetBuffer().Length);
            Debug.Log("remoteEP: " + remoteEP.ToString());
            Console.WriteLine("SENDING: " + packet.ToString());

            try
            {
                //Send multicast packets to the listener.
                //this.mcastSocket.SendTo(packet.GetBuffer(), endPoint); //old code
                this.mcastSocket.SendTo(packet.GetBuffer(), remoteEP); // new code
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString());
            }
        }

        protected override void receive()
        {
            EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0); // Recieve ENDPOINT
            byte[] results = new byte[1024];
            string stringData = "";

            while (true)
            {
                if (!ack)
                {
                    Acknowledgment(); // send joining packet.
                    ack = true;
                }
                else
                {
                    Console.WriteLine("Waiting for multicast packets.......");
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                    endPoint.ToString(),
                      Encoding.ASCII.GetString(results, 0, results.Length));

                    results = new byte[BUFFER_SIZE * sizeof(float)];

                    Console.WriteLine("BUFFER SIZE " + results.Length);

                    int recv = mcastSocket.ReceiveFrom(results, ref remoteEP);

                    // Checking received content
                     stringData = Encoding.ASCII.GetString(results, 0, recv);
                    Console.WriteLine(stringData);

                    Buffer.BlockCopy(results, 0, this.buffer, 0, BUFFER_SIZE * sizeof(float));


                    // P packet = new P(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4]);
                    // Console.WriteLine("RECEIVED PACKET: " + packet.ToString());
                    //  manager.notify(packet);
                }
            }

        }


        // public void Listening(EndPoint remoteEP)
        // {
        //     byte[] results = new byte[BUFFER_SIZE * sizeof(float)];

        //     Console.WriteLine("BUFFER SIZE " + results.Length);
        //     mcastSocket.ReceiveFrom(results, ref remoteEP);

        //     Buffer.BlockCopy(results, 0, this.buffer, 0, BUFFER_SIZE * sizeof(float));


        //     P packet = new P(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4]);
        //     Console.WriteLine("RECEIVED PACKET: " + packet.ToString());
        //     manager.notify(packet);
        // }


        private List<Packet> GetGameInfo()
        {
            EndPoint remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0); // Recieve ENDPOINT

            List<Packet> packets = new List<Packet>();

            var startTime = DateTime.Now.AddSeconds(3);
            byte[] results = new byte[BUFFER_SIZE * sizeof(float)];


            while (DateTime.UtcNow < startTime)
            {

                Console.WriteLine("BUFFER SIZE " + results.Length);
                mcastSocket.ReceiveFrom(results, ref remoteEP);

                Buffer.BlockCopy(results, 0, this.buffer, 0, BUFFER_SIZE * sizeof(float));

                P packet = new P(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5], buffer[6], buffer[7]);
                packets.Add(packet);
            }

            return packets;
        }


        private bool InterpretPackets(List<Packet> packets)
        {
            bool gameActive = false;

            foreach (Packet p in packets)
            {
                if (p.GetState() == 1) // 1 means a game is active.
                {
                    gameActive = true;
                    break;
                }
            }

            return gameActive;
        }

        public void Acknowledgment()
        {

            List<Packet> packets = GetGameInfo();
            bool gameActive = InterpretPackets(packets);
            int state = 0;
            int action = (int)Actions.START;
            
            if (gameActive)
            {
                state = 1;
                action = (int)Actions.JOIN;
            }

            Packet ack = new Packet((float)Actions.SERVER, (float)action, (float)state, 0, 0, 0, 0, 0);
            manager.notify(ack);
        }

        public override void setup()
        {

            this.mcastAddress = IPAddress.Parse(this.GetIPAddress());

            this.mcastSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp
                );
            Console.Write("Joining Multicast: ");
            IPAddress localIP = IPAddress.Any;
            EndPoint localEP = (EndPoint)new IPEndPoint(localIP, this.GetPort()); // RECIEVE ENDPOINT
            mcastSocket.Bind(localEP);

            // RECIEVE STUFF
            MulticastOption mcastOption = new MulticastOption(mcastAddress, localIP);
            mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        mcastOption);



            //mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            this.endPoint = new IPEndPoint(this.mcastAddress, this.GetPort()); // SEND ENDPOINT


        }
    }

}
