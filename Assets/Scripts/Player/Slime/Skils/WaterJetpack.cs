using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJetpack : MonoBehaviour
{
    [SerializeReference] private ParticleSystem particle;       //refereence to particle system of jetpack
    [SerializeField] private float defaultJetForce;             //default jetpack force value 
    [SerializeField] private float jetInWaterForce;             //jetpack force value in watter (it should be higher than default value)


    private PlayerEating playerEating;                          //reference to player eating skill
    private float jetForce;                                     //current jetpack force value
    private bool usingJetpack = false;                          //true if player is using jetpack now
    private PlayerMovement playerMovement;                      //reference for player movement
    private Rigidbody2D body;                                   //reference for Rigidbody component

    
    // Start is called before the first frame update
    void Start()
    {
        //setup references and basic values
        playerEating = GetComponent<PlayerEating>();
        playerMovement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
        jetForce =defaultJetForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.inWater)                         //cheeck if player is in water
            jetForce = jetInWaterForce;                     //if true, change jetpack force value to jetpack force value in water
        else                                                //if false
            jetForce = defaultJetForce;                     //change jetpack force value to default

        if (Input.GetKeyDown(KeyCode.Space) )               //when player press space
        {
            particle.Play();                                //make some paricles
            usingJetpack = true;                            //player is using now jetpack
        }

           
        if (Input.GetKeyUp(KeyCode.Space) || (playerEating.waterOwned <=0 && !playerMovement.inWater) ) //if player relase space or (run out of water and isn't now in watter)
        {                                                                                               //if true
            usingJetpack = false;                                                                       //stop using jetpack
            particle.Stop();                                                                            //and stop making paricles
        }
            
        if (usingJetpack)                                                                               //if player using jetpack
        {
            body.AddForce(Vector2.up * jetForce);                                                       //add some force to lift up player
            if(!playerMovement.inWater)                                                                 //if player isn't in water
            playerEating.waterOwned += -0.01f;                                                          //consume watter
        }
          
    }
}
