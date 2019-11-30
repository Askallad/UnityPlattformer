using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float moveSpeed = 1f;
    bool moveRight = true;
    
   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 3.7f)
        {
            moveRight = false;
        }
        if (transform.position.x < 1.5f)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }


    /**private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("onTriggerExit");
            transform.parent = null;

        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = this.transform;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
        { 
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("onTriggerExit");
            collision.transform.parent = null;

        }
    }

}
