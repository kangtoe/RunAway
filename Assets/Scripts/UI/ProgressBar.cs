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
    float playerStartProgress = 5f;
    float playerProgressPerSec = 0.5f;
    float DrillProgressPerSec = 0.25f;

    [SerializeField]
    float drillProgress = 0;
    [SerializeField]
    float playerProgress = 0;

    GameManager GameManager => GameManager.Instance;

    // Start is called before the first frame update
    void Start()
    {
        playerProgress = playerStartProgress;
    }

    void Update()
    {
        if (!GameManager.IsPlaying && !GameManager.IsInCutScene) return;

        
        if (playerProgress < 100)
        {
            playerProgress += playerProgressPerSec * Time.deltaTime;
        }

        if (playerProgress > 100) playerProgress = 100;

        Vector3 playerPos = Vector3.Lerp(startPoint.position, endPoint.position, playerProgress / endProgress);
        player_icon.position = playerPos;


        if (drillProgress < 100)
        {
            drillProgress += DrillProgressPerSec * Time.deltaTime;
        }

        if (drillProgress > 100) drillProgress = 100;

        Vector3 drillPos = Vector3.Lerp(startPoint.position, endPoint.position, drillProgress / endProgress);
        drill_icon.position = drillPos;
    }
}
