using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 vel = Vector3.zero;
    Vector3 velX = Vector3.zero;
    Vector3 velY = Vector3.zero;

    Rigidbody rigid;
    Transform trans;

    Vector3 rot;

    public bool inverted = false;

    public Transform gunTrans;

    public int playerNum;

    string horizontalAxisStr = "";
    public KeyCode jumpKey;

    bool in_air = true;
    bool jumping = false;

    public int gravitySpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = transform;
    }

    void FixedUpdate()
    {
        jumping = false;

        // Movement
        CharacterController controller = GetComponent<CharacterController>();

        if (playerNum == 0)
        {
            horizontalAxisStr = "Horizontal1";
            jumpKey = KeyCode.W;
        }
        else if (playerNum == 1)
        {
            horizontalAxisStr = "Horizontal2";
            jumpKey = KeyCode.UpArrow;
        }

        // Feed moveDirection with input
        float inputX = Input.GetAxis(horizontalAxisStr);
        velX = trans.right * inputX;

        // Jumping
        if (Input.GetKeyDown(jumpKey))
        {
            in_air = true;
            velY += Vector3.up * jumpSpeed;
        }

        // Gravity
        velY -= Vector3.up * gravitySpeed;

        //Update character position
        vel = velX.normalized * speed + velY.normalized;

        rigid.velocity = vel;
    }
}
