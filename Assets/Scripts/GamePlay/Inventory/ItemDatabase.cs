using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

/*
 * Item.json파일로 부터 데이터를 읽어들여서 Item객체화 한다.
 */
public class ItemDatabase : MonoBehaviour {
    JsonManager JManager;
    private List<Item> database = new List<Item>();
    private JsonData itemData;
    public Text pathTest;
    string path = string.Empty;


    void Start()
    {


        /* Json읽어오는 경로
         * PC, Android, IOS가 각각 다르다
         * PC : path = Application.dataPath + "/StreamingAssets";
         * Android : path = "jar:file://" + Application.dataPath + "!/assets/";
         * IOS : path = Application.dataPath + "/Raw";
         */
        path = Application.streamingAssetsPath + "/Items.json";
        if (Application.platform == RuntimePlatform.Android)//안드로이드의 경우 File.IO로 읽지 못한다.
        {
            pathTest.text += "\n안드로이드입니다.";
            StartCoroutine("Load");            
        }
        else//Window Mac IOS 환경일 시
        {
            pathTest.text += "\n윈도우입니다.";
            itemData = JsonMapper.ToObject(File.ReadAllText(path));//읽어올 경로 Application.dataPath + "/Streaming/Items.json"
            ConstructItemDatabase();
        }
        
        
    }

    public Item FetchItemByID(int id)
    {
        for (int i=0; i< database.Count; i++)
        {
            if(database[i].ID == id) return database[i];//데이터베이스 리스트에 존재하면 가져온다.
        }
        return null;
    }
    
    void ConstructItemDatabase()
    {
        pathTest.text += "Json변환\n";
        for (int i=0; i< itemData.Count; i++)//json파일의 id수 만큼
        {
            database.Add(new Item((int)itemData[i]["ID"], itemData[i]["Title"].ToString(), (int)itemData[i]["Value"], (string)itemData[i]["Slug"] ));//읽어온 데이터삽입: i번째의 id, title, value 를 Item객체화, DB에 삽입
        }//사용하지 않더라도 모든 json의 item들을 객체화 하여 database리스트에 넣는다.
    }

    IEnumerator Load()
    {
        pathTest.text += "경로 가져오기시작\n";
        WWW www = new WWW(path);
        yield return www;
        pathTest.text += "경로 가져옴\n";
        if (www.isDone)
        {
            itemData = JsonMapper.ToObject(www.text);
            pathTest.text += "jsonData변환\n";
            ConstructItemDatabase();
        }else pathTest.text += "실패\n";

    }

}
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite {get; set;}

    public Item(int id, string title, int value, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Sprite = Resources.Load<Sprite>("sprite/Items/" + slug);
    }


    public Item()
    {
        this.ID = -1;
    }

}
