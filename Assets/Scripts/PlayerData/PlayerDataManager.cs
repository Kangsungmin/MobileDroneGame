using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager playerDataManager { get; private set; }//
    public static DroneDatabase modelDatabase;
    public static int money { get; set; }
    public static int level { get; private set; }
    public static int exp { get; private set; }
    public static List<DroneModel> ownModels = new List<DroneModel>();
    public static DroneModel nowUsingModel;

    public Text MoneyView, LevelView;
    public Text Log;
    string ownStringData;
    void Awake()
    {
        playerDataManager = this;
        modelDatabase = transform.GetComponent<DroneDatabase>();
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("money", 5000);
        //PlayerPrefs.SetInt("level", 1);
        //PlayerPrefs.SetInt("exp", 0);
        //PlayerPrefs.SetString("ownModels", "1");//가진것 1번모델로 초기화
        if (!PlayerPrefs.HasKey("level"))
        {
            //=====초기 사용자 세팅=========
            PlayerPrefs.SetInt("money", 50);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("exp", 0);
            PlayerPrefs.SetString("ownModels", "1");//가진것 1번모델로 초기화
            PlayerPrefs.SetInt("UsingModel", 1);//현재 사용중인 모델 '1'
        }
        money = PlayerPrefs.GetInt("money");
        level = PlayerPrefs.GetInt("level");
        exp = PlayerPrefs.GetInt("exp");


        MoneyView.GetComponent<Text>().text = money.ToString();
        LevelView.GetComponent<Text>().text = level.ToString();
    }

    void Start()
    {
        //StartCoroutine("PlayerPrefStringLoad", "ownModels");
        PlayerPrefStringLoad("ownModels", ownStringData);//변수이름과 타입
        if(ownStringData != null)
            ownModels = ParseModels(ownStringData);
        int nowModelID = PlayerPrefs.GetInt("UsingModel");
        nowUsingModel = modelDatabase.FetchItemByID(nowModelID);
        Log.text = nowUsingModel.Title;
    }
    //===========돈, 경험치 수정[시작]==============
    public void IncreaseMoney(int value)
    {
        money = money + value;
        PlayerPrefs.SetInt("money", money);
    }

    public void DecreaseMoney(int value)
    {
        money -= value;
        PlayerPrefs.SetInt("money", money);
    }

    public void AddExp(int value)
    {
        exp += value;

        //렙업이 가능한 지 검사
        if (exp >= 100)
        {
            exp -= 100;
            level++;
        }
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("exp", exp);
    }
    //===========돈, 경험치 수정[끝]================

    public void DBRefresh()
    {
        money = PlayerPrefs.GetInt("money");
        level = PlayerPrefs.GetInt("level");
        exp = PlayerPrefs.GetInt("exp");
        ownModels = ParseModels(PlayerPrefs.GetString("ownModels"));
    }

    List<DroneModel> ParseModels(string DBstinrg)//string -> List
    {
        List<DroneModel> output = new List<DroneModel>();
        string[] ids = DBstinrg.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            DroneModel model = modelDatabase.FetchItemByID(int.Parse(ids[i]));
            output.Add(model);
        }
        return output;
    }

    IEnumerator PlayerPrefStringLoad(string name, string result)//PlayerPrefs로부터 data를 가져온다
    {
        result = PlayerPrefs.GetString(name);
        yield return result;
    }
    IEnumerator PlayerPrefIntegerLoad(string name, int result)//PlayerPrefs로부터 data를 가져온다
    {
        result = PlayerPrefs.GetInt(name);
        yield return result;
    }

}
