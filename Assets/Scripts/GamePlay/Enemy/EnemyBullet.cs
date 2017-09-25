using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//적 총알을 담당하는 추상클래스
public class EnemyBullet : MonoBehaviour {
    public string type;
    public int speed;
    public int power;


    void Start()
    {
        StartCoroutine("remove");
    }

    public void SetBullet(string Type, int Speed, int Power)//총알 세팅
    {
        type = Type;
        speed = Speed;
        power = Power;
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);//직선으로 운동
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);//총알 생성 5초 후에 제거
    }
}
