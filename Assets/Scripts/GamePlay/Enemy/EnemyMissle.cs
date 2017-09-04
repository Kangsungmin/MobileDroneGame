using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    public Transform explosion, embers;
    public GameObject Target;
    public Quaternion lookTarget;
    public Rigidbody BulletRb;
    public float power, turn;
    void Start()
    {
        power = 50;
        turn = 20;
        StartCoroutine("remove");
        BulletRb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void FixedUpdate()
    {
        if (Target != null)
        {
            lookTarget = Quaternion.LookRotation(Target.transform.position-transform.position);
            BulletRb.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookTarget, turn));
        }
        //BulletRb.AddForce(transform.forward * power);
        BulletRb.velocity = transform.forward * power;
    }

    void OnCollisionEnter(Collision col)//출돌 감지
    {
        if (col.gameObject.tag == "bomb")
        {
            Instantiate(explosion, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<Enemy>().Hit(15);//Enemy의 Hit함수 호출
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Drone")
        {
            Instantiate(embers, col.transform.position, Quaternion.identity);
            col.gameObject.GetComponent<Drone>().Hit(6);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "bullet") ;//같은 총알이면 무시
        else// 그 외 오브젝트 충돌 삭제
        {
            Destroy(gameObject);
        }
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);//총알 생성 5초 후에 제거
    }
}
