using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour {
    GameObject playEnvironment;
    GameObject Player;
	// Use this for initialization
	void Start () {
        playEnvironment = GameObject.Find("PlayEnvironment");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Box"))
        {
            playEnvironment.GetComponent<Playenv>().MissionCount++;
            UIscripts.CountDown += 25.0f;//25초 추가
            col.gameObject.SetActive(false);//박스 비활성화
            Player.SendMessage("GoalInParticlePlay");
            Playenv.SpawnBoxCount--;
        }
    }
}
