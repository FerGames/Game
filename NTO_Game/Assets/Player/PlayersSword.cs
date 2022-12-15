using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSword : MonoBehaviour
{
    public Collider sword;
    public int damage;
    void Start()
    {
        sword = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Enemy>() != null)
        {
            collider.gameObject.GetComponent<Enemy>().hp -= damage;
            //Debug.Log("attack");
            
        }sword.enabled = false;
    }
}
