using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

/*
 * Item.json파일로 부터 데이터를 읽어들여서 Item객체화 한다.
 */
public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Streaming/Items.json"));//읽어올 경로
        ConstructItemDatabase();
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
        for(int i=0; i< itemData.Count; i++)//json파일의 id수 만큼
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"], (string)itemData[i]["slug"] ));//읽어온 데이터삽입: i번째의 id, title, value 를 Item객체화, DB에 삽입
        }//사용하지 않더라도 모든 json의 item들을 객체화 하여 database리스트에 넣는다.
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
