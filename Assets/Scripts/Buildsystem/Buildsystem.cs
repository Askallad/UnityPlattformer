using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Buildsystem : MonoBehaviour
{
    [SerializeField]
    public float blockSize;
    
    [SerializeField]
    public LayerMask layerMask;

    public float slowedTime = 0.2f;
    
    private Sprite currentSprite;
    private int currentBlockID = 0;

    //variables for the Block Template
    private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    //bool to controll building system
    private bool buildModeOn = false;
    // bool to control if a sprite is allowed to build there
    private bool buildBlocked = false;
    // invoke a delay between leaving buildmode and ability to shoot
    public bool enableShooting = true;

    public static Buildsystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            
            if (Blocksystem.BS.allBlocks.Count == 0)
            {
                Debug.Log("No blocks available");
                return;
            }
            else
            {
                buildModeOn = !buildModeOn;
                ToggleShooting();
                ToogleSpeed();
                //if there is a blockTemplate, destroy it
                if (blockTemplate != null)
                {
                    Destroy(blockTemplate);
                }
                if (currentSprite == null)
                {
                    if(Blocksystem.BS.allBlocks[currentBlockID] != null)
                    {
                        currentSprite = Blocksystem.BS.allBlocks[currentBlockID];          
                    }
                }

                if (buildModeOn)
                {
                    blockTemplate = new GameObject("CurrentBlockTemplate");
                    currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                    currentRend.sprite = currentSprite;
                    currentRend.sortingLayerName = "Tilemap";
                }
            }   
        }
        if (buildModeOn && blockTemplate != null)
        {
            float newPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / blockSize) * blockSize;
            float newPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / blockSize) * blockSize;
            blockTemplate.transform.position = new Vector2(newPosX, newPosY);

            RaycastHit2D rayHit;
            rayHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, layerMask);

            if (rayHit.collider != null)
            {
                buildBlocked = true;
            }
            else
            {
                buildBlocked = false;
            }

            if (buildBlocked)
            { currentRend.color = new Color(1f, 0f, 0f, 1f); }
            else { currentRend.color = new Color(1f, 1f, 1f, 1f); }
        }

        if (Input.GetMouseButtonDown(0) && !buildBlocked && buildModeOn)
        {
            if(Blocksystem.BS.allBlocks.Count == 0)
            {
                Debug.Log("No Blocks available");
                //Ui Text here, finish BuildMode

            }   
            else
            {
                //Build Block in Game Scene
                GameObject newBlock = new GameObject("blockPrefab(clone)");
                newBlock.transform.position = blockTemplate.transform.position;
                SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
                newRend.sprite = currentSprite;
                newRend.sortingLayerName = "Tilemap";
                newBlock.AddComponent<BoxCollider2D>();
                Rigidbody2D rigidbody= newBlock.AddComponent<Rigidbody2D>();
                rigidbody.isKinematic = true; ;
                newBlock.layer = 8;
                newBlock.tag = "BuildBlock";
                newBlock.GetComponent<BoxCollider2D>().isTrigger = true;
                Blocksystem.BS.allBlocks.Remove(currentSprite);
                Blocksystem.BS.updateBlockNumber();
                if (Blocksystem.BS.allBlocks.Count == 0)
                {
                    buildModeOn = false;
                    Destroy(blockTemplate);
                    Invoke("ToggleShooting", 0.5f);
                    ToogleSpeed();
                }
                
            }
                
        }

    }

    private void ToggleShooting()
    {
        enableShooting = !enableShooting;
    }


    private void ToogleSpeed()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = slowedTime;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
