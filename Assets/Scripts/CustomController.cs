using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using SocketIO;

public class CustomController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject instanciatedBullet;
    public Vector2 bounds = new Vector2(-9, 9);
    private bool _shot = false;
    public float speed = 2.5f;
    //for smartphone control
    private SocketIOComponent socket;
    // Use this for initialization
    void Start()
    {
        // Init network for smartphone control
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("connectedtoserver", Connected);
        socket.On("playermoved", MoveWithSmartphone);
        socket.On("playerclicked",shoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="main")
        {
            // Always going forward
            transform.Translate(0, 0, speed * Time.deltaTime);
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            //float vertical = CrossPlatformInputManager.GetAxis("Vertical");
            if (transform.position.x + horizontal > bounds.x && transform.position.x + horizontal < bounds.y)
            {
                transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);
            }
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                shoot(null);
            }
        }
    }
    void shoot(SocketIOEvent e)
    {
            if (_shot)
            {
                teleport();
            }
            else
            {
                instanciatedBullet = (GameObject)Instantiate(bullet, transform.position + new Vector3(0, 0, 2), transform.rotation);
                instanciatedBullet.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 250), ForceMode.VelocityChange);

                _shot = true;
            }
    }
    void teleport()
    {
        instanciatedBullet.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        StartCoroutine(WaitAndMove(1, instanciatedBullet.transform.position));
    }
    IEnumerator WaitAndMove(float delayTime, Vector3 pos)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        transform.Translate(0,-1,0);
        float previousspeed = speed;
        speed = 0;
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= delayTime)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, pos - new Vector3(0,1,0), Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        transform.Translate(0,1,0);
        _shot = false;
        speed = previousspeed;
        GetComponent<BoxCollider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        GetComponent<ParticleSystem>().Emit(100);
    }
    public void Connected(SocketIOEvent e)
    {
        socket.Emit("unityconnected");
        GameObject.Find("ServerIP").GetComponent<Text>().text = "Go to "+ e.data.GetField("ipAdress").str+" with your smartphone";
        Debug.Log("Connected to server : "+e.data.GetField("ipAdress"));
    }
    public void MoveWithSmartphone(SocketIOEvent e)
    {
        //Debug.Log(e.data.GetField("delta"));

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "main")
        {
            float horizontal = -e.data.GetField("delta").n/10;
            //float vertical = CrossPlatformInputManager.GetAxis("Vertical");
            if (transform.position.x + horizontal > bounds.x && transform.position.x + horizontal < bounds.y)
            {
                transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);
            }
        }
    }
}