using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Collider sword;
    public int damage;
    void Start()
    {
        sword = GetComponent<Collider>();
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.GetComponent<Player>() != null)
        {
            collider.gameObject.GetComponent<Player>().hp -= damage;
            //Debug.Log("attack");
            
        }sword.enabled = false;
    }
}
