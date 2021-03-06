﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRL_Player : MonoBehaviour
{

    [Header("GameObjects/Components")]
    public GameObject playerSpriteObject;
    public Animator animator;
    public Rigidbody2D rb;
    
    [Header("Movement Variables")]
    public float runSpeed;
    public float jumpForce;
    private Vector2 movementForce;
    public bool isGrounded;

    [Header("Art/Animation Variables")]
    private bool isFlipped = false;
    private bool isRunning = false;
    private bool isIdle = false;
    private float curLocation = 0.0f;
    private float oldLocation = 0.0f;
    private float runVelocity;
    public float velocityCorrection;
    public bool isJumping = false;
    private Vector3 playerUnflippedScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 playerFlippedScale = new Vector3(-1.0f, 1.0f, 1.0f);

    [Header("Particle FX")]
    public ParticleSystem[] footParticles = new ParticleSystem[0];
    private float[] footParticleEmission = new float[0];

    // Start is called before the first frame update
    void Start()
    {
        //Sets reference to components within script on start function.
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        //for (int i = 0; i < footParticles.Length; i++)
        //{
        //    footParticleEmission[i] = footParticles[i].emissionRate;
        //    Debug.Log("FoorParticle " + i + " = " + footParticleEmission[i]);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        getInput();

        UpdateAnimator();
    }

    private void getInput()
    {

        // Gets horizontal axis input and applies to RigidBody2D Component on Character
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.0f)
        {
            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                if (isFlipped)
                {
                    playerSpriteObject.transform.localScale = playerUnflippedScale;
                    isFlipped = false;
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") < 0.0f)
                {
                    if (!isFlipped)
                    {
                        playerSpriteObject.transform.localScale = playerFlippedScale;
                        isFlipped = true;
                    }
                }
            }
            this.transform.position += Vector3.right * Input.GetAxis("Horizontal") * runSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }

    }

    private void jump()
    {
        animator.SetTrigger("jump");
        disableFootParticles();
        isJumping = true;
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpForce);
    }

    public void enableFootParticles()
    {
        for (int i = 0; i<footParticles.Length; i++)
        {
            //footParticles[i].emissionRate = footParticleEmission[i];
        }
    }

    public void disableFootParticles()
    {
        for (int i = 0; i < footParticles.Length; i++)
        {
            //footParticles[i].emissionRate = 0.0f;
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", isRunning);
    }


}
