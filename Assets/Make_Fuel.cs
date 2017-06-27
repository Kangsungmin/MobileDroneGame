using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_Fuel : MonoBehaviour {

    public Transform fuel;
    public Transform item;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MakeIteam();
        Makeitem();
    }

    void MakeIteam()
    {
        if (UnityEngine.Random.Range(0, 1000) > 995)
        {
            Instantiate(fuel);
        }
    }
    void Makeitem()
    {
        if (UnityEngine.Random.Range(0, 1000) > 995)
        {
            Instantiate(item);
        }
    }
}
