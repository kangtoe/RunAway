using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{ 
    ready = 0,
    cutScene,
    play,
    pause,
    over,
    clear
}

// 게임 전체 흐름 관리
// 1. 게임의 상태 (시작 전, 플레이 중, 일시정지 등) 관리
// 2. 게임의 속도 관리
public class GameManager : MonoBehaviour
{    
    //싱글톤
    public static GameManager Instance
    {
        get
        {            
            if (instance == null)
            {                
                instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return instance;
        }
    }
    private static GameManager instance;

    Drill drill;
    float drillMove = 7f;

    [SerializeField]
    float gameSpeed = 1f; // 시작 시 1
    public float GameSpeed => gameSpeed;

    [SerializeField]
    GameState state = GameState.ready;

    UiManager UiManager => UiManager.Instance;

    public bool IsPlaying => state == GameState.play ? true : false;    
    
    void Start()
    {
        UiManager.SetPauseUi(false);
        UiManager.SetInGameUIs(false);
        UiManager.SetStartUi(true);                

        Time.timeScale = gameSpeed;

        drill = FindObjectOfType<Drill>();
    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = gameSpeed;
    }  

    public void StartGame()
    {
        state = GameState.play;        
    }

    public void TogglePause()
    {
        if (state == GameState.play)
        {            
            state = GameState.pause;
            UiManager.SetPauseUi(true);
            Time.timeScale = 0;
        }
        else if (state == GameState.pause)
        {
            state = GameState.play;
            UiManager.SetPauseUi(false);
            Time.timeScale = gameSpeed;
        }
        else
        {
            Debug.Log("cannot toggle pause! state :" + state);
        }

        return;
    }

    public void GameOver()
    {
        state = GameState.over;
        UiManager.SetOverUi(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetGameSpeed(float f)
    {
        gameSpeed = f;
    }

    #region 컷씬 연출

    public void PlayStartCutScene()
    {
        if (state != GameState.ready)
        {
            Debug.Log("Play StartCutScene Only ready state!");
            return;
        }

        UiManager.SetInGameUIs(true);
        UiManager.SetStartUi(false);

        float duration = 3f;
        state = GameState.cutScene;
        StartCoroutine(StartCutSceneCr(duration));
    }

    IEnumerator StartCutSceneCr(float duration)
    {
        SetGameSpeed(2);        

        float t = 0;
        drill.moveDrill = false;
        while (true)
        {
            t += Time.fixedDeltaTime / duration;
            if (t > 1) t = 1;
            if (t == 1) break;

            drill.originPos -= Vector3.right * Time.fixedDeltaTime / duration * drillMove;            

            yield return new WaitForFixedUpdate();
        }

        //yield return new WaitForSeconds(duration);
        SetGameSpeed(1);

        StartGame();
    }

    public void PlayOverCutScene()
    {

    }

    public void PlayClearCutScene()
    {

    }

    #endregion 컷씬 연출
}
