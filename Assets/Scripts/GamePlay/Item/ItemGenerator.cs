using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
    public GameObject genitem;
    bool genScheduled = false;
    void Start()
    {
        Transform temp = (Transform)Instantiate(genitem.transform, transform.position, transform.rotation);
        temp.SetParent(transform);//이곳을 부모로 지정
        temp.name = genitem.name;
    }
	
	// Update is called once per frame
	void Update () {
        if ((transform.childCount == 0) && (genScheduled == false))
        {//아이템이 없다면
            StartCoroutine("ItemGen", genitem);
            genScheduled = true;
        }
	}

    IEnumerator ItemGen(GameObject it)
    {
        yield return new WaitForSeconds(15.0f);//15초 후에 피자 생성
        Transform temp = (Transform)Instantiate(genitem.transform, transform.position, transform.rotation);
        temp.SetParent(transform);//이곳을 부모로 지정
        temp.name = genitem.name;
        genScheduled = false;
    }
}
