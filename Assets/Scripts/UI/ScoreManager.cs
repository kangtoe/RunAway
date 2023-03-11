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
        string str = "";
        if (i < 10)
        {
            str = "0,00" + i.ToString();
        }
        else if (i < 100)
        {
            str = "0,0" + i.ToString();
        }
        else if (i < 1000)
        {
            str = "0," + i.ToString();
        }
        else if (i < 10000)
        {
            str = string.Format("{0:#,###}", i);
        }
        else
        {
            str = "9,999+";
        }
        scoreText.text = str;
    }

    public void AddScore(int amount)
    {
        score += amount;
        SetScoreText(score);
    }
}
