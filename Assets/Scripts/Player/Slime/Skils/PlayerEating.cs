using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEating : MonoBehaviour
{

    [SerializeReference] private GameObject EatingObject;

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovement.CanEat())
        {
            EatingObject.SetActive(true);
            Eating();
        }
        if (Input.GetKeyUp(KeyCode.E) || !playerMovement.CanEat())
        {
            EatingObject.SetActive(false);
        }
    }

    private void Eating()
    {
        if (playerMovement.inWater)
        {
            EatingWatter();
        }
    }
    private void EatingWatter()
    {
        
    }
}
