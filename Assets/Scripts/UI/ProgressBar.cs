using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    RectTransform startPoint;
    [SerializeField]
    RectTransform endPoint;

    [SerializeField]
    RectTransform total_icon;
    [SerializeField]
    RectTransform drill_icon;
    [SerializeField]
    RectTransform player_icon;
    
    [SerializeField][Range(0, 1)]
    float curr_Progress = 0f; // 0 ~ 1

    [Header("플레이어 진행 중지 기간")] // 이 값이 0보다 크다면, 플레이어는 이동 중이 아님
    [SerializeField]
    float holdPlayerTimeLeft = 0;

    float progressPerSec = 0.005f;
    
    Animator playerIconAnim;

    float farDist = 20;
    float closeDist = 12;
    float impactDist = 0;

    public bool isFullProgress => curr_Progress >= 1;

    GameManager GameManager => GameManager.Instance;
    

    // Start is called before the first frame update
    void Start()
    {
        playerIconAnim = player_icon.GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.IsPlaying) return;

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
                if (curr_Progress < 1)
                {
                    curr_Progress += progressPerSec * Time.deltaTime;
                }

                if (curr_Progress > 1) curr_Progress = 1;

                Vector3 iconPos = Vector3.Lerp(startPoint.anchoredPosition, endPoint.anchoredPosition, curr_Progress);
                total_icon.anchoredPosition = iconPos;
            }
        }
    }

    public void MoveDrillIconSmooth(DrillDistanceState state, float duration = 1)
    {
        float dist = 0;
        if (state == DrillDistanceState.far) dist = farDist;
        else if (state == DrillDistanceState.close) dist = closeDist;
        else if (state == DrillDistanceState.impact) dist = impactDist;
        else Debug.Log("undefined state!");

        StopAllCoroutines();
        StartCoroutine(MoveDrillIconSmoothCr(dist, duration));
    }

    IEnumerator MoveDrillIconSmoothCr(float dist, float duration)
    {
        float t = 0;

        Vector2 startPos = drill_icon.anchoredPosition;
        Vector2 targetPos = player_icon.anchoredPosition - Vector2.right * dist;

        while (t <= 1)
        {
            t += Time.deltaTime / duration;
            if (t > 1) t = 1;

            Vector2 iconPos = Vector3.Lerp(startPos, targetPos, t);
            drill_icon.anchoredPosition = iconPos;

            yield return null;
        }    
    }

    public void SetHoldTime(float f)
    {
        holdPlayerTimeLeft = f;
    }
}
