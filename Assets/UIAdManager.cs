using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UIAdManager : MonoBehaviour {

    public Button _BtnUnityAds;
    
    ShowOptions _ShowOpt = new ShowOptions();

    void Awake()
    {
        Advertisement.Initialize("1560964", true);
        _ShowOpt.resultCallback = OnAdsShowResultCallBack;
        UpdateButton();
    }

    void OnAdsShowResultCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerDataManager.spanner++;//스패너 감소
            PlayerPrefs.SetInt("spanner", PlayerDataManager.spanner);
        }
    }

    void UpdateButton()
    {
        _BtnUnityAds.interactable = Advertisement.IsReady();
        _BtnUnityAds.GetComponentInChildren<Text>().text
            = "광고보고 스패너 충전하기";
    }

    public void OnBtnUnityAds()
    {
        print("광고클릭");
        Advertisement.Show(null, _ShowOpt);
    }

    void Update() { UpdateButton(); }
}
