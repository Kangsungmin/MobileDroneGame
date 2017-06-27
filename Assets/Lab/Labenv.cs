using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labenv : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(1280, 800, true);
    }
	
    public void GoMenu()
    {
        print("메뉴 버튼 클릭");
        Invoke("startMenu", .1f);
    }

    void startMenu()
    {
        print("메뉴 씬 호출");
        Application.LoadLevel("Menu");
    }
}
