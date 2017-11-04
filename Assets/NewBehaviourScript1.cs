using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("ID")) {
			print ("user is logging");
			SceneManager.LoadScene ("Menu");
		} else {
			SceneManager.LoadScene ("flogintest");
		}

	}
		
}
