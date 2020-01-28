using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D playerRigid;
    private float jumpForce = 10f;
    public float gravity = 1f;
    public float jumpHeight = 10f;
    public float leftBound,rightBound;
    public GameObject jumpEffect;
    public GameObject deathEffect;
    private Vector2 startPos, endPos;
    public GameObject touchToStartText ,titleText;

    bool isDragging = false;
    bool isDead = false;
    bool isStart = false;
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
                ScoreManagerScript.instance.AddScore();
                SoundManager.instance.JumpSound();
                gravity += 0.01f;
                Camera.main.backgroundColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
                DestroyAndMakePlatform(collision);
                JumpEffect();
            }
            
        }
    }

    private void JumpEffect()
    {
       Destroy( Instantiate(jumpEffect, transform.position, Quaternion.identity), 0.5f);
    }

    void DestroyAndMakePlatform(Collider2D platform)
    {
        //Destroy(platform.gameObject);
        platform.gameObject.SetActive(false);
        spawnPlatformScript.instance.MakePlatform();
    }
    void Update()
    {
        WaitForTouch();
        if(isDead){
            return;
        }
        if(!isStart){
            return;
        }
        AddGravity();
        GetInput();
        MovePlayer();
        CheckPlayer();
    }
    void WaitForTouch(){
        if(!isStart){
            if(Input.GetMouseButtonDown(0)){
                isStart = true;
                touchToStartText.SetActive(false);
                titleText.SetActive(false);
            }
        }
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

        /*if(Input.GetMouseButtonDown(0)){
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0)){
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(endPos.x > startPos.x){
            transform.position = new Vector2(playerPos.x + startPos.x, playerPos.y);
        }else if(endPos.x < startPos.x){
             transform.position = new Vector2(playerPos.x + endPos.x, playerPos.y);
        }*/

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

    void CheckPlayer()
    {
        if(!isDead && transform.position.y < Camera.main.transform.position.y - 10)
        {
            isDead = true;
            playerRigid.velocity = Vector2.zero;
            Destroy( Instantiate(deathEffect, transform.position, Quaternion.identity), 1f);
            SoundManager.instance.DeathSound();
            GameManager.instance.GameOver();
        }
    }
}



