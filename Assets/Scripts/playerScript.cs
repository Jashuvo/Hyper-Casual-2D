using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D playerRigid;
    public float jumpForce = 10f;
    public float gravity = 1f;
    public float jumpHeight = 10f;
    public float leftBound,rightBound;

    bool isDragging = false;
    Vector2 touchPos,playerPos, dragPos;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            if(playerRigid.velocity.y <= 0f){
                jumpForce = gravity * jumpHeight;
                playerRigid.velocity = new Vector2(0, jumpForce);
                DestroyAndMakePlatform(collision);
            }
            
        }
    }

    void DestroyAndMakePlatform(Collider2D platform)
    {
        Destroy(platform);
        spawnPlatformScript.instance.MakePlatform();
    }
    void Update()
    {
        AddGravity();
        GetInput();
        MovePlayer();
    }

    void MovePlayer(){
        if(isDragging){
            dragPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(playerPos.x + (dragPos.x - touchPos.x), playerPos.y);

            if(transform.position.x < leftBound)
            {
                transform.position = new Vector2(-4.25f, transform.position.y);
            }
            if (transform.position.x > rightBound)
            {
                transform.position = new Vector2(4.25f, transform.position.y);
            }
        }
    }
    void GetInput(){
        if(Input.GetMouseButtonDown(0)){
            isDragging = true;
            touchPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerPos = transform.position;
        }
        else if(Input.GetMouseButtonUp(0)){
            isDragging = false;
        }
    }


    void AddGravity(){
        playerRigid.velocity =new Vector2(0,playerRigid.velocity.y - (gravity * gravity));
    }
}
