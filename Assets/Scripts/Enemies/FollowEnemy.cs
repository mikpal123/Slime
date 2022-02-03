using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;


    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float idleTime;
    [SerializeField] private bool haveRunAnimation;
    [SerializeReference] private float speed;
    [SerializeReference] private float stopDistance;
    [SerializeReference] private Transform target;
    private Rigidbody2D body;
    private Animator anim;

    private bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {   anim = GetComponent<Animator>(); 
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("isAttacking"))
        {
            MoveInDirecrion((int)enemy.localScale.x, 0);
            if (haveRunAnimation)
            { 
                anim.SetBool("run", false); 
            }

        }
        else if(target.position.x >= leftEdge.position.x && target.position.x <= rightEdge.position.x)
        {
            FollowTarget(target);
        }
        else if(enemy.position.x >= leftEdge.position.x && enemy.position.x <= rightEdge.position.x)
        {
            FollowTarget(target);
        }
        else
        {
            StartCoroutine(IdleTimer());
        }
       
    }

    private void FollowTarget( Transform _target)
    {
        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            if (haveRunAnimation)
            {
                anim.SetBool("run", true);
            }
            if (movingLeft)
            {
                if (enemy.position.x >= target.position.x)
                    MoveInDirecrion(-1, speed);
                else
                    ChangeDirection();
            }
            else
            {
                if (this.transform.position.x <= target.position.x)
                    MoveInDirecrion(1, speed);
                else
                    ChangeDirection();
            }
        }
        else
            {
                StartCoroutine(IdleTimer());
            }
    }

    private void ChangeDirection()
    {
        movingLeft = !movingLeft;
    }

    private void MoveInDirecrion(int _direction, float _speed)
    {
        body.velocity = new Vector2((_direction * _speed), 0);
        enemy.transform.localScale = new Vector3(_direction, enemy.localScale.y, enemy.localScale.z);
    }

    IEnumerator IdleTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(idleTime);    //start timer
        if (haveRunAnimation) 
        { 
            anim.SetBool("run", false); 
        }
        body.velocity = new Vector2(0, 0);
        yield return wait;
        
    }
}
