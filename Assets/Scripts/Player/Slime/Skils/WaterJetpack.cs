using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJetpack : MonoBehaviour
{
    [SerializeReference] private ParticleSystem particle;
    public float jetForce;
    public float defaultJetForce;
    private bool usingJetpack = false;
    private PlayerMovement playerMovement;
    private Rigidbody2D body;

    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
       
        defaultJetForce = jetForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.inWater)
            jetForce = 10;
        else
            jetForce = defaultJetForce;



        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            particle.Play();
            usingJetpack = true;
        }
           
        if (Input.GetKeyUp(KeyCode.Space))
        {
            usingJetpack = false;
            particle.Stop();
        }
            
        if (usingJetpack)
        {
            body.AddForce(Vector2.up * jetForce);           
        }
          
    }
}
