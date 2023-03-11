using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // ?떛湲??넠
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
    int score = 0;

    [SerializeField]
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetScoreText(int i)
    {
        // 숫자를 문자열로 변환
        string str = i.ToString();

        // 숫자 자릿수가 1~3자리면 앞에 '0,00' ~ '0,' 를 붙여주기
        if (i < 1000)
        {
            str = "0," + str.PadLeft(3, '0');
        }
        // 숫자 자릿수가 4자리면 ',' 구분자 삽입
        else if (i < 10000)
        {
            str = string.Format("{0:#,}", i);
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
}
