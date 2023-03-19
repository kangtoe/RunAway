using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : ItemBase
{
    [SerializeField]
    float boostTime = 3f;
    [SerializeField]
    float boostAmount = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UseItem(PlayerCharacter user)
    {
        if (GameManager.Instance.DrillDist == DrillDistanceState.close)
        {
            GameManager.Instance.SetDrillDistance(DrillDistanceState.far);
        }

        user.StartBoost(boostTime, boostAmount);
        SoundManager.Instance.PlaySound("item");
        Destroy(gameObject);
    }
}
