using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    SceneFader Fader;
    GameObject ViewCamera, LeftButton, RightButton, BuyPanel;
    public GameObject ChangeButton;
    GameObject[] AllModels;
    Text MoneyView, ModelPriceView;
    int nowPoint;
    bool isMoving;
	// Use this for initialization
	void Start () {
        Fader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
        ViewCamera = GameObject.Find("Main Camera");
        AllModels = GameObject.FindGameObjectsWithTag("Player"); //모든 드론 모델들을 가져온다.
        LeftButton = GameObject.Find("LeftBtn");
        RightButton = GameObject.Find("RightBtn");
        BuyPanel = GameObject.Find("BuyPanel");
        nowPoint = 0;
        MoneyView = GameObject.Find("MoneyAmount").GetComponent<Text>();
        ModelPriceView = GameObject.Find("ModelPrice").GetComponent<Text>();
        
        LeftButton.SetActive(false);
        isMoving = false;
    }
    void Update()
    {
        //GUI 업데이트
        MoneyView.text = PlayerDataManager.money.ToString();
        DroneModel thismodel = DroneDatabase.FetchDroneByID(nowPoint);
        updateModelInfo(thismodel);
        if (CheckModelIs(thismodel))
        {
            BuyPanel.SetActive(false);
            if (PlayerDataManager.nowUsingModel.ID == thismodel.ID) ChangeButton.SetActive(false); //현재 사용중
            else ChangeButton.SetActive(true); //보유하고 있지만 현재 사용중 아님.

        }
        else
        {
            BuyPanel.SetActive(true);
            ChangeButton.SetActive(false);
        }
            
    }
	
    public void BuyModel(int id)
    {
        //PlayerData에 있는지 확인, 없으면 구매: 플레이어프리팹 추가 후 새로고침
        DroneModel model = DroneDatabase.FetchDroneByID(id);
        if (CheckModelIs(model))
        {
            //모델을 이미 보유하고 있음
        }
        else//아이템 구매 
        {
            if (model.Price <= PlayerDataManager.money)//해당 모델 가격보다 돈이 많을 때 Player Prefebs 에 보유 모델 추가
            {
                PlayerDataManager.playerDataManager.DecreaseMoney(model.Price);
                //구매: 플레이어프리팹 추가 후 새로고침
                PlayerPrefs.SetString("Models", PlayerPrefs.GetString("Models") + "," + id);
            } else ;//돈이 부족합니다.
        }
        //새로고침
        PlayerDataRefresh();
        //PlayerDataManager.playerDataManager.DBRefresh();
    }
    void updateModelInfo(DroneModel model)
    {
        //가격, 정보 업데이트
        ModelPriceView.text = model.Price.ToString();
    }

    bool CheckModelIs(DroneModel model)
    {
        //PlayerData에 있는지 확인
        for (int i = 0; i < PlayerDataManager.ownModels.Count; i++)
            if (PlayerDataManager.ownModels[i].ID == model.ID) return true;
        return false;
    }

    public void ThisModelBuy()//현재 모델 구매
    {
        BuyModel(nowPoint);
    }
    
    public void PlayerDataRefresh()
    {
        PlayerDataManager.money = PlayerPrefs.GetInt("money");
        PlayerDataManager.ownModels = PlayerDataManager.playerDataManager.ParseModels(PlayerPrefs.GetString("Models"));
        PlayerDataManager.nowUsingModel = DroneDatabase.FetchDroneByID(PlayerPrefs.GetInt("nowModel"));
    }

    public void LeftShift()//좌로 이동
    {
        if (!isMoving)
        {
            isMoving = true;
            nowPoint--;
            RightButton.SetActive(true);
            if (nowPoint == 0)
                LeftButton.SetActive(false);
            ViewCamera.SendMessage("ShiftLeft");

            StartCoroutine("MovigLock");
        }

    }

    public void RightShift()//우로 이동
    {
        if (!isMoving)
        {
            isMoving = true;
            nowPoint++;
            LeftButton.SetActive(true);
            if (nowPoint == AllModels.Length-1)
                RightButton.SetActive(false);
            ViewCamera.SendMessage("ShiftRight");
            StartCoroutine("MovigLock");
        }

    }

    public void ChangeModel()
    {
        PlayerDataManager.nowUsingModel = DroneDatabase.FetchDroneByID(nowPoint);//현재 모델로 교체
        PlayerPrefs.SetInt("nowModel", nowPoint);
        print("현재 모델 "+PlayerDataManager.nowUsingModel.Title+"로 교체");
    }

    public void GoBackMenu()
    {
        Fader.FadeTo("Menu");
    }


    
    IEnumerator MovigLock()
    {
        yield return new WaitForSeconds(1.05f);
        isMoving = false;
    }
}
