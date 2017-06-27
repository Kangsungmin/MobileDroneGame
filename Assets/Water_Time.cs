using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Water_Time : MonoBehaviour {

    public Transform waterTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(waterTrans.position.y < 300)
            StartCoroutine("AddToForce");
    }

    IEnumerator AddToForce()
    {
        Vector3 v = waterTrans.position;
        v.y += 0.02f;
        waterTrans.position = v; // Drone의 위(y축)으로 추력만큼 힘을 가한다.
        yield return new WaitForSeconds(15f);//해당 메소드에 1초 마다 호출
    }
    public float getWaterPosition()
    {
        return waterTrans.position.y;
    }
}
