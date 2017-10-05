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
    public Text MissionExplainText;
    public int MissionCount;
    public SceneFader fader;
    //미션클리어UI
    public GameObject MissonBackground, Title_Mission, Title_Clear, SubTitle, MissionClearPanel;
    // Use this for initialization
    GameObject AllNpc, AllItems, AllSpwanArea;
    GameObject[] thisRoundEnemys;
    GameObject DroneSpawn;

    void Awake(){
        Time.timeScale = 1;
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
        
        switch (StageLevel)
        {
            case 1:
                //미션 설명
                UIscripts.CountDown = 70.0f;
                MissionExplainText.text = "박스를 지정된 구역으로 옮기세요.";
                MissionCount = 1;
                break;
            case 2:
                UIscripts.CountDown = 60.0f;
                MissionExplainText.text = "박스를 지정된 구역으로 옮기세요.";
                MissionCount = 2;
                break;
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
        }

        
    }

    void Update()
    {
        if (MissionCount > -1 )
        {
            switch (StageLevel)
            {
                case 1:
                    //미션 완료 검사
                    if (MissionCount == 0)
                    {
                        //현재까지 걸리 시간 측정, Rating
                        int score;
                        if (UIscripts.CountDown < 15.0f)
                        {
                            score = 1;
                        }
                        else if (UIscripts.CountDown < 30.0f)
                        {
                            score = 2;
                        }
                        else//별 3개
                        {
                            score = 3;
                        }
                        UIManager.MissionEnd(score, 30, 25);
                        MissionEnd(score, 30, 25);
                        MissionCount = -1;
                    }
                    //NPC에게 피자 배달이 올 때까지 검사.(미션 클리어시 까지 대기한다.)
                    break;
                case 2:
                    /*
                    int count = 0;
                    foreach (GameObject enemy in thisRoundEnemys)
                    {
                        if (enemy.activeSelf) count++;
                    }
                    MissionCount = count;
                    */
                    if (MissionCount == 0)
                    {
                        int score = 0;
                        //현재까지 걸린시간과 남은 HP를 계산하여 별을 준다.
                        if (UIscripts.CountDown < 10.0f)//
                        {
                            score = 1;
                        }
                        else if (UIscripts.CountDown < 20.0f)//
                        {
                            score = 2;
                        }
                        else score = 3;
                        UIManager.MissionEnd(score, 45, 20);
                        MissionEnd(score, 45, 20);
                        MissionCount = -1;
                    }
                    break;
                case 3:
                    if (MissionCount == 0)
                    {
                        int score;
                        if (UIscripts.CountDown < 20.0f)
                        {
                            score = 1;
                        }
                        else if (UIscripts.CountDown < 40.0f)
                        {
                            score = 2;
                        }
                        else//별 3개
                        {
                            score = 3;
                        }
                        UIManager.MissionEnd(score, 30, 25);
                        MissionEnd(score, 120, 85);
                        MissionCount = -1;
                    }
                    break;
                case 4:
                    if (MissionCount == 0)
                    {
                        int score = 0;
                        //현재까지 걸린시간과 남은 HP를 계산하여 별을 준다.
                        if (UIscripts.CountDown < 10.0f)//85초 초과 OR HP 20이하
                        {
                            score = 1;
                        }
                        else if (UIscripts.CountDown < 30.0f)//65초 초과하거나 OR HP가 60이하일때
                        {
                            score = 2;
                        }
                        else score = 3;
                        UIManager.MissionEnd(score, 45, 20);
                        MissionEnd(score, 45, 20);
                        MissionCount = -1;
                    }
                    break;
                case 5:
                    if (MissionCount == 0)
                    {
                        int score = 0;
                        //현재까지 걸린시간과 남은 HP를 계산하여 별을 준다.
                        if (UIscripts.CountDown < 15.0f)//85초 초과 OR HP 20이하
                        {
                            score = 1;
                        }
                        else if (UIscripts.CountDown < 35.0f)//65초 초과하거나 OR HP가 60이하일때
                        {
                            score = 2;
                        }
                        else score = 3;
                        UIManager.MissionEnd(score, 45, 20);
                        MissionEnd(score, 45, 20);
                        MissionCount = -1;
                    }
                    break;
            }
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
    public void MissionFail()
    {
        MissionCount = -1;
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