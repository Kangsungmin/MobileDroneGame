using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public GameObject LevelMenu;
    //public AudioClip backMusic;
    void Start()
    {
        //AudioSource.PlayClipAtPoint(backMusic,transform.position);
        Time.timeScale = 1;
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
    public void GoLab()
    {
        Invoke("startLab", .1f);
    }
    public void Exit()
    {
        Invoke("doExit", .1f);
    }

    void startLab()
    {
        Application.LoadLevel("LabScene");
    }

    void doExit()
    {
        print("종료 버튼");
        Application.Quit();
    }
}
