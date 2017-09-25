using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public static string grabState;//Idle, Try, Using
    public Animator GrabModeCtrl;//UI애니메이션
    // Use this for initialization
    void Start () {
        grabState = "Idle";
        GrabModeCtrl = GameObject.Find("UI").transform.Find("FireButtonActive").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabState.Equals("Using")) GrabModeCtrl.SetBool("ATK", true);
        else GrabModeCtrl.SetBool("ATK", false);
    }

    public void GrabMode()
    {
        switch (grabState)
        {
            case "Idle":
                transform.root.SendMessage("GrabSomthing");
                break;
            case "Using":
                transform.root.SendMessage("DropSomthing");
                break;
        }
    }
}
