using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocksystem : MonoBehaviour
{
    public List<Sprite> allBlocks;
    public static Blocksystem BS;
    

    void Awake()
    {
        if (BS == null)
        {
            BS = this;
        }

        allBlocks = new List<Sprite>();     
    }


   public List<Sprite> getAllBlocks()
    {
        return allBlocks;
    }

    public void addBlock(Sprite newBlock)
    {
        allBlocks.Add(newBlock);
    }

    public void updateBlockNumber()
    {
        UIManager.currentUIManager.updateBlockNumber(allBlocks.Count);
    }
}
