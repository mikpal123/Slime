using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEating : MonoBehaviour
{

    [SerializeReference] private GameObject EatingObject;
    private Water water;
    private PlayerMovement playerMovement;
    private Health health;
    private BoxCollider2D box;
    private string whatHit;
    public float waterOwned;
    public bool isEating { get; private set; }

    // Start is called before the first frame update
    void Start()
    {   
        water = FindObjectOfType<Water>();
        box = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        isEating = false;
        health = GetComponent<Health>();
        waterOwned = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovement.CanEat() && !health.dead)
        { 
            isEating = true;
            EatingObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E) || !playerMovement.CanEat())
        {
            isEating=false;
            EatingObject.SetActive(false);
        }
        if (isEating)
        {
            Eating();
        }
    }

    private void Eating()
    {
        if (water.canGaingWater)
        {
            EatingWatter();
        }
    }
    private void EatingWatter()
    {   
        if(waterOwned <=10)
        waterOwned += 0.01f;
    }

}
