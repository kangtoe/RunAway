using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    // 싱글톤
    public static AdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdManager>();
            }
            return instance;
        }
    }
    private static AdManager instance;

    [SerializeField] int deathCount = 0;
    int adInterval = 3;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Count()
    {
        deathCount++;
        if (deathCount % adInterval == 0)
        {
            PlayAd();
            deathCount = 0;
        }
    }

    void PlayAd()
    {
        // 구글 애드 실행하는 로직
    }
}
