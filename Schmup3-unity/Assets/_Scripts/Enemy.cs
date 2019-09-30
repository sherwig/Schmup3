﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Set in Inspector: Enemy")]

    public float speed = 10f; 
    public float fireRate=.3f;
    public float health = 10; 
    public int score = 100;

    protected boundsScript bndCheck;

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    private void Awake()
    {
        bndCheck = GetComponent<boundsScript>();
    }



    // Update is called once per frame
    void Update()
    {
        Move();

        if (bndCheck!=null && bndCheck.offDown)
        {    
          Destroy(gameObject); 
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGo = collision.gameObject;
        if(otherGo.tag=="ProjectileHero")
        {
            Destroy(otherGo);
            Destroy(gameObject);
        }
        else
        {
            print("Enemy hit by non-projectileHero: " + otherGo.name);
        }
    }
}
