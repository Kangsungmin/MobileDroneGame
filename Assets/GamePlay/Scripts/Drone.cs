using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

public class Drone : MonoBehaviour {

    bool canControl = true;
    bool Drone_starting;
    bool doAutoAttitude = true; //주기적으로 자세복귀를 위해 사용
    bool Drone_ON = false; //드론 작동 가능 여부
    bool changeFuel = true;
    public bool GameOver = false;
    bool bool_time = false;
    bool AutoAim = false;

    int speed = 10;
    int RotSpeed = 120;
    public float My_Hp = 100.0f;
    public float Fuel = 100.0f;
    private Vector3 EnemyLastPos;
    float PropRotSpeed = 0;//초기 프로펠러 회전 속도
    Vector3 wingDir;
    Vector3 bodyDir;
    Vector3 rotVec;

    float currentY, Max_Hp = 100, Max_Fuel = 60;//currentY : Left조이스틱회전값을 저장하는 변수 


    float Thrust = 0.000f, hovering_Thrust = 48.031f;
    public GameObject prop1, prop2, prop3, prop4;//각각 프로펠러 오브젝트
    public GameObject wingFront, wingBack;
    //public GameObject Water;
    public GameObject UIcs;
    public Rigidbody Rb;
    //public GUISkin skin;
    public Renderer rend;
    //public Transform fuel;
    
    public VirtualJS_Left moveJoystickLeft;//조이스틱 객체
    public VirtualJS_Right moveJoystickRight;
    

    public Animation ani;

    //타게팅변수들[시작]
    private Transform target;
    public float range = 100f;
    public string enemyTag = "enemy";
    //타게팅변수들[끝]
    public AudioSource bulletAudio;

    void Start() {

        Drone_starting = false;
        Rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        HingeJoint hinge = GetComponent<HingeJoint>();
        wingDir = new Vector3(270, 180, 0);
        bodyDir = Vector3.zero;
        ani = GetComponent<Animation>();
        //타겟확인 함수 호출
        InvokeRepeating("UpdateTarget", 0f, 0.5f);//0.5초마다 갱신
    }

    bool getbooltime() { return this.bool_time; }
    void setbooltime(bool time) { this.bool_time = time; }

    //=============================Update함수(프레임마다 호출) [시작]=============================
    void Update() {
        bodyDir = Vector3.zero;
       
        float amtMove = speed * Time.smoothDeltaTime;//프레임당 이동 거리
        float amtRot = RotSpeed * Time.smoothDeltaTime;//드론 z축기준 회전 속도
        float amtPropRot = (float)PropRotSpeed * Time.deltaTime;
        float keyForward = Input.GetAxis("Vertical");
        float keySide = Input.GetAxis("Horizontal");
        float keyUp = Input.GetAxis("Up");

        //=============================드론 시동[시작]=============================
        //*초기 비행 시에만 동작한다.
        ////추력을 사용자가 위로 올리기 시작하면 시동이 켜진다.
        //<<05.26수정>>
        if (!Drone_starting)
        {
            if (moveJoystickRight.Vertical() > 0.0f)
            {
                Drone_ON = true; 
                Drone_starting = true;
            }

        }
        //=============================드론 시동[끝]===============================

        //=============================드론 조작[시작]=============================
        //*드론 작동 가능 시 동작한다.
        //*조이스틱 조작에따른 날개와 몸체 회전을 시킨다.

        if (Drone_ON)//드론 작동 가능 시,
        {
            currentY = transform.eulerAngles.y;
            currentY += moveJoystickLeft.Horizontal()*2;//left조이스틱 회전 누적
            bodyDir.z = -50.0f * moveJoystickLeft.Horizontal();//몸체 좌우 회전
            bodyDir.y = currentY;

            //자동 조준 모드가 아닐 시,
            if (!AutoAim) transform.eulerAngles = bodyDir;//Drone 오브젝트 좌우 회전 적용

            //좌측 조이스틱에 따른 날개 회전 애니메이션은 DroneAnim스크립트에서 처리한다.
            if (canControl) StartCoroutine("AddCtrlToDrone", moveJoystickRight.Vertical()); //상하강 버튼을 누를시
            if (changeFuel) StartCoroutine("fuelControl");
            //초당 힘을 가하도록 한다.
            
            SpinProp(amtPropRot);//프로펠러 회전
            //MakeIteam();
        }
        //=============================드론 조작[끝]===============================

        //=============================드론 죽은지 체크[시작]===========================
        /*
        if (Rb.position.y <= Water.gameObject.GetComponent<Water_Time>().getWaterPosition())
        {
            GameOver = true;
            Drone_ON = false;
        }*/
        //=============================드론 죽은지 체크[끝]=============================
        if (target == null)//타겟이 범위에 없을 때
        {
            if (AutoAim)//<오토에임이 'on'이지만 타겟이 제거되거나 없을 때>
            {
                Vector3 IdleLook = EnemyLastPos;
                IdleLook.y = transform.position.y;
                SmoothLookAt(IdleLook);//마지막 타겟좌표의 벡터(x,z)좌표 + 현재 나의 높이 
                AutoAim = false;//오토에임 해제
            }
        }
        else
        {
            if (AutoAim) SmoothLookAt(target.position);//드론은 현재 타게팅 된 적을 바라본다.
        }
    }
    //=============================Update함수(프레임마다 호출) [끝]===============================
    
    //드론 자체적으로 안정성 향상을 위해 주기적으로 자세제어를 호출한다.

