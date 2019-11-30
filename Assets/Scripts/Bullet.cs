using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    public float bulletSpeed = -1f;
    public float lifetime = 3;
    private Animator animator;

    private void Start()
    {
        gameObject.tag = "Bullet";
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime);
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveTransform = new Vector2(bulletSpeed, 0);
        transform.Translate(moveTransform * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if(hitInfo.gameObject.layer == 8 || hitInfo.gameObject.layer == 10)
        {
            if (hitInfo.gameObject.tag == "Enemy")
            {
                Enemy e = hitInfo.gameObject.GetComponent<Enemy>();
                e.decreaseHealth();
            }
            animator.SetBool("death", true);
            CircleCollider2D coll = gameObject.GetComponent<CircleCollider2D>();
            coll.enabled = false;
        }
    }

    public void DeleteMe()
    {
        Destroy(gameObject);
    }

}
