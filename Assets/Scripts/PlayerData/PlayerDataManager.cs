using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager playerDataManager { get; private set; }//
    //public DroneDatabase modelDatabase;
    public static int spanner { get; set; }
    public static int money { get; set; }
    public static int level { get; private set; }
    public static int exp { get; private set; }
    public static List<DroneModel> ownModels = new List<DroneModel>();
    public static DroneModel nowUsingModel;

    public Text SpannerView, MoneyView, LevelView;
    string ownStringData;
    bool[] Loadmemory = new bool[6]{ false, false, false, false, false, false };
    private IEnumerator coroutine;
    void Awake()
    {
        playerDataManager = this;
        //초기화 할때 주석 제거
        /*
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("spanner",5);
        PlayerPrefs.SetInt("money", 500);
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("exp", 0);
        PlayerPrefs.SetInt("nowModel", 1);//현재 사용중인 모델 '1'
        PlayerPrefs.SetString("Models", "1");//가진것 1번모델로 초기화
        */
        if (!PlayerPrefs.HasKey("level"))
        {
            //=====초기 사용자 세팅=========
            PlayerPrefs.SetInt("spanner",5);
            PlayerPrefs.SetInt("money", 50);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("exp", 0);
            PlayerPrefs.SetInt("nowModel", 1);//현재 사용중인 모델 '1'
            PlayerPrefs.SetString("Models", "1");//가진것 1번모델로 초기화
            
        }
        //modelDatabase 구성될 때 까지 대기

    }

    void Start()
    {
        
        print(DroneDatabase.path);
        //Log.text = "경로 : " + DroneDatabase.path + "\n";
        coroutine = DroneDBLoad();
        StartCoroutine(coroutine);//DB로드 완료후, PlayerPrefs을 로드한다.
        
    }

    void Update()
    {
        if (ownModels.Count != 0) ;

        if (nowUsingModel != null) ;

        if (Loadmemory[0] && Loadmemory[0] && Loadmemory[0] && Loadmemory[0] && Loadmemory[0] && Loadmemory[0])
        {
            spanner = PlayerPrefs.GetInt("spanner");
            SpannerView.text = spanner.ToString() + "/5";
        }

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

    public void DBRefresh()//PlayerPrefs 다시 불러와서 새로고침
    {
        coroutine = PlayerPrefsLoad("spanner");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("money");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("level");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("exp");
        StartCoroutine(coroutine);

        coroutine = PlayerPrefsLoad("Models");
        StartCoroutine(coroutine);//PlayerPrefs 데이터네임, 결과 받을 변수
        coroutine = PlayerPrefsLoad("nowModel");
        StartCoroutine(coroutine);
    }

    public List<DroneModel> ParseModels(string DBstinrg)//string -> List
    {
        List<DroneModel> output = new List<DroneModel>();
        string[] ids = DBstinrg.Split(',');//
        //Log.text += "보유리스트 길이 : "+ids.Length+"\n";
        for (int i = 0; i < ids.Length; i++)
        {
            DroneModel model = DroneDatabase.FetchDroneByID(int.Parse(ids[i]));
            output.Add(model);
        }
        return output;
    }
    public IEnumerator DroneDBLoad()
    {
        while (!DroneDatabase.isLoaded)
        {
            yield return null;
        }
        //DroneDB로딩이 완료
        coroutine = PlayerPrefsLoad("spanner");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("money");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("level");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("exp");
        StartCoroutine(coroutine);

        coroutine = PlayerPrefsLoad("nowModel");
        StartCoroutine(coroutine);
        coroutine = PlayerPrefsLoad("Models");
        StartCoroutine(coroutine);//PlayerPrefs 데이터네임, 결과 받을 변수
    }


    public IEnumerator PlayerPrefsLoad(string dataName)
    {
        switch (dataName)
        {
            case "Models":
                string load = "-1";
                load = PlayerPrefs.GetString(dataName);
                while (load.Equals("-1"))
                {
                    //Log.text = "\n 리스트 데이터 로딩중";
                    yield return null;
                }
                //Log.text += load + "리스트 끝\n";
                Loadmemory[0] = true;
                ownModels = ParseModels(load);//ownModels로 변환
                break;

            case "nowModel":
                int id = -1;
                id = PlayerPrefs.GetInt(dataName);
                while (id == -1)
                {
                    //Log.text = "\n 장착 데이터 로딩중";
                    yield return null;
                }
                nowUsingModel = DroneDatabase.FetchDroneByID(id);
                Loadmemory[1] = true;
                break;

            case "money":
                int amount = -1;
                amount = PlayerPrefs.GetInt(dataName);
                while (amount == -1)
                {
                    //Log.text = "\n money 데이터 로딩중";
                    yield return null;
                }
                
                money = amount;
                MoneyView.GetComponent<Text>().text = money.ToString();
                Loadmemory[2] = true;
                break;

            case "level":
                int lev = -1;
                lev = PlayerPrefs.GetInt(dataName);
                while (lev == -1)
                {
                    //Log.text = "\n lev 데이터 로딩중";
                    yield return null;
                }
                level = lev;
                LevelView.GetComponent<Text>().text = level.ToString();
                Loadmemory[3] = true;
                break;

            case "exp":
                int e = -1;
                e = PlayerPrefs.GetInt(dataName);
                while (e == -1)
                {
                    //Log.text = "\n lev 데이터 로딩중";
                    yield return null;
                }
                exp = e;
                Loadmemory[4] = true;
                break;

            case "spanner":
                int s = -1;
                s = PlayerPrefs.GetInt(dataName);
                while (s == -1)
                {
                    yield return null;
                }
                spanner = s;
                Loadmemory[5] = true;
                break;

            default:
                break;
        }
    }

}
