using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour {
    GameObject playEnvironment;
	// Use this for initialization
	void Start () {
        playEnvironment = GameObject.Find("PlayEnvironment");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Box"))
        {
            playEnvironment.GetComponent<Playenv>().MissionCount--;
        }
    }
}
