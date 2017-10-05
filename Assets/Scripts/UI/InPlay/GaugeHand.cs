using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeHand : MonoBehaviour {
    string direction;
    float gaugePos;
    float speed;
	// Use this for initialization
	void Start () {
        direction = "R";
        speed = 5.0f;
        gaugePos = -120.0f;
	}
	
	// Update is called once per frame
    /*
     * 게이지는 -120에서 120까지 움직인다.
     * -10 ~ 10 에서 버튼을 놓아야 상자가 들린다.
     */
	void Update () {
        
        if (direction.Equals("R"))
        {
            gaugePos += 1.0f * speed;
            if (gaugePos >= 120) direction = "L";
        }
        else
        {
            gaugePos -= 1.0f * speed;
            if (gaugePos <= -120.0f) direction = "R";
        }
	}
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(gaugePos, 0, 0);
    }
    public void ResetGauge()
    {
        gaugePos = -120.0f;
    }
}
