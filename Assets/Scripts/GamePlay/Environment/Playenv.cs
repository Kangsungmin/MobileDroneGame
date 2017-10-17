using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * 게임전체적인 환경을 관리한다. 
 * 스테이지 관리
 * UI버튼 관리
 * 스크린 사이즈 관리
 */
public class Playenv : MonoBehaviour
{
    UIscripts UIManager;
    public string MapName;
    public int StageLevel;
    public int killCount = 0;
    public GameObject MssionPanel;
    public GameObject RatingView, MissionClearView;
    GameObject DroneBoxSearcher;
    public Text MissionExplainText;
    public static int SpawnBoxCount;
    public int MissionCount;//상자를 넣은 수.
    public static bool GameOver;
    public SceneFader fader;
    //박스아이템
    public GameObject ItemBox;
    public GameObject MiniMapMarkManager;
    //미션클리어UI
    public GameObject MissonBackground, Title_Mission, Title_Clear, SubTitle, MissionClearPanel;
    // Use this for initialization
    GameObject AllNpc, AllItems, AllSpwanArea;
    GameObject[] thisRoundEnemys;
    GameObject DroneSpawn;

    void Awake(){
        Time.timeScale = 1;
        GameOver = false;
        SpawnBoxCount = 0;
        MissionCount = 0;
        Screen.SetResolution(1280, 800, true);
        UIManager = GameObject.Find("UI").GetComponent<UIscripts>();
        AllNpc = GameObject.Find("KeyNpcs");
        AllItems = GameObject.Find("KeyItems");
        AllSpwanArea = GameObject.Find("SpawnAreas");

        MapName = SceneData.MapName;//현재 맵 이름
        StageLevel = int.Parse(SceneData.SceneLevelName);//현재 스테이지 레벨
        Debug.Log("맵 이름 : " + MapName + "\n스테이지 레벨: " + StageLevel);
        MssionPanel.SetActive(true);
        

        //자신이 현재 선택한 드론을 생성한다.
        /*
         * ==============현재 스테이지에 필요한 오브젝트만 활성화 시킨다.[시작]================ 
         * NPC 활성화/비활성화
         * Enemy 활성화/비활성화
         * Item 활성화/비활성화
         */
        for (int i = 0; i < AllNpc.transform.childCount; i++)
        {
            Transform StageNpcs = AllNpc.transform.GetChild(i);
            if (!StageNpcs.name.Equals("Stage" + StageLevel)) StageNpcs.gameObject.SetActive(false);
        }
        for (int i = 0; i < AllItems.transform.childCount; i++)
        {
            Transform StageItems = AllItems.transform.GetChild(i);
            if (!StageItems.name.Equals("Stage" + StageLevel)) StageItems.gameObject.SetActive(false);
        }
        for (int i = 0; i < AllSpwanArea.transform.childCount; i++)
        {
            Transform StageSpawnArea = AllSpwanArea.transform.GetChild(i);
            if (!StageSpawnArea.name.Equals("Stage" + StageLevel)) StageSpawnArea.gameObject.SetActive(false);
        }
        DroneSpawn = GameObject.Find("DroneSpawnArea");
        GameObject PlayerDrone = Resources.Load("Prefabs/Drones/Drone_" + PlayerDataManager.nowUsingModel.Title) as GameObject;
        Instantiate(PlayerDrone, DroneSpawn.transform.position, DroneSpawn.transform.rotation);
        //==============현재 스테이지에 필요한 오브젝트만 활성화 시킨다.[끝]==================

    }
    void Start()
    {
        DroneBoxSearcher = GameObject.Find("Claw");
        switch (StageLevel)
        {
            case 1:
                //미션 설명
                UIscripts.CountDown = 70.0f;
                MissionExplainText.text = "박스 1개를 지정된 구역으로 옮기세요.";
                break;
            case 2:
                UIscripts.CountDown = 60.0f;
                MissionExplainText.text = "제한시간(60초)동안 박스를 지정된 구역으로 최대한 옮기세요.";
                break;
                /*
            case 3:
                UIscripts.CountDown = 120.0f;
                MissionExplainText.text = "피자를 여러 사람에게 배달하세요.";
                MissionCount = 3;
                //현재 스테이지에 맞는 NPC 생성
                break;
            case 4:
                UIscripts.CountDown = 90.0f;
                MissionExplainText.text = "박스를 지정된 구역으로 옮기세요.";
                MissionCount = 2;
                break;
            case 5:
                UIscripts.CountDown = 120.0f;
                MissionExplainText.text = "박스를 지정된 구역으로 옮기세요.";
                MissionCount = 3;
                break;
                */
        }

        
    }

