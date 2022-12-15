using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public AudioSource asus;
    void Start()
    {
        asus = GetComponent<AudioSource>();
        
    }
    public override void CurseAction()
    {
        hp -= 1;
    }

    void Update()
    {
        asus.Play();
    }
}
