using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;
    private GameObject playerInstance;

    private float maxHealth = 20f;

    private float currentHealth ;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       gm = GameManager.instance;
       playerInstance = gm.playerReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Doooooooom();
        }

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            decreaseHealth();
        }
    }

    public void decreaseHealth()
    {
        Debug.Log("Hit");

        currentHealth = currentHealth - 1;
        if (currentHealth <= 0)
        {
            Wiiiiiiiiin();
        }
    }

    void Doooooooom()
    {
        gm.Death();
    }

    void Wiiiiiiiiin()
    {
        AudioManager.playSound("Winning");
        Destroy(gameObject);
    }
}
