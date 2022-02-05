using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public int exp;
    private int expToNextLevel;

    public LevelSystem()
    {
        level = 0;
        exp = 0;
        expToNextLevel = 100;
    }

    public void AddExperience(int _ammount)
    {
        exp += _ammount;
        if(exp >= expToNextLevel)
        {
            level++;
            exp -= expToNextLevel;
        }
    }

    public float GetCurrentExp()
    {
        return (float)exp/expToNextLevel;
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
