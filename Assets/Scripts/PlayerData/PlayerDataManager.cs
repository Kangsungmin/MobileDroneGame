using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour {
    public static PlayerDataManager playerDataManager { get; private set; }//

    public static int money { get; set; }
    public static int level { get; private set; }
    public static int exp { get; private set; }

    public Text MoneyView, LevelView;

    void Awake()
    {
        playerDataManager = this;
        //PlayerPrefs.SetInt("money", 50);
        //PlayerPrefs.SetInt("level", 1);
        //PlayerPrefs.SetInt("exp", 0);
        
        if (!PlayerPrefs.HasKey("money") && !PlayerPrefs.HasKey("level") && !PlayerPrefs.HasKey("exp"))
        {
            //=====초기 사용자 세팅=========
            PlayerPrefs.SetInt("money", 50);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("exp", 0);
        }
        money = PlayerPrefs.GetInt("money");
        level = PlayerPrefs.GetInt("level");
        exp = PlayerPrefs.GetInt("exp");

        MoneyView.GetComponent<Text>().text = money.ToString();
        LevelView.GetComponent<Text>().text = level.ToString();
    }
    //===========돈, 경험치 수정==============
    public void IncreaseMoney(int value)
    {
        money = money + value;
        PlayerPrefs.SetInt("money", money);
    } 

    public void DecreaseMoney(int value)
    {
        money -= value;
        PlayerPrefs.SetInt("money", money);
    }

    public void AddExp(int value)
    {
        exp += value;

        //렙업이 가능한 지 검사
        if (exp >= 100) {
            exp -= 100;
            level++;
        }
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("exp", exp);
    }
    

}
