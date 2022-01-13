using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatterAttack : MonoBehaviour
{   //variables available in editor
    [SerializeField] private float speed;

    //variables unavailable in editor
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    

    private void Awake()
    {
        //take references from object
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        //making projectile move
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        //change projectile lifetime
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    //When projectile hit something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    //Setting direction of a projectile
    public void SetDirection(float _direction)
    {   
        //setup basic variables for projectile
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) //check in what direction projectile should be facing
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Deactivate projectile
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
