using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private RectTransform coinUIReward;
    [SerializeField] private RectTransform totalUIReward;
    [SerializeField] private int minRandomReward;
    [SerializeField] private int maxRandomReward;
    [SerializeField] private float animDuration;
    private int currentRandomReward;

    private void OnEnable()
    {
        currentRandomReward = Random.Range(minRandomReward, maxRandomReward + 1);
        rewardText.text = currentRandomReward.ToString();
        totalText.text = $"{currentRandomReward + ScoreLogic.Instance.Score}";

        AnimateTexts();
    }

    private void AnimateTexts()
    {
        Vector3 coinUIScale = coinUIReward.localScale;
        Vector3 totalUIScale = totalUIReward.localScale;

        coinUIReward.localScale = Vector3.zero;
        totalUIReward.localScale = Vector3.zero;
        coinUIReward.DOScale(coinUIScale, animDuration).From(0).OnComplete(() =>
        {
            totalUIReward.DOScale(totalUIScale, animDuration).From(0);
        });

    }

    public void BE_Claim()
    {
        ScoreLogic.Instance.IncreaseScore(currentRandomReward);
    }

    public void BE_LoadNextScene()
    {
        SceneManager.LoadScene("BossFight_Level");
    }
}