    void FixedUpdate()//일정간격으로 호출한다.
    {
        rotVec.x = Mathf.Cos(70 * -bodyDir.z);
        rotVec.y = Mathf.Sin(70 * -bodyDir.z);

        if (Drone_ON)
        {
            Rb.AddForce(Vector3.up * Thrust); // Drone의 위(y축)으로 추력만큼 힘을 가한다.
            Rb.AddRelativeForce(Vector3.forward * 100 * moveJoystickLeft.Vertical());
        }
    }

    //=============================드론 타겟 추척[시작]=============================
    void UpdateTarget()
    {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);//태그가 enemy인 객체들
            float shortDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)//모든 enemies에 대해
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);//enemy와의 거리
                bool enemyRender = enemy.GetComponent<Renderer>().isVisible; //enemy가 카메라 범위 내에 있는가
                if (distanceToEnemy < shortDistance && enemyRender)//가까이 있고 카메라에 잡혀있다면,
                {
                    shortDistance = distanceToEnemy;
                    nearestEnemy = enemy;//가장 가까운 적 갱신
                }
            }
        if (nearestEnemy != null && shortDistance <= range)//가장 가까운 적이 범위안에 있을 때,
        {
            nearestEnemy.GetComponent<EnemyAnim>().Targeted();//타겟에게 타게팅 되었음을 알림. @타겟이 없어졌을때 에러남 
            target = nearestEnemy.transform;
            EnemyLastPos = target.position;//적군의 마지막 최근 위치를 저장한다.
            if (moveJoystickLeft.Horizontal() == 0.0f && moveJoystickLeft.Vertical() == 0.0f) AutoAim = true;
            else AutoAim = false;
        }
        else//범위 내에 타겟이 없을 때,
        {
            target = null;
        }
    }
    //=============================드론 타겟 추척[끝]=============================


    //=============================드론 공격 반경정의[시작]=============================
    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
    //=============================드론 공격 반경정의[끝]=============================

    //=============================드론 추력 조작[시작]=============================
    //*파라미터로는 1 ~ -1이 넘어온다.
    IEnumerator AddCtrlToDrone(float Up) 
    {
        canControl = false;
        if (Drone_ON == false && Up > 0) { Thrust = 40; Drone_ON = true; }//드론 첫 동작시 초기 모터속도 1650
        if (Thrust > 80 && Up > 0) ;
        else if (Drone_ON && Up == 0.0f) Thrust = hovering_Thrust;
        else
        {
            if (Thrust >= 0)
            {
                Thrust += 10 * Up;
                PropRotSpeed = 500;
            }
            else
            {
                if (Up > 0.0f) Thrust += 10 * Up;
            }
        }
        yield return new WaitForSeconds(0.07f);//해당 메소드에 0.07초 지연을 시킨다.
        //이곳의 코드는 지연 후 이루어 진다.
        canControl = true;
    }
    //=============================드론 추력 조작[끝]===============================
    
    
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "charger")
        {
            col.gameObject.GetComponentInParent<Charger>().chargeOn = false;
            getFuel();
        }

    }


    //=============================드론 충돌 판정[시작]=============================
    void OnCollisionEnter(Collision collision)//오브젝트와 충돌시 호출.
    {
        if (collision.gameObject.tag == "launcher") ; 
        else if(collision.gameObject.tag == "bullet")
        {
            Hit(0);
            bulletAudio.Play();
            UIcs.gameObject.GetComponent<UIscripts>().damageAni();
        }
        else
        {
            Hit(1);
            //if (Drone_ON) My_Hp -= 1;
            UIcs.gameObject.GetComponent<UIscripts>().damageAni();
        }

        
        //
        
    }

    //=============================드론 충돌 판정[끝]===============================

    //=============================드론 연료함수[시작]=============================
    IEnumerator fuelControl()//연료 감소 메소드
    {
        changeFuel = false;
        if (Fuel <= 0)
        {
            GameOver = true;//게임 종료
            Drone_ON = false;
        }
        else if (Thrust > 20) Fuel--;       //연료가 남아있을 때 감소시킨다.
        if (Fuel <= 0) GameOver = true;
        

        yield return new WaitForSeconds(1.0f);//해당 메소드에 1초 지연을 시킨다.
        changeFuel = true;
    }
    //=============================드론 연료함수[끝]===============================

    //=============================연료 충전함수[시작]=============================
    private void getFuel()
    {
        if(Fuel<100) Fuel += 20;
    }
    //=============================연료 충전함수[끝]===============================

    //=============================프로펠러 회전[시작]=============================
    void SpinProp(float AmtPropRot)
    {
        prop1.transform.Rotate(Vector3.up * AmtPropRot); // 예로 Vector3.up*Time.deltaTime 은 초당 1도 회전
        prop3.transform.Rotate(Vector3.up * AmtPropRot);
        prop2.transform.Rotate(Vector3.up * AmtPropRot * -1);
        prop4.transform.Rotate(Vector3.up * AmtPropRot * -1);
    }
    //=============================프로펠러 회전[끝]===============================

    //=============================드론 공격받음[시작]=============================
    public void Hit(int damage)
    {
        My_Hp -= damage;
        if (My_Hp <= 0)
        {
            GameOver = true;
            Drone_ON = false;

        }
    }
    //=============================드론 공격받음[끝]===============================

     

    public void SmoothLookAt(Vector3 T)
    {
        transform.rotation = Quaternion.RotateTowards( transform.rotation, 
            Quaternion.LookRotation(-transform.position + T),
              Time.deltaTime * 50);
    }
    
}
