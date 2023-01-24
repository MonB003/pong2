
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using pong2;

public abstract class Paddle : MonoBehaviour
{
    //  private NetworkAPI network;
    //public NetworkClient network = new NetworkClient();
    public float speed = 5f;
    public int id;
    public float y;
    public float x;
    public float z;
    private float state;
    private float action;
    private float host;
    private float coordinateID;
    protected Rigidbody2D body;
    public Vector2 _direction;

    public Packet p;
    //public NetworkClient network;


    private void Awake()
    {
        Debug.Log("AWAKE PADDLE");
        body = GetComponent<Rigidbody2D>();
    }
    
    public int GetID() {
        return id;
    }

     public void SetID(int id){
         this.id = id;
     }
    public void SetPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
        this.y = y;
        this.x = x;
        this.z = z;
    }

    public Packet Packetize()
    {
        Packet packet = new Packet(id, action,state, host,coordinateID, x, y, z);
        return packet;
    }

    public void Init(bool isPlayer)
    {
        if (isPlayer)
        {
            id = 1;
        }
        else
        {
            id = 2;
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        // send, set the vars
        y = this.transform.position.y;
        x = this.transform.position.x;
        z = this.transform.position.z;
        p = Packetize();
    }

    public virtual Packet GetPacket()
    {
        return p;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UPDATE PADDLE");

        // update locations
        y = this.transform.position.y;
        x = this.transform.position.x;
        z = this.transform.position.z;


   if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("UP");
            _direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("DOWN");
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }

        // update locations
        y = _direction.y;
        x = _direction.x;
        z = 0;
        

    }


    

    public void UpdatePos()
    {
        Debug.Log("UPDATE POS PADDLE");

        // update locations
        y = this.transform.position.y;
        x = this.transform.position.x;
        z = this.transform.position.z;
        p = Packetize();
    }

    public void ResetPosition()
    {
        body.position = new Vector2(body.position.x, 0.0f);
        body.velocity = Vector2.zero;
    }

    public override string ToString(){
        return "Paddle: " + id + " " + x + " " + y + " " + z;
    }



    public void SetPaddle(Packet packet) {
        this.id           = (int)packet.GetID();
        this.action       = (int)packet.GetAction();
        this.state        = (int) packet.GetState();
        this.host         = (int) packet.GetHost();
        this.coordinateID = (int)packet.GetCoordinateID();
        // this.x            = packet.x;
        // this.y            = packet.y;
        // this.z            = packet.z;
        //p                 = packet;
    }

    


}
