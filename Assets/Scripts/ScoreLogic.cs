using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;
    private int multiplier = 1;

    public static ScoreLogic Instance { get; private set; }
    public static double CoinsGathered { get; set; }

    public double Score { get; set; }


    void Awake()
    {
        Instance = this;
        UpdateScore();
    }


    public void IncreaseScore(double value, bool multiply = false)
    {
        value *= (multiply ? multiplier : 1);
        CoinsGathered += value;
        Score += value;

        //        Debug.Log("coings: " + CoinsGathered);

        UpdateScore();

        //GoToUIEffect();
        //SaveCoinsGathered();
    }

    public void SetMultiplier(int multi)
    {
        multiplier = multi;
    }

    public int GetMultiplier()
    {
        return multiplier;
    }


    //Vector3(-5.7300415,5.61999512,-3.79999995) Camera

    public void UpdateScore()
    {
        if (Score >= 1000 && Score < 1000000)
            textComp.text = $"${((Score) / 1000):0.00}K";

        else if (Score >= 1000000 && Score < 1000000000)
            textComp.text = $"${((Score) / 1000000):0.00}M";

        else if (Score >= 1000000000 && Score < 1000000000000)
            textComp.text = $"${((Score) / 1000000000):0.00}B";

        else if (Score >= 1000000000000 && Score < 1000000000000000)
            textComp.text = $"${((Score) / 1000000000000):0.00}T";

        else if (Score >= 1000000000000000)
            textComp.text = $"${((Score) / 1000000000000000):0.00}Q";

        else
            textComp.text = $"${Score: 0}";
    }

    //public bool SpendCoins(double amount)
    //{
    //    if ((Score - amount) >= 0)
    //    {
    //        Score -= amount;
    //        SaveAndLoadManager.SaveScore(Score);
    //        UpdateScore();
    //        return true;
    //    }
    //    else
    //    {
    //        //SFX
    //        return false;
    //    }
    //}

    //void OnDisable()
    //{
    //    SaveCoinsGathered();
    //}

    //void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        Debug.Log("Paaaused: ");
    //        SaveCoinsGathered();

    //    }
    //}


}
