using UnityEngine;
using System.Collections;

public class ropeManager : MonoBehaviour {
    public GameObject member1;
    public GameObject member2;
    LineRenderer rope;

	// Use this for initialization
	void Start () {
        rope = member1.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        rope.SetPosition(0, member1.transform.position);
        rope.SetPosition(1, member2.transform.position);
	}
}
