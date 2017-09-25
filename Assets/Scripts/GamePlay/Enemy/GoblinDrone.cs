using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinDrone : Enemy {
    public Transform bullet;
    Transform bulletSpwan;
    bool AttackTrigger;
    int ReloadCount;
    // Use this for initialization
    void Awake() {
        Name = "고블린드론";
        PlayEnvironment = GameObject.Find("PlayEnvironment");
        bulletSpwan = transform.Find("BulletSpawnPoint");
        Speed = 4.0f;
        HP = 50;
        MaxHP = 50;
        Durable = 1;
        Gold = 12;
        Exp = 10;
        Range = 40.0f;
        State = "idle";//Enemy상태
        AttackTrigger = true;
        ReloadCount = 0;
        //드롭 가능한 아이템 리스트 프리펩리스트
        //DropItemList.Add();

        EnemyDataUI = GameObject.Find("EnemyData");

        //healthBar = GameObject.Find("EnemyHPbar").GetComponent<Image>();
        //고블린드론 스텟세팅
        Player = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject p in Player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < Range)
            {
                /* <공격 대상 발견>
                 * 1.대상을 바라본다.
                 * 2.대상을 향해서 움직인다.(speed만큼)
                 * 3.대상과의 거리가 Range/2때까지 반복.
                 */
                transform.LookAt(p.transform);
                if (distanceToPlayer < Range * 0.5)//공격범위 내일 시,
                {   //공격
                    State = "attack";
                }
                else//가까이 간다.
                {
                    State = "move";
                }
                
            }
        }

        switch (State)
        {
            case "idle":

                break;
            case "move":
                transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);//보는방향으로 움직인다.
                break;
            case "attack":
                Attack();
                break;
            case "die"://죽음
                
                break;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        print("트리거");
        switch (col.tag)
        {
            case "PlayerBullet"://플레이어 공격에 맞음
                print("맞음");
                TakeDamage(col.transform.GetComponent<Bullet>().power);
                Destroy(col.gameObject);
                break;
        }
    }

    public override void TakeDamage(float amount)
    {
        HP -= amount;
        EnemyDataUI.SetActive(true);
        EnemyDataUI.transform.GetChild(1).GetComponent<Text>().text = Name;
        healthBar.fillAmount = (float)HP / (float)MaxHP; //UI의 Enemy HP 업데이트
        if (HP <= 0)
        {
            State = "die";
            GameObject.Find("MarkerGenerator").SendMessage("EnemyDead", gameObject);
            EnemyDataUI.SetActive(false);
            gameObject.SetActive(false);

        }
    }

    public override void DropItems()
    {
        //드랍가능한 아이템중 랜덤하게 드랍을 한다.

    }

    public void Attack()
    {
        if(AttackTrigger == true)
        {
            AttackTrigger = false;
            Transform spawnBullet = (Transform)Instantiate(bullet, bulletSpwan.transform.position, bulletSpwan.transform.rotation);
            spawnBullet.transform.GetComponent<EnemyBullet>().SetBullet("RedLaser",20, 6);
            //총알은 내장된 코드로 직선운동한다.
            ReloadCount++;
            if (ReloadCount == 2)
            {
                Invoke("FireSpeed", 1.0f);
                ReloadCount = 0;
            }
            else
            {
                Invoke("FireSpeed", 0.3f);//연사 속도 조절최소 0.4초 간격
            }
            
        }
    }
    void FireSpeed()
    {
        AttackTrigger = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
