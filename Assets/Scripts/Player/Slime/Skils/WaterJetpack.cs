using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJetpack : MonoBehaviour
{
    [SerializeReference] private ParticleSystem particle;
    [SerializeField] private float defaultJetForce;
    [SerializeField] private float jetWaterForce;
    private Water water;
    private PlayerEating playerEating;
    private float jetForce;
    private Health health;
    private bool usingJetpack = false;
    private PlayerMovement playerMovement;
    private Rigidbody2D body;

    
    // Start is called before the first frame update
    void Start()
    {
        water = FindObjectOfType<Water>();
        playerEating = GetComponent<PlayerEating>();
        playerMovement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        jetForce =defaultJetForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.inWater)
            jetForce = jetWaterForce;
        else
            jetForce = defaultJetForce;

        if (Input.GetKeyDown(KeyCode.Space) && !health.dead)
        {
            particle.Play();
            usingJetpack = true;
        }

           
        if (Input.GetKeyUp(KeyCode.Space) || (playerEating.waterOwned <=0 && !playerMovement.inWater) )
        {
            usingJetpack = false;
            particle.Stop();
        }
            
        if (usingJetpack)
        {
            body.AddForce(Vector2.up * jetForce);
            if(!playerMovement.inWater)
            playerEating.waterOwned += -0.01f;
        }
          
    }
}
