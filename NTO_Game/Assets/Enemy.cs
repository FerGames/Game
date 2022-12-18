using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private NavMeshAgent Agent;
    private float distance;
    public Transform target;
    public Animator Animator;
    
    public Collider sword;
    void Attack(int damage)
    {
        kd = 0;
    }
    private IEnumerator SwordOff()
    {
        yield return new WaitForSeconds(1.5f);
        sword.enabled = false;
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    void Start()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            Animator.Play("Арматура|death");
            StartCoroutine(Die());
        }
        else
        {
            if (kd < maxKd)
                kd++;
            distance = Vector3.Distance(target.position, transform.GetChild(0).position);
            if (distance > 20)
            {
                Agent.enabled = false;
                Animator.SetBool("IsWalking", false);
            }
            else if (distance < 20 && distance > 3)
            {
                Animator.SetBool("IsWalking", true);
                Agent.enabled = true;
                Agent.SetDestination(target.position);
            }
            else
            {
                if (maxKd <= kd)
                {
                    //sword.enabled = true;
                    Animator.SetBool("IsWalking", false);
                    Agent.enabled = false;
                    Animator.Play("Арматура|Attack1");
                    Attack(damage);
                    StartCoroutine(SwordOff());
                }
            }
        }
    }
}
