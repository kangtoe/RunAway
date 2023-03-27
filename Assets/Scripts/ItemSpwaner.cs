using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpwaner : MonoBehaviour
{
    [SerializeField]
    bool SpwanItemCheck = true;

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
        if (!SpwanItemCheck) return null;

        GameObject go;

        if (SpwanedItemCount != 0 && SpwanedItemCount % 12 == 0)
        {
            go = Instantiate(ShieldItem, tf);
        }
        else if (SpwanedItemCount != 0 &&  SpwanedItemCount % 6 == 0)
        {
            go = Instantiate(BoostItem, tf);
        }
        else
        {
            go = Instantiate(ScoreItem, tf);
        }

        go.transform.localPosition = Vector3.zero;
        SpwanedItemCount++;

        return go;
    }
}
