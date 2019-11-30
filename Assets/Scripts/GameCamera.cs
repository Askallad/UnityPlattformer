using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    private Transform target;
    public float cameraTrackSpeed = 10;


    public void SetTarget(Transform t)
    {
        target = t;
    }

    void LateUpdate()
    {
        if(target)
        {
            float x = IncrementToward(transform.position.x, target.position.x, cameraTrackSpeed);
            float y = IncrementToward(transform.position.y, target.position.y, cameraTrackSpeed);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    private float IncrementToward(float rightnow, float target, float accel)
    {
        if (rightnow == target)
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

    
}
