using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float speed = 1f;

	private float TurnAmount;
	private float ForwardAmount;
	private Vector3 CamForward; 
	private bool jump;
	//private bool grounded = true;
	private Vector3 m_Move;  
	private float  GroundCheckDistance = 0.1f;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
	
	}


	void FixedUpdate()
		{

		float Hori = Input.GetAxis ("Horizontal");
		float Vert = Input.GetAxis ("Vertical");
		jump = Input.GetKeyDown (KeyCode.Space);

		if (Camera.main.transform != null) {
			// calculate camera relative direction to move:
			CamForward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)).normalized;
			m_Move = Vert * CamForward + Hori * Camera.main.transform.right;
		}
		//Set animations to play anim.SetBool

		if (jump &! CheckGroundStatus()) 
		{
			m_Move.y = 100;
			jump =false;
			//grounded = false;
		}

		if (Hori != 0f || Vert != 0f) {
			if(m_Move.magnitude > 1f) m_Move.Normalize();
			Vector3 move = m_Move;
			move = transform.InverseTransformDirection(move);
			move = Vector3.ProjectOnPlane(move, Vector3.up);
			TurnAmount = Mathf.Atan2(move.x, move.z);
			ForwardAmount = move.z;
			if(ForwardAmount <= 0.0)
			{
				float backturn;
				backturn = Input.GetAxis ("Horizontal");
				backturn = Mathf.Clamp(backturn,-1.0f,1.0f);
				transform.Rotate (0, backturn,0);
			}
			else
			{
				float turnSpeed = Mathf.Lerp(90, 180, ForwardAmount);
				transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
			}
			//Vector3 v = Vector3.zero;
			//v.y = gameObject.GetComponent<Rigidbody>().velocity.y;
			//gameObject.GetComponent<Rigidbody>().velocity = v;
		}
		gameObject.GetComponent<Rigidbody> ().velocity =(m_Move) * speed;
	}





	private bool CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, GroundCheckDistance))
		{
			//m_GroundNormal = hitInfo.normal;
			//m_Animator.applyRootMotion = true;
			Debug.Log("Grounded is true");
			return true;
		}
		else
		{
			//m_GroundNormal = Vector3.up;
			//m_Animator.applyRootMotion = false;
			Debug.Log("Grounded is false");
			return false;
		}
	}
}
