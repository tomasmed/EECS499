using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    Rigidbody rigid;
    Transform trans;

    Vector3 rot;

    public bool inverted = false;

    public Transform gunTrans;

    public int playerNum;

    string horizontalAxisStr = "";
    KeyCode jumpKey;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = transform;
    }

    void FixedUpdate()
    {
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
        
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis(horizontalAxisStr), 0.0f, 0.0f);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetKeyDown(jumpKey))
                moveDirection.y = jumpSpeed;
            moveDirection = transform.TransformDirection(moveDirection);
        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
