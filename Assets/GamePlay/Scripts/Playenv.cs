using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playenv : MonoBehaviour
{
    public int killCount = 0;
    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1280, 800, true);
    }


    public void GoMenu()
    {
        print("나가기");
        Invoke("startMenu", .1f);
    }

    void startMenu()
    {
        print("메뉴 씬 호출");
        Application.LoadLevel("Menu");
    }

}