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
    GameObject clearUI;

    [SerializeField]
    Text rightBtnTxt;

    [SerializeField]
    GameObject slideBtn;

    [SerializeField]
    GameObject slamBtn;

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

    public void SetClearUi(bool active)
    {
        clearUI.SetActive(active);
    }

    public void SetInGameUIs(bool active)
    {
        foreach (GameObject go in inGameUis) { go.SetActive(active); }

        //if (active) SetSlideBtn();
    }

    public void SetRightBtnTxt(string str)
    {
        rightBtnTxt.text = str;
    }

    public void SetSlamBtn()
    {
        slideBtn.SetActive(false);
        slamBtn.SetActive(true);        
    }

    public void SetSlideBtn()
    {
        //Debug.Log("SetSlideBtn");

        slideBtn.SetActive(true);
        slamBtn.SetActive(false);
    }
}
