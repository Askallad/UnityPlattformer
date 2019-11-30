using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBoard : MonoBehaviour
{
    
    public float speed = 1.0f;
    private Vector2 target;
    private Vector2 position;
    private GameObject player;
    public GameObject targetPoint;

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "BuildBlock")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //float playerY = (float)player.transform.position.y;
            //float newY = playerY - 0.235f;
            target = new Vector2(gameObject.transform.position.x, targetPoint.transform.position.y);
            position = gameObject.transform.position;
            collision.GetComponent<BoxCollider2D>().transform.SetParent(transform);
            float step = speed * Time.deltaTime;
            while (position != target){
                transform.position = Vector2.MoveTowards(transform.position, target, step);
                position = transform.position;
            }
            Destroy(collision.gameObject);
            
            
        }
    }
      

}