    void Update()
    {
        //현재 맵에 gen된 상자의 수를 확인한다. 일정 수 이하면 박스를 랜덤한 맵 위치에 생성한다.
        if(SpawnBoxCount < 20)
        {
            Vector3 BoxSpawnPos = new Vector3(Random.Range(-170.0f, 170.0f),79.0f,Random.Range(-170.0f, 170.0f));
            GameObject obj = Instantiate(ItemBox, BoxSpawnPos, Quaternion.identity);//랜덤한 위치에 박스 생성
            DroneBoxSearcher.SendMessage("AddBoxList", obj);//Grab스크립트 Box리스트에 추가
            MiniMapMarkManager.SendMessage("BoxGened", obj);//미니맵에도 박스 추가
            SpawnBoxCount++;
        }
    }
    //==============================미션 클리어[시작]====================================
    public void MissionEnd(int Rate, int getExp, int amountMoney)
    {
        //============보상[시작]=====================================================
        PlayerDataManager.playerDataManager.AddExp(getExp);//현재 경험치 
        PlayerDataManager.playerDataManager.IncreaseMoney(amountMoney);//얻은 돈
        //============보상[끝]==================================17.09.04 성민 최종수정
    }
    //==============================미션 클리어[끝]=============17.09.06 성민 최종수정
    public void GameEnd()
    {
        GameOver = true;//시간초과 게임 끝
        //정산
        switch (StageLevel)
        {
            
            case 1:
                //미션 완료 검사
                {
                    int getScore, getExp, getMoney;
                    if (MissionCount > 0)
                    {
                        getScore = 3;
                        getExp = 0;
                        getMoney = 10;
                    }
                    else
                    {
                        getScore = 0;
                        getExp = 0;
                        getMoney = 0;
                    }
                    UIManager.MissionEnd(getScore, getExp, getMoney); //점수,exp, money
                    MissionEnd(getScore, getExp, getMoney);
                    break;
                }
            case 2:
                //미션 완료 검사
                {
                    int getScore, getExp, getMoney;
                    if (MissionCount > PlayerDataManager.level * 2) //유저 Lev*2 이상일 시 3최상
                    {
                        getScore = 3;
                        getExp = 2 * MissionCount;
                        getMoney = 10 * MissionCount;
                    }
                    else if(MissionCount > PlayerDataManager.level * 1.5) //유저 Lev*1.5 이상일 시 2
                    {
                        getScore = 2;
                        getExp = 2 * MissionCount;
                        getMoney = 10 * MissionCount;
                    }
                    else if (MissionCount > 1) //1개이상 넣을 시
                    {
                        getScore = 1;
                        getExp = 2 * MissionCount;
                        getMoney = 10 * MissionCount;
                    }
                    else
                    {
                        getScore = 0;
                        getExp = 0;
                        getMoney = 0;
                    }
                    UIManager.MissionEnd(getScore, getExp, getMoney); //점수,exp, money
                    MissionEnd(getScore, getExp, getMoney);
                    break;
                }
        }
    }
    

    public void ExplainOk()
    {
        MssionPanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        UIManager.OnPause();
    }

    public void startMenu()
    {
        print("메뉴 씬 호출");
        Application.LoadLevel("Menu");
    }

    public void retrytGame()
    {
        print("게임 씬 호출");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}