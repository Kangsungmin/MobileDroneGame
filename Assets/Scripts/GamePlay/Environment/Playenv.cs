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
    static SceneData sceneData = new SceneData();
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
    void Start()
    {
        Time.timeScale = 1;
        Screen.SetResolution(1280, 800, true);
        MapName = SceneData.MapName;//현재 맵 이름
        StageLevel = int.Parse(SceneData.SceneLevelName);//현재 스테이지 레벨
        Debug.Log("맵 이름 : " + MapName + "\n스테이지 레벨: " + StageLevel);

        MssionPanel.SetActive(true);
        switch (StageLevel)
        {
            case 1:
                //미션 설명
                MissionExplainText.text = "피자가게 앞에서 피자를 얻고, 피자를 원하는 사람에게 배달하세요.";
                MissionCount = 1;

                //미션완료 메세지 출력
                //메뉴 씬으로 전환
                break;
            case 2:
                break;
            case 3:
                MissionExplainText.text = "피자를 여러 사람에게 배달하세요.";
                MissionCount = 4;
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    void Update()
    {
        switch (StageLevel)
        {
            case 1:
                //미션 완료 검사
                if (MissionCount == 0)
                {
                    //현재까지 걸리 시간 측정, Rating
                    int score;
                    if (UIscripts.stopwatch.Elapsed.Seconds > 120)
                    {
                        score = 1;
                    }
                    else if (UIscripts.stopwatch.Elapsed.Seconds > 45)
                    {
                        score = 2;
                    }
                    else//별 3개
                    {
                        score = 3;
                    }

                    MissionClear(score,30,25);
                    MissionCount = -1;
                }
                //NPC에게 피자 배달이 올 때까지 검사.(미션 클리어시 까지 대기한다.)
                break;
            case 2:
                break;
            case 3:
                if (MissionCount == 0)
                {
                    int score;
                    if (UIscripts.stopwatch.Elapsed.Seconds > 500)
                    {
                        score = 1;
                    }
                    else if (UIscripts.stopwatch.Elapsed.Seconds > 300)
                    {
                        score = 2;
                    }
                    else//별 3개
                    {
                        score = 3;
                    }
                    MissionClear(score, 30, 25);
                    MissionCount = -1;
                }
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
    //==============================미션 클리어[시작]====================================
    public void MissionClear(int Rate, int getExp, int amountMoney)
    {
        //Time.timeScale = 0;//게임 정지

        UIscripts.stopwatch.Reset();

        //1.백그라운드 활성화
        MissonBackground.SetActive(true);
        //2.Mission & Subtitle 활성화 
        Title_Mission.SetActive(true);
        SubTitle.SetActive(true);
        //3.Clear & 패널 활성화 (점차)
        Title_Clear.SetActive(true);
        MissionClearPanel.SetActive(true);

        //============보상[시작]====================================================
        PlayerDataManager.playerDataManager.AddExp(getExp);//현재 경험치 
        PlayerDataManager.playerDataManager.IncreaseMoney(amountMoney);//얻은 돈
        //============보상[끝]==================================17.09.04 성민 최종수정

        //============보상출력[시작]=================================================

        RatingView.GetComponent<Rating>().SetRate(Rate);//별 개수
        MissionClearView.GetComponent<MissionReward>().ViewUpdate(amountMoney, PlayerDataManager.level, PlayerDataManager.exp);
        //============보상출력[끝]==============================17.09.05 성민 최종수정

    }
    //==============================미션 클리어[끝]=============17.09.06 성민 최종수정


    public void ExplainOk()
    {
        MssionPanel.SetActive(false);
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