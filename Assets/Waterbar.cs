using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waterbar : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private PlayerEating eating;
    [SerializeField] private Image totalWaterBar;
    [SerializeField] private Image currentWaterBar;

    // Start is called before the first frame update
    void Start()
    {
        totalWaterBar.fillAmount =1; //setup max health
    }

    // Update is called once per frame
    void Update()
    {
        currentWaterBar.fillAmount = eating.waterOwned / 10; //change health in game
    }
}
