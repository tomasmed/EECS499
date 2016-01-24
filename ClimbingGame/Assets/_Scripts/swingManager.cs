using UnityEngine;
using System.Collections;

public class swingManager : MonoBehaviour {
    public GameObject other;
    public float length;
    Rigidbody bod;

	// Use this for initialization
	void Start () {
        bod = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dist = this.transform.position - other.transform.position;
        if(dist.magnitude > length)
        {
            this.gameObject.transform.position = other.gameObject.transform.position + (dist.normalized * length);
            Vector3 vel = bod.velocity;
            float mag = vel.magnitude;
            vel = vel.normalized - (dist.normalized * Vector3.Dot(dist.normalized, vel.normalized));
            bod.velocity = vel * mag;
        }
    }
}
