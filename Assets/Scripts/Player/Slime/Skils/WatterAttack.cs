using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatterAttack : MonoBehaviour
{   //variables available in editor
    [SerializeField] private float speed;                           //projectile moevement speed

    //variables unavailable in editor
    private float direction;                                        //direction of projectile
    private bool hit;                                               //if true, projecile hit something
    private float lifetime;                                         //lifetime of the projectile, if it runs out projectile will be dissable
    private Animator anim;                                          //Anim reference
    private BoxCollider2D boxCollider;                              //box collider reference
    

    private void Awake()
    {
        //settup referencee
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;                                            //end when proejctile hit something
        //making projectile move
        float movementSpeed = speed * Time.deltaTime * direction;   //setup movemement speed of projectile
        transform.Translate(movementSpeed, 0, 0);                   //movig projectile

        //change projectile lifetime
        lifetime += Time.deltaTime;                                 //counting down projectile lifetime
        if (lifetime > 5) gameObject.SetActive(false);              //disable projectile when time count to >5
    }

    //When projectile hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;                                                 //set up that projectile hit something
        boxCollider.enabled = false;                                //disable coilison with that projectile
        anim.SetTrigger("explode");                                 //start explode animation

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    //Setting direction of a projectile
    public void SetDirection(float _direction)
    {   
        //setup basic variables for projectile
        lifetime = 0;                                             //reset life time timer
        direction = _direction;                                   //set direction
        gameObject.SetActive(true);                               //active projectile
        hit = false;                                              //reset hit bool
        boxCollider.enabled = true;                               //enable colision

        float localScaleX = transform.localScale.x;               //setup scale 
        if (Mathf.Sign(localScaleX) != _direction)                //check in what direction projectile should be facing
            localScaleX = -localScaleX;                           //left/right

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); //and face projectile in that direction
    }

    //Deactivate projectile
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
