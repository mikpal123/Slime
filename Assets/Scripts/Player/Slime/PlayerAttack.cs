using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //variables available in editor
    [SerializeReference] private float attackCooldown;
    [SerializeReference] private Transform firePoint;
    [SerializeReference] private GameObject[] WaterAttack;

    //variables unavailable in editor
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;


    //Do every time when script instance is being loaded
    private void Awake()
    {   //take references from object
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;    
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        WaterAttack[FindWaterAttacks()].transform.position = firePoint.position;
        WaterAttack[FindWaterAttacks()].GetComponent<WatterAttack>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

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
