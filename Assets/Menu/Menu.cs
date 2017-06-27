using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public AudioClip backMusic;
    void Start()
    {
        AudioSource.PlayClipAtPoint(backMusic,transform.position);
    }
    public void GameplayBtn()
    {
        Invoke("startGame", .1f);
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
    
    void startGame()
    {
        Application.LoadLevel("map");
    }

    void doExit()
    {
        print("종료 버튼");
        Application.Quit();
    }
}
