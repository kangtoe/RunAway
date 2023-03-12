using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : ItemBase
{
    [SerializeField]
    int addScore = 100;

    public override void UseItem(PlayerCharacter user)
    {
        ScoreManager.Instance.AddScore(addScore);
        Destroy(gameObject);
    }
}
