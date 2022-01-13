using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeReference] private GameObject water;

    //variables unavailable in editor
    private Rigidbody2D body;
    private Vector3 moveDelta;
    private Animator anim;
    private bool grounded;
    private bool inWater;
    private float basicMovement;



    // Start is called before the first frame update
    void Start()
    {
        //take references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement bindings
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Jump");

        //checking movement multiplayer
        basicMovement = water.GetComponent<Water>().BasicMovement();

        //chcecking if player is in water
        inWater = water.GetComponent<Water>().IsInWater();


        //Reset MoveDelta
        moveDelta = new Vector3(x, y, 0);

        //Swap sprite direction, wether youre going right or left
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        //Move left or right
        body.velocity = new Vector2((x * movementSpeed)/basicMovement, body.velocity.y/ basicMovement);

        //Jumping
        if(y>0 && grounded && !inWater) //check if target is on ground and not in water, if yes it can jump
        {
            Jump();
        }

        //Animator parameters
        anim.SetBool("run",x!=0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight); //adding a velocity to jump
        IsGrounded(false);
        anim.SetTrigger("jump"); //Starting playing animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") 
            IsGrounded(true);   
    } 
    
    // chceck if target can attack
    public bool canAttack()
    {
        if (!inWater) 
        { 
            return true; 
        }      
        else
        {
            return false;
        }
    }
   
    //setting grounded variable
    public bool IsGrounded(bool var)
    {
        return grounded = var;
    }
   
}
