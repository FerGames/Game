using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Curse : MonoBehaviour
{
    public float radius;

    public float timerTime;
    public int actionTime;
    public int stackTime;
    

    public LayerMask player;
    public LayerMask enemy;
    public LayerMask entity;

    public Collider[] entities;
    public Collider[] enemies;
    public Collider[] players;
    private System.Random Random;

    public float timeBtwTelep = 60;
    public float timerBtwTelep;

    void Start()
    {
        Random = new System.Random();
    }

    void FixedUpdate()
    {
        timerBtwTelep++;
        players = Physics.OverlapSphere(transform.position, radius, player);
        enemies = Physics.OverlapSphere(transform.position, radius, enemy);

        timerTime += Time.deltaTime;
        if(Convert.ToInt32(timerTime) % stackTime == 0)
        {
            CurseAction();
            timerTime = actionTime;
        }
    }

    public void CurseAction()
    {
        foreach(Collider enemy in enemies)
        {
            enemy.GetComponent<Entity>().stackingTimer += 1;
            enemy.GetComponent<Entity>().isCursed = true;
            if(enemy.GetComponent<Entity>().stackingTimer % stackTime == 0)
                enemy.GetComponent<Entity>().curseStack += 1;
        }
        foreach (Collider player in players)
        {
            player.GetComponent<Entity>().stackingTimer += 1;
            player.GetComponent<Entity>().isCursed = true;
            if (player.GetComponent<Entity>().stackingTimer % stackTime == 0)
                player.GetComponent<Entity>().curseStack += 1;
            if(timerBtwTelep >= timeBtwTelep)
            {
                timerBtwTelep = 0;
                transform.position = new Vector3(transform.position.x + Random.Next(-30, 30), transform.position.y + Random.Next(-30, 30), transform.position.z + Random.Next(-30, 30));
                Debug.Log("t");
            }
        }
    }
}