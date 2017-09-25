using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string type;
    public int speed;
    public int power;
    public GameObject Target;
    
    void Start()
    {
        StartCoroutine("remove");
    }
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);//직선으로 운동
    }
    public void SetBullet(string Type, int Speed, int Power)//총알 세팅
    {
        type = Type;
        speed = Speed;
        power = Power;
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);//총알 생성 5초 후에 제거
    }
}
