using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class processModel : MonoBehaviour {
    public float yRotation = 0.0F;
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        yRotation = 40 * Time.deltaTime;
        transform.Rotate(Vector3.up * yRotation);
    }
}
