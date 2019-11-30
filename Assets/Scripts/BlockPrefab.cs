using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPrefab : MonoBehaviour
{
    public float fallDelay = 3.0f;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.gameObject.tag == "BuildBlock")
        {
            StartCoroutine(FallAfterDelay());
            Invoke("RemoveBlock", 8);
        }
    }

    

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void RemoveBlock()
    {
        
    }
}
