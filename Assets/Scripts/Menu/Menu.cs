using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public GameObject LevelMenu;
    public SceneFader fader;
    //public Text Path;
    //public AudioClip backMusic;
    void Start()
    {
        //AudioSource.PlayClipAtPoint(backMusic,transform.position);
        //화면 사이즈 적용
        Screen.SetResolution(1280, 800, true);
        Time.timeScale = 1;
        //Path.text = Application.dataPath;
       // Debug.Log("플레이어 레벨 : " + PlayerDataManager.level);
       // Debug.Log("경험치 : " + PlayerDataManager.exp);
       // Debug.Log("돈 : "+ PlayerDataManager.money);
    }
    public void SingleplayBtn()
    {
        //Invoke("startGame", .1f);
        LevelMenu.SetActive(true);
    }
    public void SingleplayExit()
    {
        LevelMenu.SetActive(false);
    }
    public void GoShop()
    {
        fader.FadeTo("Shop");
    }

    public void Exit()
    {
        Invoke("doExit", .1f);
    }

    void doExit()
    {
        print("종료 버튼");
        Application.Quit();
    }
}
