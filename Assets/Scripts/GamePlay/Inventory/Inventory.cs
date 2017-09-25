using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
	// Use this for initialization
	void Start () {
        database = GetComponent<ItemDatabase>();//인벤토리 객체의 스크립트

        slotAmount = 6;//총 슬롯 수
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;
        for (int i = 0; i<slotAmount; i++)
        { 
            items.Add(new Item());//비어있는 아이템 추가
            slots.Add(Instantiate(inventorySlot));//슬롯객체
            slots[i].transform.SetParent(slotPanel.transform);//슬롯의 부모를 패널로 설정
        }
        inventoryPanel.SetActive(false);//인벤토리 환경세팅 완료후엔 비활성화 시킨다. 
        //AddItem(1);//1번째 아이템을 넣는다.
        //AddItem(0);
    }
	
	public void AddItem(int id)
    {
        /*
         * DB가 아닌 해당씬에서만 사용하려면,
         * database 객체가 아닌 gameplay씬 내에서 사용하는 객체를 따로 만든다.
         */

        Item itemToAdd = database.FetchItemByID(id);//DB리스트에 해당 id가 있으면 가져온다.
        if (CheckItemIs(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            /*
         * 처음 슬롯부터 탐색하여 비어있는 슬롯이 있다면
         * 그곳에 아이템을 넣는다.
         */

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)//빈 아이템
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.transform.SetParent(slots[i].transform);//아이템의 부모를 슬롯으로 설정
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//아이템 이미지 설정
                    itemObj.transform.localPosition = Vector2.zero;
                    itemObj.name = itemToAdd.Title;
                    int Amount = ++itemObj.GetComponent<ItemData>().amount;
                    itemObj.transform.GetChild(0).GetComponent<Text>().text = Amount.ToString();

                    break;
                }
            }
        }
        
        
        
        

    }

    public bool isItem(int id)//해당 아이템이 현재 있는지 확인
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id) return true;
        }
        return false;
    }

    public void RemoveItem(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)//빈 아이템
            {
                ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                if(data.amount == 1)
                {
                    items[i] = new Item();
                    Destroy(slots[i].transform.Find("pizza").gameObject);
                }
                else
                {
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                }
                break;
            }
        }
    }

    bool CheckItemIs(Item item)
    {
        for (int i=0; i < items.Count; i++)
            if (items[i].ID == item.ID) return true;
        return false; 
    }
}
