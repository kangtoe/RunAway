using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }
            return instance;
        }
    }
    private static ScoreManager instance;

    [SerializeField]
    float score = 0;

    [SerializeField]
    float scorePerSec = 1;

    [SerializeField]
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText(score);
        StartCoroutine(GetScorePerSec(scorePerSec));
    }

    // Update is called once per frame
    void Update()
    {
        //score += scorePerSec * Time.deltaTime;
        //SetScoreText(score);
    }

    void SetScoreText(float f)
    {
        // 숫자를 문자열로 변환
        string str = ((int)f).ToString();

        // 숫자 자릿수가 1~3자리면 앞에 '0,00' ~ '0,' 를 붙여주기
        if (f < 1000)
        {
            str = "0," + str.PadLeft(3, '0');
        }
        // 숫자 자릿수가 4자리면 ',' 구분자 삽입
        else if (f < 10000)
        {
            str = string.Format("{0:#,###}", f);
        }
        // 숫자 자릿수가 5자리 이상이면 '9,999+' 로 출력
        else
        {
            str = "9,999+";
        }

        // 점수 표시 업데이트
        scoreText.text = str;
    }

    public void AddScore(int amount)
    {
        score += amount;
        SetScoreText(score);
    }

    IEnumerator GetScorePerSec(float scorePerSec)
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / GameManager.Instance.GameSpeed);
            if (!GameManager.Instance.IsPlaying) continue;

            score += scorePerSec;
            SetScoreText(score);
        }
    }
}
