using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : ItemBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UseItem(PlayerCharacter user)
    {
        SoundManager.Instance.PlaySound("item");
        user.GetShield();
        Destroy(gameObject);
    }
}
