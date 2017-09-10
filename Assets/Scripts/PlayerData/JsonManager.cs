using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class StreamalbeItem
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public string Slug { get; set; }

    public StreamalbeItem(int id, string title, int value, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Slug = slug;
    }

    public StreamalbeItem()
    {
        this.ID = -1;
    }

}

public class JsonManager : MonoBehaviour {
    string path = string.Empty;
    public List<StreamalbeItem> AllItems = new List<StreamalbeItem>();

    // Use this for initialization
    void Start()
    {

        path = Application.streamingAssetsPath + "/Items.json";

        Debug.Log("제이슨매니저 실행");
        AllItems.Add(new StreamalbeItem(0, "pizza", 1, "Item_pizza"));
        AllItems.Add(new StreamalbeItem(1, "speedItem", 3, "Item_boost"));
        AllItems.Add(new StreamalbeItem(2, "powerItem", 3, "Item_power"));
        //Save();
    }
    public void Save()
    {
        Debug.Log("제이슨 저장 , " + AllItems[0].Title);
        JsonData itemjson = JsonMapper.ToJson(AllItems);
        File.WriteAllText(path, itemjson.ToString());//"jar:file://" + Application.persistentDataPath + "!/assets" !!!이 라인 지워보기
    }

    public void Load()
    {
        Debug.Log("제이슨 불러오기");


    }

	
	// Update is called once per frame
	void Update () {

    }
}
