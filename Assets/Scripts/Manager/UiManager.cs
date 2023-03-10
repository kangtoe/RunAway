using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ui 이동, 활성화 제어 기능만을 제공하는 헬퍼 클래스
public class UiManager : MonoBehaviour
{
    //싱글톤
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }
            return instance;
        }
    }
    private static UiManager instance;

    [SerializeField]
    GameObject startUI;

    [SerializeField]
    GameObject overUI;

    [SerializeField]
    GameObject puaseUI;

    [SerializeField]
    Text rightBtnTxt;

    [SerializeField]
    GameObject[] inGameUis;

    public void SetStartUi(bool active)
    {
        startUI.SetActive(active);
    }

    public void SetOverUi(bool active)
    {
        overUI.SetActive(active);
    }

    public void SetPauseUi(bool active)
    {
        puaseUI.SetActive(active);
    }

    public void SetInGameUIs(bool active)
    {
        foreach (GameObject go in inGameUis) { go.SetActive(active); }
    }

    public void SetRightBtnTxt(string str)
    {
        rightBtnTxt.text = str;
    }
}
