using UnityEngine;
using System.Collections;


public class calcPosition : MonoBehaviour {

    public Transform player1;
    public Transform player2;

    Camera cam;

    public float yOffset = 0.0f;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        // Have the camera look at the point inbetween the two players
        Vector3 newPos = transform.position;
        newPos.x = player1.position.x + (player2.position.x - player1.position.x) / 2;
        newPos.y = (player1.position.y + (player2.position.y - player1.position.y) / 2) + yOffset;
        transform.position = newPos;
	}
}
