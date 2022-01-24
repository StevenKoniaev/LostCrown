using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    public CharacterController2D controller;
    public Animator animator;
    public float dashduration = 0;
    public float dashspeed = 50f;
    public int leftTotal = 0;
    public float leftTimeDelay = 0;
    public int rightTotal = 0;
    public float rightTimeDelay = 0;
    public float olddashdur;
    public GameObject EchoObj;
    public float runSpeed = 40f;
    public float wallJumpTime = .2f;
    public bool isDashing = false;
    public Transform wallGrabPoint;
    public float horizontalMove = 0f;
    public Rigidbody2D m_Rigidbody;
    #endregion

    #region Private Variables
     bool crouch = false;
    private float rememberSpeed;
    EchoEffect EchoScript;
    private float g; 
    bool jump = false;
    private bool canGrab, isGrabbing;
    public float wallJumpCounter = 0;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {

       
        g = m_Rigidbody.gravityScale;
        rememberSpeed = runSpeed;
        olddashdur = dashduration;

        EchoScript = EchoObj.GetComponent<EchoEffect>();

       
    }
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (wallJumpCounter <= 0)
        {

     
        //Handles horizontal movement
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jump input
        if (Input.GetKeyDown(KeyCode.W) && isDashing == false){
            jump = true;
            animator.SetTrigger("isJumping");

        }
        else
        {
            
            animator.SetBool("isJumping", false);
            }

        }
      
        //Double Tap Left or Right
        if (Input.GetKeyDown(KeyCode.D) & controller.m_Grounded == true)
        {
            rightTotal += 1;
        } else if (Input.GetKeyDown(KeyCode.A) & controller.m_Grounded == true)
        {
            leftTotal += 1;
        }


        //Within time imit
        if ((rightTotal == 1) && rightTimeDelay< .3){
            rightTimeDelay += Time.deltaTime;
        } else if ((leftTotal ==1)&& leftTimeDelay < .3)
        {
            leftTimeDelay += Time.deltaTime;
        }

        //Reset
        if ((rightTotal ==1) && rightTimeDelay >= .3)
        {
            rightTimeDelay = 0;
            rightTotal = 0;
        }else if ((leftTotal ==1) && leftTimeDelay >= .3)
        {
            leftTimeDelay = 0;
            leftTotal = 0;
        }

        //Start Dash
        if ((rightTotal == 2) && rightTimeDelay < .3)
        {
            m_Rigidbody.gravityScale = 0;
            EchoScript.Dashing(true);
            isDashing = true;
            runSpeed = dashspeed;
            rightTotal = 0;
        } else if ((leftTotal == 2) && leftTimeDelay < .3)
        {
            m_Rigidbody.gravityScale = 0;
            EchoScript.Dashing(true);
            isDashing = true;
            runSpeed = dashspeed;
            leftTotal = 0;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            m_Rigidbody.gravityScale = g;
            EchoScript.Dashing(false);
            runSpeed = rememberSpeed;
            isDashing = false;
           
        } else if (Input.GetKeyUp(KeyCode.A))
        {
            m_Rigidbody.gravityScale = g;
            EchoScript.Dashing(false);
            runSpeed = rememberSpeed;
            isDashing = false;
        }
        if ((rightTotal == 2)&& rightTimeDelay >= .3)
        {
            m_Rigidbody.gravityScale = g;
            EchoScript.Dashing(false);
            rightTotal = 0;
            rightTimeDelay = 0;
            runSpeed = rememberSpeed;
            isDashing = false;
           
        } else if ((leftTotal == 2)&& leftTimeDelay >= .3)
        {
            m_Rigidbody.gravityScale = g;
            EchoScript.Dashing(false);
            leftTotal = 0;
            leftTimeDelay = 0;
            runSpeed = rememberSpeed;
            isDashing = false;
        }

        if (runSpeed == dashspeed)
        {
            dashduration += Time.deltaTime;
        }

        if (dashduration > 1)
        {
            m_Rigidbody.gravityScale = g;
            runSpeed = rememberSpeed;
            dashduration = olddashdur;
            EchoScript.Dashing(false);
            rightTimeDelay = 0;
            rightTotal = 0;
            isDashing = false;
        }
        
     

     


        //wall jump
        canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .1f, controller.m_WhatIsGround);

        isGrabbing = false;
        if (canGrab == true && !controller.m_Grounded == true)
        {
            if((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
            {
                isGrabbing = true;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            controller.m_CrouchSpeed = 0f;

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        animator.SetBool("isGrabbing", isGrabbing);
    }

    


    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);   
    }

     void FixedUpdate()
    {
        if (isGrabbing == true)
        {
            controller.m_CrouchSpeed = 0f;
            m_Rigidbody.gravityScale = 0;
            m_Rigidbody.velocity = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W))
            {
                wallJumpCounter = wallJumpTime;
                m_Rigidbody.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * 15f, 10f);
                m_Rigidbody.gravityScale = g;
                isGrabbing = false;
            }
        }
        else
        {
            m_Rigidbody.gravityScale = g;
        }

        if (wallJumpCounter <= 0)
        {
            //Move char 
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        }
    }

    public void SlidingForce()
    {

        controller.m_CrouchSpeed = 1;
        runSpeed = 30f;
    }
    public void SlidingEnd()
    {
        controller.m_CrouchSpeed = 0;
        runSpeed = rememberSpeed;
    }

    public void ComboMove()
    {
        if (controller.m_FacingRight == true)
        {
            controller.Move(5f, crouch, jump);
        }else
        {
            controller.Move(-5f, crouch, jump);
        }
      
    }

  
}



