using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private LayerManager layerManager;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI textValue;
    private int ammoGameObjectsTotalCount;
    private int ammoGameObjectsCount;
    private float progressNormalized;



    private void Start()
    {
        ammoGameObjectsTotalCount = GameObject.FindGameObjectsWithTag("ammo").Length;
        layerManager.onObjectSwallowed += LayerManager_onObjectSwallowed;

    }
    private void OnDisable()
    {
        layerManager.onObjectSwallowed -= LayerManager_onObjectSwallowed;
    }

    private void LayerManager_onObjectSwallowed(object sender, GameObject e)
    {
        ammoGameObjectsCount++;
        ammoGameObjectsCount = Mathf.Min(ammoGameObjectsTotalCount, ammoGameObjectsCount);
        progressNormalized = (float)ammoGameObjectsCount / ammoGameObjectsTotalCount;
        progressSlider.value = progressNormalized;
        textValue.text = $"{(int)(progressNormalized * 100)}%";


    }
}
