using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour
{
    public LayerMask collisionMask;

    [HideInInspector] //Controller needs grounded info
    public bool grounded;


    [HideInInspector] //Controller needs bumped info
    public bool bumped;

    public Camera camera;
    private BoxCollider2D collider;
    private Vector3 size;
    private Vector3 center;
    private bool onMovingPlattform;
    private float skin = .005f;

    Ray ray;
    RaycastHit2D hit;



    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        size = collider.size;
        center = collider.offset;
        camera = Camera.main;
    }

    // All the raycast collision is in here
    public void Move(Vector2 moveAmount)
    {
        float yRayLength = moveAmount.y;
        float xRayLength = moveAmount.x;
        Vector2 playerPosition = transform.position;
        grounded = false;
        bumped = false;
        onMovingPlattform = false;
        //Check above&below
        for(int i = 0; i < 3; i++)
        {
            float dirY = Mathf.Sign(yRayLength); //Checking upwards or below?
            float x1 = (playerPosition.x + center.x - (size.x / 2)); //Setting x coordinates for left, center and right end of collider for raycasts
            float x2 = (playerPosition.x + center.x + (size.x / 2));
            float x3 = playerPosition.x + center.x;
            float[] arr = new float[] { x1, x2, x3 };
            float y = playerPosition.y + center.y + size.y/2 * dirY; //Setting y coordinate (top/bottom)

            Vector2 org = new Vector2(arr[i], y);
            hit = Physics2D.Raycast(org, new Vector2(0, dirY), Mathf.Abs(yRayLength) + skin,  collisionMask);
            //Debug.Log("Hit something:" + hit.transform + " " + hit.collider + " at: " + hit.point);
            ray = new Ray(new Vector2(arr[i], y), new Vector2(0, dirY));
            Debug.DrawRay(ray.origin, ray.direction);

            if (hit)        
        {
                float distance = Vector2.Distance(org, hit.point);
                //Debug.Log("distance is: " + distance);
                if(distance > skin) //Stop minimal vor dem Boden um Raycasts nicht zu buggen.
                {
                    yRayLength = distance*dirY - skin*dirY;
                }
                else
                {
                    yRayLength = 0;

                }
                grounded = true;
                break;
            }
        }


        //Check left&right
        for (int i = 0; i < 3; i++)
        {
            float dirX = Mathf.Sign(xRayLength); //Checking left or right?


            float x = playerPosition.x + center.x + size.x / 2 * dirX;
            float y = playerPosition.y + center.y - size.y / 2 + size.y/2 * i; //Setting y coordinate (top/bottom)
            Vector2 org = new Vector2(x, y);
            hit = Physics2D.Raycast(org, new Vector2(dirX, 0), Mathf.Abs(yRayLength) + skin, collisionMask);
            //Debug.Log("Hit something:" + hit.transform + " " + hit.collider + " at: " + hit.point);
            ray = new Ray(new Vector2(x, y), new Vector2(dirX,0));
            Debug.DrawRay(ray.origin, ray.direction);

            if (hit)
            {
                if (hit.transform.gameObject.tag == "Slope"&&grounded)
                {
                    yRayLength = yRayLength+15f;
                    break;
                }
                else
                {
                    float distance = Vector2.Distance(org, hit.point);
                    //Debug.Log("distance is: " + distance);
                    if (distance > skin) //Stop minimal vor dem Boden um Raycasts nicht zu buggen.
                    {
                        xRayLength = distance * dirX - skin * dirX;
                    }
                    else
                    {
                        xRayLength = 0;

                    }



                    bumped = true;
                    break;
                }
            }
        }

        if(!grounded && !bumped)
        {
            Vector2 playerDirection = new Vector2(xRayLength, yRayLength);
            Vector2 rayOrigin = new Vector2(playerPosition.x + center.x + size.x / 2 * Mathf.Sign(xRayLength), playerPosition.y + center.y + size.y / 2 * Mathf.Sign(yRayLength));
            Debug.DrawRay(rayOrigin, playerDirection.normalized);
            if (Physics2D.Raycast(rayOrigin, playerDirection.normalized, Mathf.Sqrt(xRayLength * xRayLength + yRayLength * yRayLength), collisionMask))
            {
                grounded = true;
                yRayLength = 0;
            }
        }
        

        Vector2 modifiedTransform = new Vector2(xRayLength, yRayLength);
        transform.Translate(modifiedTransform);
    }
    
    public void PlayerOutOfBounds()
    {
        if (camera)
        { 
            GameObject player = this.gameObject;
            Vector2 pointOnScreen = camera.WorldToScreenPoint(player.GetComponentInChildren<Renderer>().bounds.center);
            if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
            {
                
                GameManager.instance.Death();
            }
        }
       
    }



}






/**
    GameObject tm = hit.transform.gameObject;
    if(tm.tag == "Tilemap")
    {}    //There is a function called Tilemap.GetTileBase but apparently it only works if you do your own scriptable tiles..
    //this was supposed to check for slopes..but its not working.
    y = playerPosition.y + center.y - size.y / 2 + size.y - 0.1f; //Setting y coordinate (top/bottom)
    org.y = y;
    hit = Physics2D.Raycast(org, new Vector2(dirX, 0), Mathf.Abs(yRayLength) + skin, collisionMask);
    ray = new Ray(hit.point, hit.normal);
    Debug.DrawRay(ray.origin, ray.direction);
    if(hit.normal.normalized == Vector2.up.normalized )
    {
        Debug.Log("sth hit" + hit.collider);
    } 
**/
