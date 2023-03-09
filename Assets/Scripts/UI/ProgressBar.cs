using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;

    [SerializeField]
    Transform drill_icon;
    [SerializeField]
    Transform player_icon;

    float startProgress = 0;
    float endProgress = 100;
    float playerStartProgress = 5f; //5
    float playerProgressPerSec = 0.5f;
    float DrillProgressPerSec = 0.25f;

    [SerializeField]
    float drillProgress = 0;
    [SerializeField]
    float playerProgress = 0;

    GameManager GameManager => GameManager.Instance;

    public bool isFullProgress => playerProgress >= endProgress;

    // Start is called before the first frame update
    void Start()
    {
        playerProgress = playerStartProgress;
    }

    void Update()
    {
        if (!GameManager.IsPlaying && !GameManager.IsInCutScene) return;

        
        if (playerProgress < endProgress)
        {
            playerProgress += playerProgressPerSec * Time.deltaTime;
        }

        if (playerProgress > endProgress) playerProgress = endProgress;

        Vector3 playerPos = Vector3.Lerp(startPoint.position, endPoint.position, playerProgress / endProgress);
        player_icon.position = playerPos;


        if (drillProgress < endProgress)
        {
            drillProgress += DrillProgressPerSec * Time.deltaTime;
        }
        
        if (drillProgress > endProgress)
        {
            drillProgress = endProgress;
        } 
        

        Vector3 drillPos = Vector3.Lerp(startPoint.position, endPoint.position, drillProgress / endProgress);
        drill_icon.position = drillPos;
    }
}
