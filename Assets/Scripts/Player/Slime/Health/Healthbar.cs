using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //variables available in editor
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    // Start is called before the first frame update
    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10; //setup max health
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth/10; //change health in game
    }

}
