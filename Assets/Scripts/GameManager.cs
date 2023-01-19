using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int playerCount      = 0;
    public List<Paddle> paddles = new List<Paddle>();
    public float[,] positions   = new float[2,3]{{-8.830017f,0.04998779f,0.0f}, {9.02f,-0.08f, 0.0f}};


    public NetworkClient network;



   // public List<Paddle> = new List<Paddle>()
   // Start is called before the first frame update


    public void SetPlayerPosition(Paddle paddle, float x, float y, float z){
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
    

    void Start()
    {

        //network = connect(); // connect to multi cast network
        // renderArea();
        network = new NetworkClient();
        
        PlayerPaddle   = Instantiate (PlayerPaddle) as Paddle;
        ball           = Instantiate (ball) as Ball;
        PlayerPaddle2  = Instantiate(PlayerPaddle2) as Paddle;
        
        SetPlayerPosition(PlayerPaddle, positions[0,0], positions[0,1], positions[0,2]);
        SetPlayerPosition(PlayerPaddle2, positions[1,0], positions[1,1], positions[1,2]);

        network.send(PlayerPaddle.Packetize());
        network.send(PlayerPaddle2.Packetize());

        // initial send to network
        // infinite loop to recieve from network

        bool gamePlay = true;
        // while (gamePlay)
        // {
        // //    RenderArea(network.recieve());
            
        // }
      
    }

    private void RenderArea(Packet p){

        // checking on the bytes
    }

    // Update is called once per frame, 60 frames/second
    void Update()
    {
        network.send(PlayerPaddle.Packetize());
        network.send(PlayerPaddle2.Packetize());

        // renderArea();

        //    for(int i = 0; i < paddles.Count; i++)
        //    {
        //         Paddle p = paddles[i];
        //         Packet pack = p.GetPacket();
        //         // send to network here// network.send(pack)

        //    }
    }

    private void ResetRound(){
        this.PlayerPaddle.ResetPosition();
        this.PlayerPaddle2.ResetPosition();
        ball.Reset();
    }
}
