using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[Serializable]
public struct Booster
{
    public enum BoosterType { biggerHole, fasterHole, moreTime }
    [field: SerializeField] public BoosterType Type { get; private set; }
    [field: SerializeField] public Button BoosterButton { get; private set; }
    [field: SerializeField] public float BoosterMax { get; private set; }
    [field: SerializeField] public float BoosterIncreaseValue { get; private set; }
}

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private Booster[] boostersArray;
    [SerializeField] private HoleManager hole;
    [SerializeField] private NavMeshAgent agent;
    //[SerializeField] private GameObject boosterMenuVisual;
    //[SerializeField] private Hole hole;
    //[SerializeField] private Timer timer;

    private void Awake()
    {
        //BE_SetBoosterMenuVisualActive(true);
        foreach (var booster in boostersArray)
        {
            InitBoosters(booster.Type);
        }
    }

    //private void Start()
    //{
    //    gameStateManager.onGameStateChanged += GameStateManager_onGameStateChanged;
    //}

    //private void OnDisable()
    //{
    //    gameStateManager.onGameStateChanged -= GameStateManager_onGameStateChanged;
    //}

    //private void GameStateManager_onGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    //{
    //    BE_SetBoosterMenuVisualActive(e._gameState == GameStateManager.GameState.MainMenu);
    //}

    private void InitBoosters(Booster.BoosterType type)
    {
        Booster selectedBooster = boostersArray.FirstOrDefault(x => x.Type == type);
        switch (type)
        {
            case Booster.BoosterType.biggerHole:
                selectedBooster.BoosterButton.onClick.AddListener(() =>
                {
                    hole.Enlarge();
                    //BE_SetBoosterMenuVisualActive(false);
                });
                break;
            case Booster.BoosterType.fasterHole:
                selectedBooster.BoosterButton.onClick.AddListener(() =>
                {
                    if (agent.speed < selectedBooster.BoosterMax)
                    {
                        agent.speed += selectedBooster.BoosterIncreaseValue;
                    }
                    //BE_SetBoosterMenuVisualActive(false);
                });
                break;
            case Booster.BoosterType.moreTime:
                selectedBooster.BoosterButton.onClick.AddListener(() =>
                {
                    if (hole.GetTime() < selectedBooster.BoosterMax)
                    {
                        hole.IncreaseLevelTimeMax(selectedBooster.BoosterIncreaseValue);
                    }
                    //BE_SetBoosterMenuVisualActive(false);
                });
                break;
            default:
                break;
        }
    }


}
