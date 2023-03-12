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
    Transform total_icon;
    [SerializeField]
    Transform drill_icon;
    [SerializeField]
    Transform player_icon;

    float startProgress = 0;
    float endProgress = 100;
    float playerStartProgress = 5f; // 기본값 : 5?
    float playerProgressPerSec = 0.5f;
    public float DrillProgressPerSec = 0.5f;

    Animator playerIconAnim;

    [SerializeField]
    float drillProgress = 0;
    [SerializeField]
    float playerProgress = 0;

    [Header("플레이어 진행 중지 기간")] // 이 값이 0보다 크다면, 플레이어는 이동 중이 아님
    [SerializeField]
    float holdPlayerTimeLeft = 0;

    GameManager GameManager => GameManager.Instance;

    public bool isFullProgress => playerProgress >= endProgress;

    // Start is called before the first frame update
    void Start()
    {
        playerProgress = playerStartProgress;
        playerIconAnim = player_icon.GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.IsPlaying && !GameManager.IsInCutScene) return;

        // 플레이어
        {
            if (holdPlayerTimeLeft > 0)
            {
                holdPlayerTimeLeft -= Time.deltaTime;
                // 플레이어 아이콘 애니메이션 대기
                playerIconAnim.enabled = false;
            }
            else
            {
                holdPlayerTimeLeft = 0;
                playerIconAnim.enabled = true;
            }

            if (holdPlayerTimeLeft == 0)
            {
                if (playerProgress < endProgress)
                {
                    playerProgress += playerProgressPerSec * Time.deltaTime;
                }

                if (playerProgress > endProgress) playerProgress = endProgress;

                Vector3 playerPos = Vector3.Lerp(startPoint.position, endPoint.position, playerProgress / endProgress);
                player_icon.position = playerPos;
            }
        }

        // 드릴
        {
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

    public void SetHoldTime(float f)
    {
        holdPlayerTimeLeft = f;
    }
}
