using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public AudioSource As;
    public GameObject FireEffect;
    public Aim aim;
    public FireButton fireButton;
    public int power = 200;
    public Transform bullet;
    public Rigidbody BulletRb;//총알 강체
    public Rigidbody SpRb;//총기 강체
    private int vibration = 5;
    bool On = true;
	// Use this for initialization
	void Start () {
        FireEffect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //발사 버튼을 누를 시,
        if (On)//쏠 수 있을때
        {
            if (fireButton.isFire)//마우스 좌클릭Input.GetMouseButton(0)
            {
                FireEffect.SetActive(true);//발사이펙트 보임
                As.Play();//오디오 플레이
                On = false;
                //=============================총기 반동[시작]=============================
                float ran = Random.Range(-1.0f, 1.0f);
                Vector3 vibVector = Vector3.back + new Vector3(ran, ran, ran);//반동이 일어날 랜덤의 방향 벡터를 생성
                
                GameObject spPoint = GameObject.Find("BulletSpawnPoint");
                spPoint.transform.Rotate(vibVector);
                //=============================총기 반동[끝]=============================

                //=============================총알 발사[시작]=============================
                Transform Bullet = (Transform)Instantiate(bullet, spPoint.transform.position, spPoint.transform.rotation);
                BulletRb = Bullet.GetComponent<Rigidbody>();
                BulletRb.AddForce(spPoint.transform.forward * power);
                //=============================총알 발사[끝]=============================

                spPoint.transform.Rotate(-vibVector);
                //StartCoroutine(aim.Aimed());
                Invoke("FireSpeed",0.2f);//연사 속도 조절최소 0.4초 간격
            }
        }
        else
        {
            FireEffect.SetActive(false);
        }
		
	}

    void FireSpeed()
    {
        On = true;
    }
}
