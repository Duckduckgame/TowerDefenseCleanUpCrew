using System.Collections;
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

    public GameObject fixableObject;
    FixingHandler fixingHandler;

    SpriteRenderer  spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FlatMovement();

        FixObject();

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
        if (Input.GetKey(KeyCode.Space) && fixableObject != null)
        {
            if (fixingHandler == null)
                fixingHandler = fixableObject.GetComponent<FixingHandler>();

            fixingHandler.life++;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        fixableObject = null;
        fixingHandler = null;
    }
}
