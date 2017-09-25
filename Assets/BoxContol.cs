using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContol : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        transform.position = transform.GetComponent<BoxCollider>().transform.position; 
        transform.rotation = transform.GetComponent<BoxCollider>().transform.rotation;
    }
}
