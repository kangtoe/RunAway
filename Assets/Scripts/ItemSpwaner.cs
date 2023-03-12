using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpwaner : MonoBehaviour
{
    [SerializeField]
    GameObject ScoreItem;

    [SerializeField]
    GameObject BoostItem;

    [SerializeField]
    GameObject ShieldItem;

    [SerializeField]
    int SpwanedItemCount = 0;

    public GameObject SpwanItem(Transform tf)
    {
        SpwanedItemCount++;

        GameObject go;

        if (SpwanedItemCount % 12 == 0)
        {
            go = Instantiate(ShieldItem, tf);
        }
        else if (SpwanedItemCount % 6 == 0)
        {
            go = Instantiate(BoostItem, tf);
        }
        else
        {
            go = Instantiate(ScoreItem, tf);
        }

        go.transform.localPosition = Vector3.zero;
        return go;
    }
}
