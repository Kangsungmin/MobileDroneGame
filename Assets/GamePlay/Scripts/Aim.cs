using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    public Transform aim;
    public RectTransform aim2;
    public GameObject aim22;
    RaycastHit Hit;

    public IEnumerator Aimed()
    {
        
        Physics.Raycast(aim.position, aim.forward, out Hit, Mathf.Infinity);//부딛히는 여부에 대해 Hit에 저장
        if (Hit.collider)
        {
            
            print(Hit.collider.tag);
            if(Hit.collider.tag == "enemy")
            {
                print("hit the enemy \n");
                aim22.active = false;
                //Destroy(Hit.collider.gameObject);
            }
        }
        
        yield return null;
    }

    private void Aim_Changed_Red()
    {
        aim22.active = false;
        print("aim changed red \n");
    }
    private void Aim_Changed_Green()
    {
        aim22.active = true;
        print("aim changed green \n");
    }
}
