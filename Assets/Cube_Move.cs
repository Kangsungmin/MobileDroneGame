using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Move : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 50 * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 50 * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * 50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * 50 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * 50 * Time.deltaTime);
        }
    }
}
