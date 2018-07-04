﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;﻿

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    
    private Rigidbody2D myRigidbody;
    //private Collider2D myCollider;
    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

   // private PowerUp thePowerUp;



	// Use this for initialization
	void Start () {
       // thePowerUp = GameObject.FindObjectOfType<PowerUp>();
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {

        // grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        MilestoneSpeedIncrease();

        JumpControlCheck();

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
	}
    public void MilestoneSpeedIncrease()
    {
        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    }
    public void JumpControlCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //DoubleJump
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }
            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping) //OneJump
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                
            }
            else 
            {
                stoppedJumping = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) //When Jump key has been released, reset JumpTimeCounter
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }
    }


    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            

            deathSound.Play();
           /* if (thePowerUp.doublePoints == true || thePowerUp.safeMode == true)
            {
                thePowerUp.doublePoints = false;
                thePowerUp.safeMode = false;
            }*/
        }
       
    }

}
