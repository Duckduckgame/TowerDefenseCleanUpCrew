﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{

    float inputHori;
    float inputVert;
    [SerializeField]
    float mvmntSpeed = 5;
    [SerializeField]
    float maxSpeed = 5;
    Rigidbody rb;
    [SerializeField]
    float distanceFromFixer;

    public GameObject fixableObject;
    FixingHandler fixingHandler;

    SpriteRenderer  spriteRenderer;
    Animator animator;
    public float corpseCount = 0;

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FlatMovement();

        FixObject();
        if (fixableObject != null)
            fixingHandler = fixableObject.GetComponent<FixingHandler>();
    }

    void FlatMovement()
    {

        inputHori = Input.GetAxisRaw("Horizontal");
        inputVert = Input.GetAxisRaw("Vertical");

        if(inputHori < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (inputHori > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(inputHori, 0, inputVert) * mvmntSpeed, ForceMode.VelocityChange);

            animator.SetBool("isWalking", true);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }


        if (inputHori == 0 && inputVert == 0)
        {
            animator.SetBool("isWalking", false);
            rb.velocity = Vector3.zero;
        }

    }

    void FixObject()
    {
        if (Input.GetKey(KeyCode.Space) && fixableObject != null && Vector3.Distance(transform.position, fixableObject.transform.position) < distanceFromFixer)
        {
            animator.SetBool("isAttacking", true);
            if (fixingHandler == null)
                fixingHandler = fixableObject.GetComponent<FixingHandler>();

            fixingHandler.life++;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clips[1];
                audioSource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            corpseCount++;
            audioSource.clip = clips[0];
            audioSource.Play();
        }
    }

}
