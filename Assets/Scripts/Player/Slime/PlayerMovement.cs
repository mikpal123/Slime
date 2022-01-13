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
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;

    //variables unavailable in editor
    private BoxCollider2D boxColider;
    private Rigidbody2D body;
    private Vector3 moveDelta;
    private Animator anim;
    private bool inWater;
    private float basicMovement;
    private float wallJumpCooldown;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        //take references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement bindings
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Jump");

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

        

        //Animator parameters
        anim.SetBool("run",x!=0);
        anim.SetBool("grounded", IsGrounded());

        //Wall Jump
        WallJump();
        
    }

    private void Jump()
    {   
        //jump
        if (IsGrounded() && !inWater)//check if target is on ground and not in water, if yes it can jump  
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight); //adding a velocity to jump
            anim.SetTrigger("jump"); //Starting playing animation
        }
        //walljump
        else if (IsOnWall() && !IsGrounded())
        {
            if (x == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);   //jumping off te wall
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 0, 1); //wall jumping
            wallJumpCooldown = 0;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
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
   
    //check if we are om the ground
    public bool IsGrounded()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, Vector2.down, 0.01f, groundMask); //raycast to check if there are near ground
        return Hit.collider != null;
    }

    public bool IsOnWall()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.01f, wallMask); //raycast to check if there are near wall
        return Hit.collider != null;
    }

    private void WallJump()   //Wall jump
    {
        
        if (wallJumpCooldown < 0.1f)  //check cooldown
        {
            body.velocity = new Vector2((x * movementSpeed) / basicMovement, body.velocity.y / basicMovement);

            if (IsOnWall() && !IsGrounded())    //check if can jump
            {
                body.gravityScale = 0;          //set gravity and velocity of the player to 0
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 1;          //when end return to default gravity
            }
            //Jumping
            if (y > 0)
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }
}
