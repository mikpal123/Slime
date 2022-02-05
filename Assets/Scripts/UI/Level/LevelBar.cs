using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    [SerializeReference] LevelSystem levelSystem;
    [SerializeReference] private Text currentLevel;
    [SerializeReference] private Image currentExp;

    private void Awake()
    {
        currentLevel = transform.Find("CurrentLevel").GetComponent<Text>();
        currentExp = transform.Find("LevelBarCurrent").GetComponent<Image>();
    }

    private void Update()
    {
        currentExp.fillAmount = levelSystem.GetCurrentExp();
        currentLevel.text = levelSystem.GetCurrentLevel().ToString();
    }
}
