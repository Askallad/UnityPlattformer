using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.tag == "Player")
        {
            gm.Win();
        }
    }

}
