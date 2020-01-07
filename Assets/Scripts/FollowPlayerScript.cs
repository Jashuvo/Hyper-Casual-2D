using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 0.3f;
    public float offsetY;

    Vector2 velocity = Vector2.zero;
    void Update()
    {
        Vector2 playerPos = player.transform.TransformPoint(new Vector3(0, offsetY));
        if(playerPos.y < transform.position.y)
        {
            return;
        }

        playerPos = new Vector2(0, playerPos.y);
        transform.position = Vector2.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

    }
}
