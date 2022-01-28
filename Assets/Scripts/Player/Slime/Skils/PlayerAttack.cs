using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //variables available in editor
    [SerializeReference] private float attackCooldown;
    [SerializeReference] private Transform firePoint;
    [SerializeReference] private GameObject[] WaterAttack;

    //variables unavailable in editor
    private Animator anim;
    private Health health;
    private PlayerMovement playerMovement;
    private PlayerEating playerEating;
    private float cooldownTimer = Mathf.Infinity;


    //Do every time when script instance is being loaded
    private void Awake()
    {   //take references from object
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
        playerEating = GetComponent<PlayerEating>();
    }
    

    // Update is called once per frame
    void Update()
    {
        //chceck if player can attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack() && !playerEating.isEating && !health.dead && playerEating.waterOwned > 0)
            Attack();
        
        cooldownTimer += Time.deltaTime;    
    }

    private void Attack()
    {
        playerEating.waterOwned += -0.1f;
        anim.SetTrigger("attack");//start anim
        cooldownTimer = 0;//reset cd

        //taking free prefab and making transform
        WaterAttack[FindWaterAttacks()].transform.position = firePoint.position; 
        WaterAttack[FindWaterAttacks()].GetComponent<WatterAttack>().SetDirection(Mathf.Sign(transform.localScale.x));
       
    }

    //search for a free prefab to eject
    private int FindWaterAttacks()
    {
        for (int i = 0; i < WaterAttack.Length; i++)
        {
            if (!WaterAttack[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
