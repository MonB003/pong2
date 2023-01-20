
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using pong2;

public class Paddle : MonoBehaviour
{
    //  private NetworkAPI network;
    //public NetworkClient network = new NetworkClient();
    public float speed = 5f;
    public int id;
    public float y;
    public float x;
    public float z;
    protected Rigidbody2D body;
    public Packet p;
    //public NetworkClient network;


    private void Awake()
    {
        id = 1;
        body = GetComponent<Rigidbody2D>();
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
     //   Debug.Log("packet 1: " + id);
     //    Debug.Log("packet 2: " + 0);
      //    Debug.Log("packet 3: " + x);
      //     Debug.Log("packet 4: " + y);
           // Debug.Log("packet 5: " + z);
        Packet packet = new Packet(id, 0, x, y, z);
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

        //p = Packetize();


        // network.Send(Packetize());

    }


    

    public void UpdatePos()
    {
        Debug.Log("UPDATE POS PADDLE");

        // update locations
        y = this.transform.position.y;
        //UnityEngine.Debug.Log("y " + y);
        x = this.transform.position.x;
        //UnityEngine.Debug.Log("x " + x);

        z = this.transform.position.z;
        //UnityEngine.Debug.Log("z " + z);

        p = Packetize();

        // p = Packetize();

    }

    public void ResetPosition()
    {
        body.position = new Vector2(body.position.x, 0.0f);
        body.velocity = Vector2.zero;
    }

    public override string ToString(){
        return "Paddle: " + id + " " + x + " " + y + " " + z;
    }
}
