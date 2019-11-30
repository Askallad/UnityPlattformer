using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
        
    [SerializeField] private Transform rightBarrel;
    [SerializeField] private Transform leftBarrel;
    public GameObject bullet;
    private SpriteRenderer rend;
    private Buildsystem bs;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        bs = Buildsystem.instance;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && bs.enableShooting)
        {
            AudioManager.playSound("Fire");
            Shoot();
        }
    }

    void Shoot()
    {
        int dir = 1;
        Transform firePoint = rightBarrel;
        if (rend.flipX)
        {
            dir = -1;
            firePoint = leftBarrel;
        }

        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
