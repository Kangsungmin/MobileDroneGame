using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class UIscripts : MonoBehaviour {
    GameObject Player;
    public GameObject damagedImg;
    public GameObject MissonBackground, Title_Mission, Title_Clear, SubTitle, MissionClearPanel;
    public GameObject RatingView, MissionClearView;
    public Image fireBtn, joystickLeft, joystickRight ;
    private float health = 100.0f, maxHealth = 100.0f;
    private float fuel = 100.0f, maxFuel = 100.0f;
    Image healthBar, fuelBar;

    //스톱워치
    public static Stopwatch stopwatch;
    public Text TimeView; 
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        stopwatch = new Stopwatch();
        stopwatch.Start();
        healthBar = transform.FindChild("HP").FindChild("HP_bar").GetComponent<Image>();
        fuelBar = transform.FindChild("FUEL").FindChild("Fuel_bar").GetComponent<Image>();
        damagedImg.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
            health = Player.gameObject.GetComponent<Drone>().Hp;
            fuel = Player.gameObject.GetComponent<Drone>().Fuel;

            healthBar.fillAmount = (float)health / (float)maxHealth;
            fuelBar.fillAmount = (float)fuel / (float)maxFuel;
            if (health <= 0.0f || fuel <= 0.0f)//게임오버시
            {
                Player.gameObject.GetComponent<Drone>().GameOver = true;
                MissionEnd(0, 0, 0);
            }

            TimeView.GetComponent<Text>().text = stopwatch.Elapsed.Minutes + " : " + stopwatch.Elapsed.Seconds + " : " + stopwatch.Elapsed.Milliseconds / 100;
    }

    public void MissionEnd(int Rate, int getExp, int amountMoney)
    {
        stopwatch.Reset();
        //1.백그라운드 활성화
        MissonBackground.SetActive(true);
        //2.Mission & Subtitle 활성화 
        Title_Mission.SetActive(true);
        SubTitle.SetActive(true);
        //3.Clear & 패널 활성화 (점차)
        Title_Clear.SetActive(true);
        MissionClearPanel.SetActive(true);
        RatingView.GetComponent<Rating>().SetRate(Rate);//별 개수

        //============보상출력[시작]=================================================
        RatingView.GetComponent<Rating>().SetRate(Rate);//별 개수
        MissionClearView.GetComponent<MissionReward>().ViewUpdate(SceneData.SceneLevelName, amountMoney, PlayerDataManager.level, PlayerDataManager.exp);
        //============보상출력[끝]==============================17.09.15 성민 최종수정


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

}
