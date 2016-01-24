using UnityEngine;
using System.Collections;

public class swingManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public nonCharacterControllerMovement p1;
    public nonCharacterControllerMovement p2;
    public float length;
    Rigidbody bod1;
    Rigidbody bod2;

	// Use this for initialization
	void Start () {
        bod1 = player1.gameObject.GetComponent<Rigidbody>();
        bod2 = player2.gameObject.GetComponent<Rigidbody>();
        p1 = player1.GetComponent<nonCharacterControllerMovement>();
        p2 = player2.GetComponent<nonCharacterControllerMovement>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (p1.latched && !p2.latched)
        {
            Vector3 dist = player2.transform.position - player1.transform.position;
            if (dist.magnitude > length)
            {
                player2.gameObject.transform.position = player1.transform.position + (dist.normalized * ((length)));
                Vector3 vel = bod2.velocity;
                float mag = vel.magnitude;
                vel = vel.normalized - ((dist.normalized * Vector3.Dot(dist.normalized, vel.normalized)));
                bod2.velocity = vel * mag;
            }
        }
        else if (!p1.latched && p2.latched)
        {
            Vector3 dist = player1.transform.position - player2.transform.position;
            if (dist.magnitude > length)
            {
                player1.gameObject.transform.position = player2.transform.position + (dist.normalized * ((length)));
                Vector3 vel = bod1.velocity;
                float mag = vel.magnitude;
                vel = vel.normalized - ((dist.normalized * Vector3.Dot(dist.normalized, vel.normalized)));
                bod1.velocity = vel * mag;
            }
        }
        else
        {
            Vector3 dist = player2.transform.position - player1.transform.position;
            if (dist.magnitude > length)
            {
                player2.gameObject.transform.position += (dist.normalized * ((length - dist.magnitude) / 2));
                player1.gameObject.transform.position -= (dist.normalized * ((length - dist.magnitude) / 2));
                Vector3 vel = bod1.velocity;
                float mag = vel.magnitude;
                vel = vel.normalized - ((dist.normalized * Vector3.Dot(dist.normalized, vel.normalized)) / 2);
                bod1.velocity = vel * mag;
                vel = bod2.velocity;
                mag = vel.magnitude;
                vel = vel.normalized - ((dist.normalized * Vector3.Dot(dist.normalized, vel.normalized)) / 2);
                bod1.velocity = vel * mag;
            }
        }
    }
}
