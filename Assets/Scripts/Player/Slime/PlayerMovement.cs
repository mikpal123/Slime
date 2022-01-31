using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private float movementSpeed;       //player movement speed basic value
    [SerializeField] private float jumpHeight;          //player jump height basic value   
    [SerializeReference] private GameObject water;      //water reference
    [SerializeField] private LayerMask groundMask;      //reference to every layer that is consider as ground
    [SerializeField] private LayerMask wallMask;        //reference to every layer that is consider as wall

    //variables unavailable in editor
    private BoxCollider2D boxColider;                   //reference to box colider
    private Rigidbody2D body;                           //reference to rigid body
    private Vector3 moveDelta;                          //with direction player should be facing
    private Animator anim;                              //reference to anim
    public bool inWater;                                //if true player is in water
    private float basicMovement;                        //conteiner for basic movement
    private float wallJumpCooldown;                     //CD for wall climbing
    private float x;                                    //value of 'a', 'd' input
    private float y;                                    //valie of 'w', 's' input

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
        x = Input.GetAxisRaw("Horizontal");             //bind x to 'a', 'd' input
        y = Input.GetAxisRaw("Jump");                   //bind y to 'w', 's' input

        //checking movement multiplayer
        basicMovement = water.GetComponent<Water>().BasicMovement();

        //chcecking if player is in water
        inWater = water.GetComponent<Water>().IsInWater();


        //Reset MoveDelta
        moveDelta = new Vector3(x, y, 0);

        //Swap sprite direction, wether youre going right or left
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);  //move right
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f); //move left

        //Move left or right
        body.velocity = new Vector2((x * movementSpeed)/basicMovement, body.velocity.y/ basicMovement);

        
        

        //Animator parameters
        anim.SetBool("run",x!=0);                   //setup value for run, if x!=0 start run anim
        anim.SetBool("grounded", IsGrounded());     //setup value for run, if x!=0 start jump anim

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

    
    //chceck if target can attack
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
   
    //check if player is on the ground
    public bool IsGrounded()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, Vector2.down, 0.01f, groundMask); //raycast to check if there are near ground
        return Hit.collider != null;                                                                                                //if true that means player is on ground
    }

    //check if player is near wall
    public bool IsOnWall()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.01f, wallMask); //raycast to check if there are near wall
        return Hit.collider != null;                                                                                                                       //if true that means player is near wall
    }


    //Wall jump
    private void WallJump()   
    {
        
        if (wallJumpCooldown < 0.1f)  //check cooldown
        {
            body.velocity = new Vector2((x * movementSpeed) / basicMovement, body.velocity.y / basicMovement); //change player velocity

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
            wallJumpCooldown += Time.deltaTime; //start tiemr
        }
    }

    //check if plaayer can use eating
    public bool CanEat()
    {
        if (!IsOnWall())        
            return true;       
        else
            return false;
    }


}
