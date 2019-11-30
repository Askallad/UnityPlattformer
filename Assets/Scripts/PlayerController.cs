using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(PlayerPhysics))]  //Kein Movement ohne Physics script!
public class PlayerController : MonoBehaviour
{
    
    //Konstanten
    public float speed = 8;
    public float acceleration = 12;
    public float gravity = 10;
    public float jumpHeight = 5;

    public Animator animator;

    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;


    private SpriteRenderer renderer;
    private PlayerPhysics physicsScript;

    public float fallDelay = 1.5f;
    private GameObject block;

    //singleton
    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        physicsScript = GetComponent<PlayerPhysics>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // No more getting stuck on blocks
        if(physicsScript.bumped)
        {
            targetSpeed = 0;
            currentSpeed = 0;
        }
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currentSpeed = IncrementTowardSpeed(currentSpeed, targetSpeed, acceleration);

        if(physicsScript.grounded)
        {
            amountToMove.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                AudioManager.playSound("Jump");
                amountToMove.y = jumpHeight;
            }
        }


        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        
        ChooseAnimation(amountToMove);
        FlipCharacter(amountToMove.x);

        physicsScript.Move(amountToMove * Time.deltaTime);
        physicsScript.PlayerOutOfBounds();
    }

    private void FlipCharacter(float xMove)
    {
        float dir = Mathf.Sign(xMove);
        if (dir == 1 && renderer.flipX)
        {
            renderer.flipX = false;
        }

        if (dir == -1 && !renderer.flipX)
        {
            renderer.flipX = true;
        }
    }
    
    private void ChooseAnimation(Vector2 amountToMove)
    {
        if (!physicsScript.grounded)
        {
            if (Mathf.Sign(amountToMove.y) == 1)
            {
                animator.SetInteger("jump", 1);; // jump up @ 1
            }
            else
            {
                animator.SetInteger("jump", 2);;// fall @ 2
                AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (currInfo.length>1) // Fix this
                {
                    animator.SetBool("damage", true);
                }
            }
        }
        else
        {
            animator.SetInteger("jump", 0);;
            if (amountToMove.x == 0 || physicsScript.bumped)
            {
                animator.SetInteger("speed", 0); // Idle @ 0
            }
            else
            {
                animator.SetInteger("speed", 1); // Idle @ 0
            }
 
        }
    }
    
    
    // Nähere den Speed an gewollten Speed an.
    private float IncrementTowardSpeed(float rightnow, float target, float accel)
    {
        if(rightnow==target)
        {
            return rightnow;
        }
     else
        {
            float dir = Mathf.Sign(target - rightnow);
            rightnow += accel * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - rightnow)) ? rightnow : target;   // Wenn rightNow == target return target, sonst return rightnow. Das ist ein fancy if statement. else ist dann der Doppelpunkt
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collecting blocks
        if (collision.gameObject.tag == "Collectable")
        {
            AudioManager.playSound("Coin");
            Sprite mySprite = collision.GetComponent<SpriteRenderer>().sprite;
            Blocksystem.BS.addBlock(mySprite);
            Blocksystem.BS.updateBlockNumber();
            Destroy(collision.gameObject);
        }

        //Blocks falling down when player hit them
        if (collision.gameObject.tag == "BuildBlock")
        {
            block = collision.gameObject;
            StartCoroutine(FallAfterDelay());
            
        }

        if(collision.gameObject.layer == 11)
        {
            animator.SetBool("damage", true);
            InvokeRepeating("FlashSprite", 0.1f, 0.2f);
            GameManager.instance.Invoke("Death", 2);
        }
    }

    void FlashSprite()
    {
        renderer.enabled = !renderer.enabled;
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        block.GetComponent<Rigidbody2D>().isKinematic = false;
    }


}
