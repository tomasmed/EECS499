using UnityEngine;
using System.Collections;

public class nonCharacterControllerMovement : MonoBehaviour {
    public float speed;
    public Rigidbody bod;
    public enum controles { WASD = 0, ARROW };
    public controles scheme = controles.WASD;
    public bool freeSwing;
    public Vector3 left;
    public Vector3 right;
    public bool leftCheck;
    public bool rightCheck;
    public bool latched;

    // Use this for initialization
    void Start () {
        bod = gameObject.GetComponent<Rigidbody>();
	}

    void Update()
    {
        left = gameObject.transform.position + new Vector3(-(gameObject.transform.lossyScale.x / 2), gameObject.transform.lossyScale.y / 2, gameObject.transform.lossyScale.z / 2);
        right = gameObject.transform.position + gameObject.transform.lossyScale / 2;
        switch (scheme)
        {
            case controles.ARROW:
                if ((Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.PageUp) || Input.GetKey(KeyCode.Return)))
                {
                    freeSwing = true;
                    bod.useGravity = true;
                    latched = false;
                }
                else if ((!Input.GetKey(KeyCode.Home) && !Input.GetKey(KeyCode.LeftShift)) || (!Input.GetKey(KeyCode.PageUp) && !Input.GetKey(KeyCode.Return)))
                {
                    freeSwing = false;
                }
                break;
            case controles.WASD:
                if ((Input.GetKey(KeyCode.Q)) && (Input.GetKey(KeyCode.E)))
                {
                    freeSwing = true;
                    bod.useGravity = true;
                    latched = false;
                }
                else if (!(Input.GetKey(KeyCode.Q)) || !(Input.GetKey(KeyCode.E)))
                {
                    freeSwing = false;
                }
                break;
        }
        leftCheck = Physics.Raycast(left, gameObject.transform.forward, 1f);
        rightCheck = Physics.Raycast(right, gameObject.transform.forward, 1f);
        if (!freeSwing && (leftCheck || rightCheck))
        {
            bod.useGravity = false;
            latched = true;
            bod.velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 change = Vector3.zero;
        if (!latched) return;
        switch (scheme)
        {
            case controles.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    change.y += 1;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    change.y -= 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    change.x -= 1;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    change.x += 1;
                }
                break;
            case controles.ARROW:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    change.y += 1;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    change.y -= 1;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    change.x -= 1;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    change.x += 1;
                }
                break;

        }
        change = change.normalized * speed * Time.fixedDeltaTime;
        bod.transform.position += change;
    }
}
