using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    Vector2 startPos, targetPos;
    float randomX;
    float smoothTime = 0.1f;
    Vector2 velocity = Vector2.zero;
    
    public void ChangePosition()
    {
        targetPos = transform.position;
        if(UnityEngine.Random.Range(0,2) == 0)
        {
            randomX = 10;
        }
        else
        {
            randomX = 10;
        }
        startPos = new Vector2(targetPos.x + randomX, targetPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(targetPos, transform.position) > 0.01f)
        {
            MoveToTargetPosition();
        }
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
