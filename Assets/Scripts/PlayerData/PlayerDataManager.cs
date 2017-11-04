using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager playerDataManager { get; private set; }//
    //public DroneDatabase modelDatabase;
	public static string userID { get; set; }
	public static string gameID { get; set; }
	public static int spanner { get; set; }
	public static string spanner_time { get; set; }
	public static int money { get; set; }
	public static int level { get; set; }
	public static int exp { get; set; }
	public static List<DroneModel> Models = new List<DroneModel>();
	public static List<DroneModel> ownModels = new List<DroneModel>();
    public static Dictionary<int, int> ownParts = new Dictionary<int, int>();
    public static DroneModel nowUsingModel;

	public Text SpannerView, MoneyView, LevelView, NicknameView;
    string ownStringData;
    bool[] Loadmemory = new bool[6]{ false, false, false, false, false, false };
    private IEnumerator coroutine;
    
	string loaddataURL = "http://13.124.188.186/load_data.php";

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

	void Awake()
    {
        playerDataManager = this;
        //============DB 구현시 해당 코드 삭제 요망.[시작]==========
        if (ownParts.Count == 0)
        {
            ownParts.Add(1, 0);
            ownParts.Add(2, 0);
            ownParts.Add(3, 0);
            ownParts.Add(4, 0);
            ownParts.Add(5, 0);
        }
        //=========DB 구현시 해당 코드 삭제 요망.[끝]============

    }

    void Start()
	{
		FB.Init ();

		userID = PlayerPrefs.GetString ("ID");

		StartCoroutine (Load_Data ());
	
    }

    void Update()
    {
            

    }
    
	IEnumerator Load_Data() {

		// -------닉네임, 돈, 레벨, 경험치 정보 로드-----------------
		// ---------------------------------------------------------
		WWWForm form = new WWWForm();
		form.AddField("userIDPost", userID);

		WWW data = new WWW(loaddataURL, form);
		yield return data;

		string user_Data = data.text;
		print (user_Data);

		gameID = GetDataValue (user_Data, "gameID:");
		money = int.Parse(GetDataValue (user_Data, "Money:"));
		level = int.Parse(GetDataValue (user_Data, "Level:"));
		exp = int.Parse(GetDataValue (user_Data, "Experience:"));
		spanner = int.Parse(GetDataValue (user_Data, "Spanner_Num:"));
		spanner_time = GetDataValue (user_Data, "Spanner_Time:");

		//--------------------------------------------------------

		// -----------------------------------------------------------
		// ------------- 스페너 개수 갱신 -----------------------------

		if (spanner != 10)
			Update_Spanner ();

		// ------------------------------------------------------------

		// -----------현재 사용 드론 로드-------------------------------
		// -------------------------------------------------------------
		int drone_equip = int.Parse(GetDataValue (user_Data, "Drone_Equip:"));

		form.AddField("droneIDPost", drone_equip);

		data = new WWW("http://13.124.188.186/load_drone.php", form);
		yield return data;

        user_Data = data.text;
        
        nowUsingModel = new DroneModel (int.Parse(GetDataValue(user_Data, "DroneID:")), 
			GetDataValue(user_Data, "Name:"), int.Parse(GetDataValue(user_Data, "Price:")));
        
		//-------------------------------------------------------------

		// ------소유 드론목록 로드--------------------------------------
		// --------------------------------------------------------------
		form.AddField("userIDPost", userID);

		data = new WWW("http://13.124.188.186/load_user_drone.php", form);
		yield return data;

		user_Data = data.text;
		user_Data = user_Data.Replace("\n","");
		print (user_Data);
		// 1,2,-1 이런식으로 return값 되있음.

		string[] ids = user_Data.Split(',');
		//Log.text += "보유리스트 길이 : "+ids.Length+"\n";
		for (int i = 0; i < ids.Length-1; i++)
		{
			print ("droneid: " + ids [i]);
			form.AddField("droneIDPost", ids[i]);

			data = new WWW("http://13.124.188.186/load_drone.php", form);
			yield return data;

			user_Data = data.text;
			print(user_Data);
            
			DroneModel model = new DroneModel(int.Parse(GetDataValue(user_Data, "DroneID:")), 
				GetDataValue(user_Data, "Name:"), int.Parse(GetDataValue(user_Data, "Price:")));
			ownModels.Add(model);
		}

		//-----------------------------------------------------------------

		// --------전체 드론목록 로드-------------------------------------
		// ---------------------------------------------------------------

		for (int i = 0; i <= 4; i++) {
			form.AddField("droneIDPost", i);
			data = new WWW ("http://13.124.188.186/load_drone.php", form);
			yield return data;

			user_Data = data.text;

			DroneModel model = new DroneModel (int.Parse(GetDataValue(user_Data, "DroneID:")), 
				GetDataValue(user_Data, "Name:"), int.Parse(GetDataValue(user_Data, "Price:")));

			Models.Add(model);
		}

		// ---------------------------------------------------------------

		print (userID + " " + gameID + " " + money + " " + exp + " " + nowUsingModel.getTitle());

		//SceneManager.LoadScene ("a");

		MoneyView.text = money.ToString();
		LevelView.text = level.ToString();
		NicknameView.text = gameID;
		SpannerView.text = spanner.ToString() + "/10";

	}

	string GetDataValue(string data, string index) {

		string value = data.Substring(data.IndexOf(index)+index.Length);

		//if (index != "Drone_Equip:") 
			value = value.Remove(value.IndexOf("|"));

		return value;
	}

	// -------------------------- 스페너 업데이트 ----------------------------------
	void Update_Spanner () {
		print (spanner_time);

		int date = int.Parse (spanner_time.Substring (9, 1));
		int h = int.Parse (spanner_time.Substring (11, 2));
		int m = int.Parse (spanner_time.Substring (14, 2));
		int s = int.Parse (spanner_time.Substring (17, 2));

		int time = ((date * 24) + h + 9) * 60 * 60 + m * 60 + s; // 시간을 초로 바꿈.

		print (date + " " + h + " " + m + " " + s + " " + time);

		string cur_datetime = DateTime.Now.ToString ("yyyy-MM-dd-HH-mm-ss");
		print (cur_datetime);
		int cur_date = int.Parse (cur_datetime.Substring (9, 1));

		int cur_h = int.Parse (cur_datetime.Substring (11, 2));
		int cur_m = int.Parse (cur_datetime.Substring (14, 2));
		int cur_s = int.Parse (cur_datetime.Substring (17, 2));

		int cur_time = ((cur_date * 24) + cur_h) * 60 * 60 + cur_m * 60 + cur_s; // 현재 시간을 초로 바꿈

		print (cur_date + " " + cur_h + " " + cur_m + " " + cur_s + " " + cur_time);

		print("날짜 같고 초계산 시작");
		// 날짜가 같기 때문에 초로 바꿔 계산
		int addspanner = (cur_time - time) / 60; // 추가 가능한 스페너 수
		print("addspanner: " + addspanner);
		if (spanner + addspanner >= 10) {
			print("full로 채워야함");
			StartCoroutine (Update_Spanner_DB (10));

		} else {
			StartCoroutine (Update_Spanner_DB (spanner + addspanner));
			print("타이머 처음 호출, 남은시간: " + (float)(cur_time - time) % 60);
			StartCoroutine (Spanner_Timer(60f - (float)(cur_time - time) % 60));
		}


	}

	IEnumerator Update_Spanner_DB (int spanner_num) {

		WWWForm form = new WWWForm();
		form.AddField("userIDPost", userID);
		form.AddField ("spannerNumPost", spanner_num);

		WWW data = new WWW("http://13.124.188.186/spanner_updater.php", form);
		yield return data;

		string user_Data = data.text;

		if (user_Data == "\n1") {
			print("에코 1받고 spanner 채움");
			spanner = spanner_num;
			SpannerView.text = spanner.ToString () + "/10";
		} else {
			Debug.Log ("Spanner update failed...");
		}
	}

	IEnumerator Spanner_Timer(float delayTime) {
		print ("딜레이시간초: " + delayTime);

		Debug.Log ("Time: " + Time.time);
		yield return new WaitForSeconds (delayTime);

		StartCoroutine (Update_Spanner_DB (spanner + 1)); // db에 스페너 개수 및 스페너 시간 설정.

		if (spanner + 1 < 10) {
			StartCoroutine (Spanner_Timer(60));
		}

	}

	public void Logout() {
		
		PlayerPrefs.DeleteAll ();
		FB.LogOut ();
		SceneManager.LoadScene ("flogintest");
	}
		

}
