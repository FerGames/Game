using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int attack;
    public int damage;
    public float kd = 0;
    public float maxKd = 180;
    public float attackSpeed;

    public float stackingTimer;

    public float curseStack;
    public float timerTime;
    public float actionTime;

    public bool isCursed;

    public virtual void CurseAction()
    {

    }

    void Start()
    {
        timerTime = actionTime;
    }
    void FixedUpdate()
    {
        timerTime -= Time.deltaTime;
        if (timerTime < 0 && isCursed)
        {
            CurseAction();
            timerTime = actionTime;
        }
    }
}
