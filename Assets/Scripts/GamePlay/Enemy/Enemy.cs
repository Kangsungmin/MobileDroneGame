using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour {
    private string Ename;
    private float speed;
    private float hp, maxhp;
    private float durable;
    private int gold;//처치시 플레이어가 얻는 골드
    private int exp;//처치시 플에이어가 얻는 경험치
    private float range;
    private string state;
    protected List<Item> DropItemList = new List<Item>();
    public GameObject EnemyDataUI;
    public Image healthBar;
    public GameObject[] Player;//Player
    public GameObject PlayEnvironment;
    // Use this for initialization

    public string Name
    {
        get { return Ename; }
        set { Ename = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float MaxHP
    {
        get { return maxhp; }
        set { maxhp = value; }
    }
    public float Durable
    {
        get { return durable; }
        set { durable = value; }
    }

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    

    public float Range
    {
        get { return range; }
        set { range = value; }
    }
    public string State
    {
        get { return state; }
        set { state = value; }
    }
    abstract public void TakeDamage(float amount);
    abstract public void DropItems();
}
