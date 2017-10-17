using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkGenerator : MonoBehaviour {
    public GameObject NpcMark, BoxMark, DestinationMark;
    GameObject[] Npcs, Boxes;
    GameObject Destination;
    List<GameObject> EnemyMarks;
    // Use this for initialization
    void Awake()
    {
        EnemyMarks = new List<GameObject>();

    }
    void Start()
    {
        //현재 스테이지 받아옴. 스테이지에 따른 해당 NPC만 미니맵에 출력
        int StageLevel = int.Parse(SceneData.SceneLevelName);
        //
        Npcs = GameObject.FindGameObjectsWithTag("NPC_R" + StageLevel);//NPC_Rn테그를 가진 게임오브젝트 배열을 리턴
        Boxes = GameObject.FindGameObjectsWithTag("Box");
        Destination = GameObject.Find("Area_R" + StageLevel);
        //Enemys = GameObject.FindGameObjectsWithTag("ENEMY_R" + StageLevel);
        if (Npcs.Length > 0)
        {
            foreach (GameObject Npc in Npcs)
            {
                GameObject temp = Instantiate(NpcMark, new Vector3(0, 1248, 0), Quaternion.identity);
                temp.GetComponent<MinimapMark>().target = Npc;//미니맵 타겟 지정
            }
        }
        if (Boxes.Length > 0)//박스 미니맵 생성
        {
            foreach (GameObject Box in Boxes)
            {
                GameObject temp = Instantiate(BoxMark, new Vector3(0, 1248.5f, 0), Quaternion.identity);
                temp.GetComponent<MinimapMark>().target = Box;
            }
        }
        if(Destination != null)//목적지 미니맵 생성
        {
            GameObject temp = Instantiate(DestinationMark, new Vector3(0, 1249, 0), Quaternion.identity);
            temp.GetComponent<MinimapMark>().target = Destination;
        }

    }

    public void BoxGened(GameObject Box)//아이템이 새로 추가되어 미니맵에 표시할 때 호출한다.
    {
        GameObject temp = Instantiate(BoxMark, new Vector3(0, 1248.5f, 0), Quaternion.identity);
        temp.GetComponent<MinimapMark>().target = Box;
    }
    /*
    void EnemyDead(GameObject sender)//박스제거
    {
        for (int i=0; i<EnemyMarks.Count; i++)
        {
            if (GameObject.ReferenceEquals(EnemyMarks[i].GetComponent<NpcMark>().target, sender)) EnemyMarks[i].SetActive(false); //마크 제거
        }
    }
    */
}
