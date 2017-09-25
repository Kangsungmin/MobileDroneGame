using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrab : MonoBehaviour {
    GameObject DroneClaw;
	// Use this for initialization
	void Start () {
        DroneClaw = GameObject.FindGameObjectWithTag("Player").transform.Find("Claw").gameObject;
	}
	
    public void Grab()
    {
        DroneClaw.GetComponent<Grab>().GrabMode();
    }
}
