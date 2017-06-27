using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform explosion, embers;
    public GameObject Target;
    public Rigidbody BulletRb;
    public int power = 300;
    void Start()
    {
        StartCoroutine("remove");
        BulletRb = GetComponent<Rigidbody>();
    }
    // Use this for initialization
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
            col.gameObject.GetComponent<Drone>().Hit(10);
            Destroy(gameObject);
        }
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
