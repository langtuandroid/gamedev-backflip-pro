using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class ProgressBarManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform player;
    private float initialPlayerZ;
    private float finishLineZ;

    private void Awake()
    {
        LevelManager.OnLevelGenerated += StoreFinishLineZ;
    }

    private void OnDestroy()
    {
        LevelManager.OnLevelGenerated -= StoreFinishLineZ;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.IsGame()) 
            UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        float maxDistance = (finishLineZ - initialPlayerZ);
        float currentDistance = finishLineZ - player.position.z;

        float percent = 1 -Mathf.Clamp01(currentDistance / maxDistance);

        UIManager.updateProgressBarDelegate(percent);
    }

    private void StoreFinishLineZ()
    {
        initialPlayerZ = player.position.z;
        finishLineZ = GameObject.FindWithTag("Finish").transform.position.z;
    }
}
