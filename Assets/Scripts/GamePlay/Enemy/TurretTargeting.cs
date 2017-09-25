using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour {
    object lockObject = new object();
    public float range = 120.0f;
    public string player_tag = "Drone";
    private Transform target;

    public Transform bullet, spPoint_L, spPoint_R;
    public Rigidbody BulletRb;
    bool Delay, pFind;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);//0.5초마다 갱신
        Delay = false;//장전 시간
        pFind = false;//범위내 적 확인
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) SmoothLookAt(target);
    }

    void FixedUpdate()
    {
        if (Delay && pFind)//장전이 되었을때,범위안에 적이 있을때
        {
            Delay = false;

            Transform Bullet_1 = (Transform)Instantiate(bullet, spPoint_L.transform.position, spPoint_L.transform.rotation);
            Transform Bullet_2 = (Transform)Instantiate(bullet, spPoint_R.transform.position, spPoint_R.transform.rotation);
            Bullet_1.GetComponent<TurretMissle>().Target = target.gameObject;//총알에 타겟을 넘김
            Bullet_2.GetComponent<TurretMissle>().Target = target.gameObject;//총알에 타겟을 넘김

            //StartCoroutine(aim.Aimed());
            Invoke("FireSpeed", 2.0f);//연사 속도 조절최소 1.0초 간격
        }
        print("범위내 :" + pFind + "장전속도 :" + Delay);
    }
    void FireSpeed()
    {
        Delay = true;
    }

    void UpdateTarget()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag(player_tag);//태그가 enemy인 객체들
        float shortDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach (GameObject player in Players)//모든 player에 대해
        {
            float distanceToEnemy = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestPlayer = player;//가장 가까운 적 갱신
            }
        }

        if (shortDistance <= range)//가장 가까운 적이 범위안에 있을 때,
        {
            //여기까지 gameObject 자신
            if (!pFind)//지금 적을 발견한 것이면, canFire == false
            {
                Delay = true;
            }
            pFind = true;
            target = nearestPlayer.transform;

        }
        else
        {
            pFind = false;
            target = null;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void SmoothLookAt(Transform T)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(-transform.position + T.position),
              Time.deltaTime * 50);
    }
}
