using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionReward : MonoBehaviour {
    public Text StageGetMoney;
    public Text NowLevel;
    public Text NowExp;
    public Text NowStage;

    public void ViewUpdate(string stage, int getMoney, int nowLevel, int nowExp)
    {
        StageGetMoney.GetComponent<Text>().text = getMoney.ToString();
        NowLevel.GetComponent<Text>().text = nowLevel.ToString();
        NowExp.GetComponent<Text>().text = nowExp.ToString();
        NowStage.GetComponent<Text>().text = "스테이지."+stage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
