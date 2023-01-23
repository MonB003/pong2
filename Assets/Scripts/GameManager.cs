

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MultiNet = pong2.Network.MulticastNetwork;
using WebSock = pong2.Network.WebSocketNetwork;
using Net = pong2.Network.Network;
using pong2;

public class GameManager : MonoBehaviour
{
    private int playerScore = 0;
    private int computerScore = 0;
    public int ballSpeed = 200;
    public Ball ball;
    public Paddle PlayerPaddle;
    public Paddle PlayerPaddle2;
    public Text playerScoreText;
    public Text computerScoreText;
    public int playerCount = 0;
    public List<Paddle> paddles = new List<Paddle>();
    public float[,] positions = new float[2, 3] { { -8.830017f, 0.04998779f, 0.0f }, { 9.02f, -0.08f, 0.0f } };

    [Header("Game Join")]
    public GameObject Canvas;
    


    // public NetworkClient network;
    public Net network;



    // public List<Paddle> = new List<Paddle>()
    // Start is called before the first frame update


    public void SetPlayerPosition(Paddle paddle, float x, float y, float z)
    {
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
        if (paddles.Count == 0)
        {
            // create game and instantiate the player
            //gameOverPanel.SetActive(true);

        }
        else
        {
            // join instantiated another player
        }
    }

    public void Start()
    {
        Debug.Log("START");

        //what kind of network
        // network = new WebSocketNetwork();
        network = new MultiNet(this);
        network.execute();

        PlayerPaddle = Instantiate(PlayerPaddle) as Paddle;
        ball = Instantiate(ball) as Ball;
        PlayerPaddle2 = Instantiate(PlayerPaddle2) as Paddle;

        paddles.Add(PlayerPaddle);
        paddles.Add(PlayerPaddle2);

        SetPlayerPosition(PlayerPaddle, positions[0, 0], positions[0, 1], positions[0, 2]);
        SetPlayerPosition(PlayerPaddle2, positions[1, 0], positions[1, 1], positions[1, 2]);

        network.send(PlayerPaddle.Packetize());
        network.send(PlayerPaddle2.Packetize());

    }

    public void CreatePlayers()
    {
        Debug.Log("START");

        //what kind of network
        // network = new WebSocketNetwork();
        network = new MultiNet(this);
        network.execute();

        PlayerPaddle = Instantiate(PlayerPaddle) as Paddle;
        ball = Instantiate(ball) as Ball;
        PlayerPaddle2 = Instantiate(PlayerPaddle2) as Paddle;

        paddles.Add(PlayerPaddle);
        paddles.Add(PlayerPaddle2);

        SetPlayerPosition(PlayerPaddle, positions[0, 0], positions[0, 1], positions[0, 2]);
        SetPlayerPosition(PlayerPaddle2, positions[1, 0], positions[1, 1], positions[1, 2]);

        network.send(PlayerPaddle.Packetize());
        network.send(PlayerPaddle2.Packetize());
    }


    private void RenderArea(Packet p)
    {

        // checking on the bytes
    }



    /*
     * Instantiates a player paddle.
     */
    public PlayerPaddle InstantiatePlayer()
    {
        PlayerPaddle p;
        p = Instantiate(PlayerPaddle) as PlayerPaddle;
        return p;
    }

    // Update is called once per frame, 60 frames/second
    void Update()
    {

        Debug.Log("***BALL SENT: " + ball.Packetize().ToString());
        network.send(ball.Packetize());

        for (int i = 0; i < paddles.Count; i++)
        {
            Paddle p = paddles[i];
            p.UpdatePos();
            Packet pack = p.GetPacket();
            Debug.Log("***PADDLE SENT: " + pack.ToString());
            network.send(p.Packetize());
            // send to network here// network.send(pack)
        }

    }


    public void notify(Packet p)
    {
        Debug.Log("notified" + p.ToString());
        Debug.Log("notify being called");
    }

    private void ResetRound()
    {
        this.PlayerPaddle.ResetPosition();
        this.PlayerPaddle2.ResetPosition();
        ball.Reset();
    }





    // When join button is clicked
    public void StartGame()
    {
        Debug.Log("START GAME");

        Destroy(GameObject.FindGameObjectWithTag("JoinPanel"));
       

        //Canvas = new GameObject();
        //Canvas.SetActive(false);

        ////Start();

        //Destroy(Canvas);

        //if (paddles.Count == 0)
        //{
        //    // create game and instantiate the player

        //}
        //else
        //{
        //    // join instantiated another player
        //}
    }
}
