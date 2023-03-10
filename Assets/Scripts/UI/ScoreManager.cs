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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetScoreText(int i)
    {
        string str = i.ToString();

        // 천의 자리까지 비어있는 경우 0으로 채우기
        // 9999 점 이상은 9999+ 로 표기
        // 3번째 자리 마다 구분자 넣기

        scoreText.text = str;
    }

    public void AddScore(int amount)
    {
        score += amount;
        SetScoreText(score);
    }
}
