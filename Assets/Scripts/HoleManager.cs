using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoleManager : MonoBehaviour
{
    private float circleCapacity;
    [SerializeField] private float time = 20;
    [SerializeField] private Image circleImg;
    [SerializeField] private Reward reward;
    [SerializeField] private Transform holeGameObj;
    [SerializeField] private TextMeshProUGUI timer_txt;
    [SerializeField] private RectTransform timeDoneBar;

    public event EventHandler onCircleEnlarged;

    [Serializable]
    public class HoleSizeClass
    {
        public Vector3 size;
    }

    HoleSizeClass _holeSizeClass = new HoleSizeClass();
    private float remainTime;

    void Start()
    {
        StartCoroutine(timer(time));

        if (PlayerPrefs.HasKey("size"))
        {
            string GetJson = PlayerPrefs.GetString("size");

            holeGameObj.localScale = JsonUtility.FromJson<HoleSizeClass>(GetJson).size;
        }

    }


    private void ProgressBarCircle(int number)
    {
        circleCapacity = 1f / number;

        circleImg.fillAmount += circleCapacity;

        if (circleImg.fillAmount.Equals(1f))
        {
            Enlarge();
        }
    }

    public void Enlarge()
    {
        holeGameObj.localScale += new Vector3(0.3f, 0f, 0.3f);

        circleImg.fillAmount = 0f;

        _holeSizeClass.size = holeGameObj.localScale;

        string SetToJson = JsonUtility.ToJson(_holeSizeClass);

        PlayerPrefs.SetString("size", SetToJson);

        onCircleEnlarged?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator timer(float time)
    {
        remainTime = time;

        while (remainTime >= 0)
        {
            timer_txt.text = $"{remainTime:00:00}";
            remainTime--;

            if (remainTime <= 0)
            {
                timeDoneBar.DOAnchorPosX(0, 1f).SetEase(Ease.OutBounce);
                yield return new WaitForSecondsRealtime(2f);
                reward.gameObject.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            ProgressBarCircle(20);

            other.gameObject.SetActive(false);


            if (other.name.Contains("Cylinder"))
            {
                PlayerPrefs.SetInt("RegularBullet", PlayerPrefs.GetInt("RegularBullet") + 1);
            }

            if (other.name.Contains("ShotGun"))
            {
                PlayerPrefs.SetInt("ShotgunBullet", PlayerPrefs.GetInt("ShotgunBullet") + 1);

                //print("got the ShotGun");
            }
        }
    }

    public void IncreaseLevelTimeMax(float increaseValue)
    {
        StopAllCoroutines();
        StartCoroutine(timer(remainTime + increaseValue));
    }

    public float GetTime()
    {
        return remainTime;
    }
}
