using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private HoleManager holeManager;
    [SerializeField] private CinemachineVirtualCamera idleCam;
    [SerializeField] private CinemachineVirtualCamera gameplayCam;
    [SerializeField] private float gameplayCamFOVMax;
    [SerializeField] private float gameCamFOVChangeSpeed;
    [Range(0f, 5f)]
    [SerializeField] private float FOVIncreaseValue;
    //private float gameplayCamFOVMin;

    public event EventHandler onGameCamActivated;



    private void Start()
    {
        //ActivateGameCam();
        //gameplayCamFOVMin = gameplayCam.m_Lens.FieldOfView;
        holeManager.onCircleEnlarged += HoleManager_onCircleEnlarged;
    }

    private void OnDisable()
    {
        holeManager.onCircleEnlarged -= HoleManager_onCircleEnlarged;
    }


    private void HoleManager_onCircleEnlarged(object sender, EventArgs e)
    {
        IncreaseGameCamOffset();
    }



    private void IncreaseGameCamOffset()
    {


        float newFOV = gameplayCam.m_Lens.FieldOfView;

        newFOV += FOVIncreaseValue;
        newFOV = Mathf.Min(newFOV, gameplayCamFOVMax);
        StartCoroutine(COR_SetNewOffset());



        IEnumerator COR_SetNewOffset()
        {
            float t = 0;
            float startFOV = gameplayCam.m_Lens.FieldOfView;

            while (t < 1)
            {
                t += Time.deltaTime * gameCamFOVChangeSpeed;
                gameplayCam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, newFOV, t);

                yield return null;

            }
        }
    }




}
