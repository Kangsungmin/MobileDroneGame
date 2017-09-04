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
                    //현재까지 걸리 시간 측정
                    MissionClear(0,0,0,0);
                }
                //NPC에게 피자 배달이 올 때까지 검사.(미션 클리어시 까지 대기한다.)
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public void MissionClear(int Rate, int nowLevel, int nowExp, int amountMoney)
    {
        Time.timeScale = 0;//게임 정지

        //1.백그라운드 활성화
        MissonBackground.SetActive(true);
        //2.Mission & Subtitle 활성화 
        Title_Mission.SetActive(true);
        SubTitle.SetActive(true);
        //3.Clear & 패널 활성화 (점차)
        Title_Clear.SetActive(true);
        MissionClearPanel.SetActive(true);

    }



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