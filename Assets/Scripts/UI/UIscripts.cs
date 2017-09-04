using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscripts : MonoBehaviour {
    public GameObject Drone,game_over_text, hpText, fuelText, damagedImg, GameOverMenu;
    public Image fireBtn, joystickLeft, joystickRight ;
    private float health = 100.0f, maxHealth = 100.0f;
    private float fuel = 100.0f, maxFuel = 100.0f;
    Image healthBar, fuelBar;
    // Use this for initialization
    void Start ()
    {
        healthBar = transform.FindChild("HP").FindChild("HP_bar").GetComponent<Image>();
        fuelBar = transform.FindChild("FUEL").FindChild("Fuel_bar").GetComponent<Image>();
        damagedImg.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
        if(Drone.gameObject.GetComponent<Drone>().GameOver) game_over_text.SetActive(true);
        hpText.GetComponent<Text>().text = health + "";
        fuelText.GetComponent<Text>().text = fuel + "";

        health = Drone.gameObject.GetComponent<Drone>().My_Hp;
        fuel = Drone.gameObject.GetComponent<Drone>().Fuel;

        healthBar.fillAmount = (float)health / (float)maxHealth;
        fuelBar.fillAmount = (float)fuel / (float)maxFuel;
        if (health <= 0.0f || fuel <= 0.0f)//게임오버시
        {
            Drone.gameObject.GetComponent<Drone>().GameOver = true;
            fireBtn.GetComponent<Image>().enabled = false;
            joystickLeft.GetComponent<Image>().enabled = false;
            joystickRight.GetComponent<Image>().enabled = false;
            GameOverMenu.SetActive(true);
        }
        /*
        if (Drone.gameObject.GetComponent<Drone>().Damaged)
        {
            damagedImg.SetActive(true);
        }
        */
    }
    public void damageAni()
    {
        damagedImg.SetActive(true);
        Invoke("DamageEnd",1.0f);
    }

    void DamageEnd()
    {
        damagedImg.SetActive(false);
    }

}
