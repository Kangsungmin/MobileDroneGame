using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class UIscripts : MonoBehaviour {
    GameObject Player, PlayEnvironment;
    public GameObject damagedImg, PauseMenu;
    public GameObject MissonBackground, Title_Mission, Title_Clear, SubTitle, MissionClearPanel, MissionPanelButtons;
    public GameObject RatingView, MissionClearView;
    public Image fireBtn, joystickLeft, joystickRight;
    private float health = 100.0f, maxHealth = 100.0f;
    private float fuel = 100.0f, maxFuel = 100.0f;
    Image healthBar, fuelBar;
    public Text MissionClearEnglish, MissionClearKorean;
    //스톱워치
    bool MissonEnd;
    //public static Stopwatch stopwatch;
    public static float CountDown = -1.0f;
    public Text TimeView;
    IEnumerator corutine;
    void Awake()
    {
        MissonEnd = false;
    }
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayEnvironment = GameObject.Find("PlayEnvironment");
        //stopwatch = new Stopwatch();
        //stopwatch.Start();

        healthBar = transform.Find("HP").Find("HP_bar").GetComponent<Image>();
        fuelBar = transform.Find("FUEL").Find("Fuel_bar").GetComponent<Image>();
        damagedImg.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
        if (!Playenv.GameOver)
        {
            health = Player.gameObject.GetComponent<Drone>().Hp;
            fuel = Player.gameObject.GetComponent<Drone>().Fuel;
            healthBar.fillAmount = (float)health / (float)maxHealth;
            fuelBar.fillAmount = (float)fuel / (float)maxFuel;
            if ((int)CountDown == 0 || health <= 0.0f || fuel <= 0.0f)//게임종료
            {
                //PlayEnvironment에 제한시간 종료 알림.
                PlayEnvironment.SendMessage("GameEnd");
            }
            else if (MissonEnd)//미션 종료시
            {
                Player.GetComponent<Rigidbody>().isKinematic = true;//드론 멈춤
                Player.GetComponent<Drone>().DronePowerOn = false;
            }
            else CountDown -= Time.deltaTime;//1초씩 감소

            TimeView.text = (int)CountDown + "초 남았습니다.";
            if (CountDown < 20) TimeView.color = Color.red;
            else TimeView.color = Color.white;
            //TimeView.GetComponent<Text>().text = stopwatch.Elapsed.Minutes + " : " + stopwatch.Elapsed.Seconds + " : " + stopwatch.Elapsed.Milliseconds / 100;
        }

    }

    public void MissionEnd(int Rate, int getExp, int amountMoney)
    {
        //stopwatch.Reset();
        //1.백그라운드 활성화
        MissonBackground.SetActive(true);
        //2.Mission & Subtitle 활성화 
        Title_Mission.SetActive(true);
        SubTitle.SetActive(true);
        //3.Clear & 패널 활성화 (점차)
        Title_Clear.SetActive(true);
        MissionClearPanel.SetActive(true);
        object[] parms = new object[2]{ MissionPanelButtons, 2.3f };
        corutine = UiActiveReserve(parms);
        StartCoroutine(corutine);//미션 버튼 2.5초 후 활성화
        RatingView.GetComponent<Rating>().SetRate(Rate);//별 개수

        //============보상출력[시작]=================================================
        RatingView.GetComponent<Rating>().SetRate(Rate);//별 개수
        MissionClearView.GetComponent<MissionReward>().ViewUpdate(SceneData.SceneLevelName, amountMoney, PlayerDataManager.level, PlayerDataManager.exp);
        //============보상출력[끝]==============================17.09.15 성민 최종수정
        MissonEnd = true;
    }


    //UiActiveReserve(object[] parms) : 게임오브젝트를 예약하여 해당시간 후에 활성화시키는 함수
    //ㄴ>첫번째 인자(parms[0]) : 게임오브젝트, 두번째 인자(parms[1]) : 시간
    IEnumerator UiActiveReserve(object[] parms)
    {
        yield return new WaitForSeconds((float)parms[1]);
        GameObject reservedObject = (GameObject) parms[0];
        reservedObject.SetActive(true);
    }

    public void damageAni()
    {
        damagedImg.SetActive(true);
        Invoke("DamageEnd",1.0f);
    }

    void DamageEnd()
    {
        damagedImg.SetActive(false);
    }

    public void OnPause()
    {
        PauseMenu.SetActive(true);
    }

    
    public void ContinuePlay()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
