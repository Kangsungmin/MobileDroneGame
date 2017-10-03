using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationUI : MonoBehaviour {
    public GameObject AddSpannerPanel;
	// Use this for initialization
	void Awake () {
        AddSpannerPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAddSpanner()
    {
        AddSpannerPanel.SetActive(true);
    }
    public void OffAddSpanner()
    {
        AddSpannerPanel.SetActive(false);
    }
}
