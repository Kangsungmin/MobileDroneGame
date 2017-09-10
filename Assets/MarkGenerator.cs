using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkGenerator : MonoBehaviour {
    public GameObject NpcMark;
    GameObject[] Npcs;
    // Use this for initialization
    void Start()
    {
        //현재 스테이지 받아옴. 스테이지에 따른 해당 NPC만 미니맵에 출력
        int StageLevel = int.Parse(SceneData.SceneLevelName);

        Npcs = GameObject.FindGameObjectsWithTag("NPC_R" + StageLevel);//NPC_Rn테그를 가진 게임오브젝트 배열을 리턴
        if(Npcs.Length > 0)
        {
            foreach (GameObject Npc in Npcs)
            {
                GameObject temp = Instantiate(NpcMark, new Vector3(0, 1248, 0), Quaternion.identity);
                temp.GetComponent<NpcMark>().target = Npc;//미니맵 타겟 지정
            }
        }

    }
}
