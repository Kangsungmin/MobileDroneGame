using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Trigger : MonoBehaviour {
    public AudioClip sndCharge;
    public GameObject aim;
   
    // Use this for initialization
    void Start () {
        aim.active = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "enemy")
            aim.active = true;
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "enemy")
            aim.active = false;
    }

    private void OnCollisionEnter(Collision collision)//오브젝트와 충돌시 호출.
    {
        if (collision.transform.tag != "Wall")
        {
            aim.active = false;
        }
        print("aim 부딪힘");
        aim.active = false;

    }
}
