using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
/*
 * 시작 메뉴에서 호출
 * PlayerDataManager에서 생성한다.
 */
public class DroneDatabase : MonoBehaviour {
    JsonManager JManager;
    private List<DroneModel> database = new List<DroneModel>();
    private JsonData droneData;
    string path = string.Empty;

    void Awake () {
        /* Json읽어오는 경로
         * PC, Android, IOS가 각각 다르다
        * PC : path = Application.dataPath + "/StreamingAssets";
        * Android : path = "jar:file://" + Application.dataPath + "!/assets/";
        * IOS : path = Application.dataPath + "/Raw";
        */
        path = Application.streamingAssetsPath + "/Drones.json";
        if (Application.platform == RuntimePlatform.Android)//안드로이드의 경우 File.IO로 읽지 못한다.
        {
            StartCoroutine("Load");
        }
        else//Window Mac IOS 환경일 시
        {
            droneData = JsonMapper.ToObject(File.ReadAllText(path));//읽어올 경로 Application.dataPath + "/Streaming/Items.json"
            ConstructDroneDatabase();
        }
        //DB로드가 완료됨
        //transform.GetComponent<PlayerDataManager>().SendMessage("ModelListLoad");
    }
    public DroneModel FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id) return database[i];//데이터베이스 리스트에 존재하면 가져온다.
        }
        return null;
    }

    void ConstructDroneDatabase()
    {
        for (int i=0; i< droneData.Count; i++)
        {
            database.Add(new DroneModel((int)droneData[i]["ID"], droneData[i]["Title"].ToString(), (int)droneData[i]["Price"]));
        }
    }

    IEnumerator Load()
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone)
        {
            droneData = JsonMapper.ToObject(www.text);
            ConstructDroneDatabase();
        }
        else;
    }
}

public class DroneModel
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public DroneModel(int id, string title, int price)
    {
        this.ID = id;
        this.Title = title;
        this.Price = price;
    }
    public DroneModel()
    {
        this.ID = -1;
    }
}