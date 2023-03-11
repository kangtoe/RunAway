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
        string str = i.ToString("D4"); // 항상 4자리 수로 출력되도록 수정
        if (i >= 10000) // 9,999점을 넘을 경우 처리
        {
            str = "9,999+";
        }
        else
        {
            // 세 번째 자리마다 콤마(,) 구분자 삽입
            str = string.Format("{0:#,###}", i);
        }

        scoreText.text = str;
    }

    public void AddScore(int amount)
    {
        score += amount;
        SetScoreText(score);
    }
}
