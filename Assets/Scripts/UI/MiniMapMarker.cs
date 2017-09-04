using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMarker : MonoBehaviour {
    public GameObject Drone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Drone.transform.position.x, transform.position.y, Drone.transform.position.z);
        transform.forward = Drone.transform.forward;
	}
}
