using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fild_Item : MonoBehaviour {

    GameObject Target;
    public GameObject child_Item;
    // Update is called once per frame
    private void Awake()
    {
        child_Item = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (Target != null)
        {
            child_Item.SendMessage("MoveToTarget", Target);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        print("트리거");
        if (target.tag.Contains("Player"))
        {
            print("타겟팅");
            Target = target.gameObject;
        }
    }

}
