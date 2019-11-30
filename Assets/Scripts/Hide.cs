using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    public GameObject blockPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" && this.gameObject.tag == "HideObject")
        {
            AudioManager.playSound("FoundBlock");
            Destroy(gameObject);
            Instantiate(blockPrefab, transform.position, transform.rotation);

        }
        else if (collision.gameObject.tag == "Bullet" && this.gameObject.tag == "Shootable")
        {
            InvokeRepeating("FlashSprite", 0.1f, 0.2f);
            Invoke("DeleteMe", 1.5f);
        }
    }

    void FlashSprite() // Code duplication is baaad.
    {
        spriteRend.enabled = !spriteRend.enabled;
    }

    void DeleteMe()
    {
        Destroy(gameObject);
    }
}
