using System.Threading;
using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MultiNet = pong2.Network.MulticastNetwork;
using WebSock = pong2.Network.WebSocketNetwork;
using Net = pong2.Network.Network;
using pong2;
using NetworkAPI;

public class GameManager : MonoBehaviour
{
    private int GAME_ACTIVE = 1;
    private int _HOST_ID = 0;
    private int playerScore = 0;
    private int computerScore = 0;
    public int ballSpeed = 200;
    public Ball ball;

    public HostPaddle Host;

    public Text playerScoreText;
    public Text computerScoreText;
    public int playerCount = 0;
    public List<Paddle> paddles = new List<Paddle>();
    public float[,] positions = new float[2, 3] { { -8.830017f, 0.04998779f, 0.0f }, { 9.02f, -0.08f, 0.0f } };

    public Net network;

    public bool startGame = false;
    bool isHost;
    private Packet acknowledgePacket;

    NetworkComm networkComm;

    public void SetPlayerPosition(Paddle paddle, float x, float y, float z)
    {
        paddle.x = x;
        paddle.y = y;
        paddle.z = z;
        paddle.SetPosition(x, y, z);
    }

    public void PlayerScored()
    {
        this.playerScore++;
        playerScoreText.text = playerScore.ToString();
        ResetRound();
    }
    public void ComputerScored()
    {
        this.computerScore++;
        computerScoreText.text = computerScore.ToString();
        ResetRound();
    }

    void Awake()
    {
        //network = new MultiNet(this);
        //network.execute();

        networkComm = new NetworkComm();
        networkComm.MsgReceived += new NetworkComm.MsgHandler(processMsg);
        (new Thread(new ThreadStart(networkComm.ReceiveMessages))).Start();


        NetworkComm.JoinMulticastGroup();
        NetworkComm.BroadcastMessage("Hello");
    }

    private void processMsg(string message)
    {
        Debug.Log("76 FROM DELEGATE: " + message);
    }

    void Start()
    {

        // make a list of incoming packets

        //Packet player = ReceiveAcknowledgement(acknowledgePacket);


        //if (player.GetHost() == 1)
        //{
        //    Debug.Log("Im the host");
        //  //  ball.Init();
        //} 
        //else 
        //{
        //   // RenderBall(ball);
        //}
        //ball = Instantiate(ball) as Ball;

        // send a packet, for entering the game



    }

    public void Render()
    {

    }

    /**
          * Renders the ball to the screen
    */
    public void RenderBall(Packet p)
    {
        ball.transform.position = new Vector3(p.GetX(), p.GetY(), p.GetZ());
        // b.transform.position = new Vector3(b.x, b.y, b.z);
    }


    public Packet CreatePlayerPacket(Packet p)
    {

        System.Random r = new System.Random();
        int id = r.Next(0, 1000);

        int serverAction = (int)p.GetAction();
        int gameState = (int)p.GetState();
        int usersState = 1;
        int usersAction = (int)Actions.START;
        float host = 1;
        float coordinateID = 0;

        if (gameState == GAME_ACTIVE)
        {
            //usersState  = 1;
            usersAction = (int)Actions.JOIN;
            host = 0;
            coordinateID = p.GetCoordinateID() + 1;
        }

        Packet playerPacket = new Packet((float)id, (float)usersAction, (float)usersState, host, coordinateID, 0, 0, 0);
        networkComm.sendMessage("Another object entered trigger: " + playerPacket.ToString());
        return playerPacket;

    }

    public Packet ReceiveAcknowledgement(Packet p)
    {
        Debug.Log("148 RECEIVED ACKNOWLEDGEMENT: " + p.ToString());

        Packet playerInformation = CreatePlayerPacket(p);

        Debug.Log("152 SENDING PLAYER INFORMATION: " + playerInformation.ToString());


        switch ((int)playerInformation.GetHost())
        {
            case 0:
                InstantiatePlayer(false, playerInformation);
                break;
            case 1:
                startGame = true;
                InstantiatePlayer(true, playerInformation);
                break;
        }

        return playerInformation;
    }

    /*
     * Instantiates a player paddle.
     */
    public Paddle InstantiatePlayer(bool host, Packet packet)
    {
        Paddle p = null;

        if (host)
        {
            Host = Instantiate(Host) as HostPaddle;
            Debug.Log("here");
            // Host.SetID(Host.GenerateIDForPaddle());
            paddles.Add(Host);
            Host.SetPaddle(packet);
            SetPlayerPosition(Host, positions[0, 0], positions[0, 1], positions[0, 2]);
            //network.send(Host.Packetize());
            networkComm.sendMessage("" + Host.Packetize());
            return Host;
        }
        else
        {
            p = Instantiate(paddles[0]) as PlayerPaddle;
            p.SetID(Host.GenerateIDForPaddle());
            paddles.Add(p);
            SetPlayerPosition(p, positions[0, 0], positions[0, 1], positions[0, 2]);
            //network.send(p.Packetize());
            networkComm.sendMessage("" + p.Packetize());
            return p;
        }
    }

    // Update is called once per frame, 60 frames/second
    void Update()
    {

        Debug.Log("***BALL SENT: " + ball.Packetize().ToString());
        //network.send(ball.Packetize());
        networkComm.sendMessage("206: " + ball.Packetize());

        for (int i = 0; i < paddles.Count; i++)
        {

            Paddle p = paddles[i];
            p.UpdatePos();
            Packet pack = p.GetPacket();
            Debug.Log("***PADDLE SENT: " + pack.ToString());
            //network.send(p.Packetize());
            networkComm.sendMessage("216: " + p.Packetize());
            // send to network here// network.send(pack)
        }

    }


    private void Movement(Packet p)
    {
        int id = (int)p.GetID();
        switch (id)
        {
            case -1: // this is the balls ID number in the packet
                RenderBall(p);
                break;
            default:
                // move player function as all other IDs will b players
                break;
        }
    }
    public void notify(Packet p)
    {
        Debug.Log("---------NOTIFIED: " + p.ToString());

        int action = (int)p.ConvertByteToFloat(1);


        // check the packet
        switch (action)
        {
            case (int)Actions.JOIN:
                // instantiate a player

                break;
            case (int)Actions.MOVE:
                Movement(p); // controls who is moving etc.

                break;
            case (int)Actions.START:
                acknowledgePacket = p;


                //  InstantiatePlayer(true); // start a game with the host.
                break;
            case (int)Actions.SERVER:
                //ReceiveAcknowledgement(p);
                break;
            default:
                // do nothing
                break;
        }


    }

    private void ResetRound()
    {
        Debug.Log("RESET ROUND");

        foreach (Paddle p in paddles)
        {
            Debug.Log(p.ToString());
            p.ResetPosition();
        }
        // this.PlayerPaddle.ResetPosition();
        // this.PlayerPaddle2.ResetPosition();
        ball.Reset();
    }
}
