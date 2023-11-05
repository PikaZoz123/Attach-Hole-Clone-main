using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float disappearTimer;
    [SerializeField] float upwardsSpeed;
    [SerializeField] float fadeAwaySpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] private TextMeshPro textComp;
    Camera mainCam;
    Color textColor;
    float defaultDisapperTimer;

    void Start()
    {
        textColor = textComp.color;
        defaultDisapperTimer = disappearTimer;
        mainCam = Camera.main;
    }

    public void SpawnText(Vector3 position, string val)
    {
        UpdateText(val);
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void UpdateText(string v)
    {
        textComp.text = v;
    }

    void Update()
    {
        LookAtCamera();
        disappearTimer -= Time.deltaTime;
        transform.position += transform.up * upwardsSpeed * Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= fadeAwaySpeed * Time.deltaTime;
            textComp.color = textColor;
            if (textColor.a <= 0)
            {
                disappearTimer = defaultDisapperTimer;
                textColor.a = 1;
                textComp.color = textColor;
                gameObject.SetActive(false);

            }
        }
    }

    void LookAtCamera()
    {
        Vector3 lookDir = (mainCam.transform.position - transform.position).normalized;
        Vector3 lookRot = Quaternion.LookRotation(-lookDir, Vector3.up).eulerAngles;
        lookRot.x = lookRot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(lookRot), rotationSpeed * Time.deltaTime);
    }
}
