using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMark : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Update () {
        if (target != null) {
            transform.position = new Vector3(target.transform.position.x, 1248, target.transform.position.z);
            transform.forward = target.transform.forward;
        }
	}
}
