using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    [SerializeField] private LayerManager layerManager;
    [SerializeField] private HoleManager holeManager;
    [SerializeField] private float spawnValueDuration;
    private FloatingText[] textPrefabs;
    private int currentTextIndex;

    void Start()
    {
        textPrefabs = GetComponentsInChildren<FloatingText>(true);
        holeManager.onCircleEnlarged += HoleManager_onCircleEnlarged;
        layerManager.onObjectSwallowed += LayerManager_onObjectSwallowed;
    }

    private void LayerManager_onObjectSwallowed(object sender, GameObject e)
    {
        SpawnText(layerManager.transform.position, "+1");
        ScoreLogic.Instance.IncreaseScore(1);

    }

    private void HoleManager_onCircleEnlarged(object sender, EventArgs e)
    {
        StartCoroutine(COR_SizeUpText());
        IEnumerator COR_SizeUpText()
        {
            yield return new WaitForSeconds(.25f);
            SpawnText(layerManager.transform.position, "Size Up!");
        }
    }



    private void OnDisable()
    {
        holeManager.onCircleEnlarged -= HoleManager_onCircleEnlarged;
        layerManager.onObjectSwallowed -= LayerManager_onObjectSwallowed;

    }


    public void SpawnText(Vector3 position, string val)
    {
        textPrefabs[currentTextIndex].SpawnText(position, val);
        currentTextIndex++;
        if (currentTextIndex >= textPrefabs.Length)
            currentTextIndex = 0;
    }



}
