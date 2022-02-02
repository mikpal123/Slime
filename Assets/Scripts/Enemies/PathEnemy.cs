using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEnemy : MonoBehaviour
{   
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]  
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    private Rigidbody2D body;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private Animator anim;

    
    




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initScale = enemy.transform.localScale;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("isAttacking"))
        {
            MoveInDirecrion((int)enemy.localScale.x, 0);
        }
        else
        {          
                if (movingLeft)
                {
                    if (enemy.position.x >= leftEdge.position.x)
                        MoveInDirecrion(-1,speed);
                    else
                        ChangeDirection();
                }
                else
                {
                    if (enemy.position.x <= rightEdge.position.x)
                        MoveInDirecrion(1,speed);
                    else
                        ChangeDirection();
                }            
        }
            
       

    }

    private void ChangeDirection()
    {
        movingLeft = !movingLeft;
    }

    private void MoveInDirecrion(int _direction,float _speed)
    {
        body.velocity = new Vector2((_direction * _speed) , 0);
        enemy.transform.localScale = new Vector3(_direction, enemy.localScale.y, enemy.localScale.z);
    }

}
