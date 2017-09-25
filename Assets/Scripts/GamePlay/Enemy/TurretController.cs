using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour {
    private int health = 100, maxHealth = 100;

    private Image healthBar;
    public GameObject Camera;
    public int Health { get { return health; } }
    // Use this for initialization
    void Start()
    {
        healthBar = transform.FindChild("EnemyCanvas").FindChild("HealthBG").FindChild("Health").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //드론 자동조준 해제시켜준다.

            gameObject.SetActive(false);//비활성화 시킨다

            //Camera.gameObject.GetComponent<Playenv>().killCount++;

            //gameObject.tag = "enemy_dead";
            //gameObject.GetComponent<EnemyAnim>().isDead = true;//EnmeyAnim의 state를 -1로 전환한다.
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        healthBar.fillAmount = (float)health / (float)maxHealth;
    }
}
