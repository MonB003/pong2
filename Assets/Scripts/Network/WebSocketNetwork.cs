using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.SignalR.Client;

// using GameThread = pong2.Threads.GameThread;
using P = pong2.Packet;

namespace pong2.Network
{
    public class WebSocketNetwork : Network
    {
        // private HubConnection connection;
        public WebSocketNetwork(GameManager gt) : base(gt)
        {
        }

        public WebSocketNetwork(int port, string ipAddress, GameManager gt) : base(port, ipAddress, gt)
        {
        }

        public WebSocketNetwork(int port, string ipAddress, GameManager gt, int bufferSize) : base(port, ipAddress, gt, bufferSize)
        {
        }

        public override void send(P packet)
        {
            Console.WriteLine("SEND: " + packet.ToString());
            byte[] b = packet.GetBuffer();
            //connection.SendAsync("SendPacket", b[0], b[1], b[2], b[3], b[4], b[5]);
        }

        public override void setup()
        {
           // connection = new HubConnectionBuilder().WithUrl("http://192.168.1.82:5000/gamehub").Build();
            // Console.WriteLine("Connection " + connection.ToString());
            //connection.StartAsync().Wait();
            // Console.WriteLine(connection.State + " " + connection.ConnectionId);
        }

        protected override void receive()
        {
            //connection.On("RecievePacket", (byte user, byte fruit, byte action, byte x, byte y, byte z) =>
            //{
            //    P p = new P(user, fruit, action, x, y, z);
            //    Console.WriteLine("RECIEVED: " + p.ToString());
            // //});
        }
    }
}

