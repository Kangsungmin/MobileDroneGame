using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFuel_small : MonoBehaviour {
    public AudioClip sndCharge;
    int rotY;
    
    // Use this for initialization
    void Start () {
        rotY = 50;
        InitFuel();
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0, rotY, 0)*Time.smoothDeltaTime);// 아이템 회전
    }

    //연료 세팅
    void InitFuel()
    {
        float x = Random.Range(200, 1800);
        float y = Random.Range(105, 105);
        float z = Random.Range(200, 1800);
        transform.position = new Vector3(x, y, z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Drone")
        {

            AudioSource.PlayClipAtPoint(sndCharge, transform.position);
            other.SendMessage("getFuel", transform.position, SendMessageOptions.DontRequireReceiver);//드론에게 연료팩을 먹었다고 메세지 보냄. getFuel메소드 호출한다.
            
            Destroy(gameObject);//연료팩 제거
        }
        
    }
}
