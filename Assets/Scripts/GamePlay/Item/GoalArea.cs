﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    GameObject playEnvironment;
    GameObject Player;
    // Use this for initialization
    void Start()
    {
        playEnvironment = GameObject.Find("PlayEnvironment");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Box"))
        {
            playEnvironment.SendMessage("MissionCountPlus");
            //playEnvironment.SendMessage("MoneyPlus", 10);
            playEnvironment.SendMessage("AddParts", col.GetComponent<Box>().PartIdList());//획득한 부품 알린다
            UIscripts.CountDown += 25.0f;//25초 추가
            col.gameObject.SetActive(false);
            Player.transform.Find("Claw").SendMessage("RemoveBoxList", col.gameObject);//Claw의 BoxList에서 제거
            Player.SendMessage("GoalInParticlePlay");
            Playenv.SpawnBoxCount--;
        }
        else if(col.tag.Contains("Player"))
        {
            col.GetComponentInParent<Drone>().DropSomthing();
        }
    }
}