using UnityEngine;
using System.Collections;

public class MovementV2 : MonoBehaviour {

	//Variables
	public float speed = 1f; //Speed multiplier by which the character will move
	public float JumpPower = 20;

	public bool _______________________;

	private float HorizIn = 0f; //Variable to hold the Horizontal Input from player
	private float VertIn = 0f; //Variable to hold the Vertical Input from player
	private bool JumpIn;

	//Rotation variables
	private float FinalRotation;
	private float TurnAmount;
	//private float TurnSpeed = 90;

	//Camera Variables 
	private Vector3 CamDir;
	private Vector3 CamRelative;

	//Jump Variables
	public bool grounded = true;

	//---------------------------------End of Variables

	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision coll)
	{
		Debug.Log ("Collided with Something");
		if (coll.gameObject.tag == "Ground")grounded = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Obtain Input from player
		HorizIn = Input.GetAxis ("Horizontal");
		VertIn = Input.GetAxis("Vertical");
		JumpIn = Input.GetKeyDown (KeyCode.Space);
		//Debug.Log ("Vertical and Horizontal Inputs are: " + VertIn + " , " + HorizIn);
		//

		//Obtain the Camera direction that is passed to the character
		if (Camera.main != null) 
		{
			CamDir = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)).normalized;
			CamRelative = VertIn * CamDir + HorizIn * Camera.main.transform.right;
		}
		else Debug.Log ("Main Camera is Not attached to Player Controller");



		//Check if the character has received input to move
		if (HorizIn != 0 || VertIn != 0) {
			//Calculate the Rotation based on Horizontal Inputs
			if (HorizIn != 0) {
				Vector3 move = Vector3.one;
				float TurnSpeed = Mathf.Lerp (180, 180, CamRelative.z);
				TurnAmount = Mathf.Atan2 (move.x, move.z);
				FinalRotation = + TurnAmount * TurnSpeed * Time.deltaTime * HorizIn;
				
				//Move and Rotate
				transform.Rotate(0, (FinalRotation % 360), 0);
			}
			//Calculate the Forward movement based on Vertical Inputs
			if (VertIn != 0) {
				Vector3 temp = gameObject.GetComponent<Rigidbody>().velocity;
				//gameObject.GetComponent<Rigidbody> ().velocity = (CamRelative) * speed;
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(CamRelative.x , temp.y/speed ,CamRelative.z)*speed;
			}
		} 
		else 
		{
			Vector3 temp = gameObject.GetComponent<Rigidbody>().velocity;
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(temp.x , temp.y, temp.z);
		}

		//Check for input on jumping and Jump if appropriate
		if (JumpIn && grounded)
		{
			Debug.Log ("Jumping!");
			Vector3 temp = gameObject.GetComponent<Rigidbody>().velocity;
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (temp.x, JumpPower, temp.z);
			grounded = false;
		}
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		//Debug.Log (gameObject.GetComponent<Rigidbody> ().velocity);
		
	}
}
