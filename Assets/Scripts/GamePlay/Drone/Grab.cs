using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    float Range = 3.0f;
    GameObject GrabButton;
    
    GameObject target;
    GameObject[] Boxes;
    // Use this for initialization
    void Start () {
        GrabButton = GameObject.Find("UI").transform.Find("GrabButton").gameObject;
        Boxes = GameObject.FindGameObjectsWithTag("Box_R"+ int.Parse(SceneData.SceneLevelName));
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isBox = false;
        if (Boxes != null)
        {
            foreach (GameObject B in Boxes)
            {
                float distanceToBox = Vector3.Distance(transform.position, B.transform.position);
                if (distanceToBox < Range)
                {
                    isBox = true;
                    target = B;
                    GrabButton.SetActive(true);//상자 들기 버튼 활성화
                }
            }
            if (!isBox) { target = null; GrabButton.SetActive(false); } //버튼 비활성화
        }
        else print("박스없음");
        
    }

    public void GrabMode()
    {
        transform.root.SendMessage("GrabSomthing",target);
    }

}
