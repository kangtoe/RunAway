using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum DrillDistanceState
{ 
    far,
    close,
    impact
}

public enum GameState
{ 
    ready = 0,
    playWait,
    play,
    pause,
    over,
    preClearWait, // 프로그래스 바 100% -> 다음 그라운드 패턴 = 피니시 그라운드 출현
    clearWait, // 클리어 포인트 도달
    clear
}

// 게임 전체 흐름(진행 상태) 제어
public class GameManager : MonoBehaviour
{    
    // 싱글톤
    public static GameManager Instance
    {
        get
        {            
            if (instance == null)
            {                
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private static GameManager instance;

    RepeatGround repeatGround;
    ProgressBar progressBar;
    
    float startGameSpeed = 1;
    [SerializeField]
    float gameSpeed = 1f; // 기본값 1
    float maxGameSpeed = 5f;
    public float GameSpeed => gameSpeed;    

    [SerializeField]
    GameState state = GameState.ready;

    Drill drill;
    float drillMove = 7f;
    [SerializeField]
    DrillDistanceState drillDist = DrillDistanceState.far;
    public DrillDistanceState DrillDist => drillDist;

    UiManager UiManager => UiManager.Instance;
    SoundManager SoundManager => SoundManager.Instance;

    public bool IsPlaying => state == GameState.play ? true : false;
    public bool IsInCutScene => state == GameState.playWait ? true : false;
    public bool IsInPreClearWait => state == GameState.preClearWait ? true : false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {        
        UiManager.SetPauseUi(false);
        UiManager.SetInGameUIs(false);
        UiManager.SetStartUi(true);                

        drill = FindObjectOfType<Drill>();
        progressBar = FindObjectOfType<ProgressBar>();
        repeatGround = FindObjectOfType<RepeatGround>();

        gameSpeed = startGameSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (state == GameState.play)
        {            
            if(gameSpeed < maxGameSpeed) gameSpeed += Time.deltaTime * 0.01f;
            else if(gameSpeed > maxGameSpeed)gameSpeed = maxGameSpeed;    
            
            if(progressBar.isFullProgress)
            {
                Debug.Log("state : preClearWait");                      
                state = GameState.preClearWait;
            }            
        }                
    }

    public void StartGame()
    {              
        UiManager.SetInGameUIs(true);
        state = GameState.play;              
    }

    public void TogglePause()
    {
        if (state == GameState.play)
        {
            SoundManager.PlaySound("pause");
            state = GameState.pause;
            UiManager.SetPauseUi(true);
            Time.timeScale = 0;
        }
        else if (state == GameState.pause)
        {
            SoundManager.PlaySound("resume");
            state = GameState.play;
            UiManager.SetPauseUi(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("cannot toggle pause! state :" + state);
        }

        return;
    }

    public void HoldPlayerProgress(float duration)
    {
        progressBar.SetHoldTime(duration);
    }

    public void GameOver()
    {
        state = GameState.over;
        SetGameSpeed(0);
        UiManager.SetOverUi(true);
    }

    void GameClear()
    {
        //Debug.Log("GameClear");
        SetGameSpeed(0);
        UiManager.SetClearUi(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetGameSpeed(float f)
    {
        //Debug.Log("SetGameSpeed : " + f);

        if (f > maxGameSpeed) Debug.Log("set speed > maxGameSpeed! : " + f);
        //if (f < startGameSpeed) Debug.Log("set speed < startGameSpeed! : " + f);        

        gameSpeed = f;        
    }

    

    #region 컷신연출

    float drillMoveDuration = 3f;


    public void PlayStartCutScene()
    {
        if (state != GameState.ready)
        {
            Debug.Log("can play StartCutScene On ready state!");
            return;
        }

        SoundManager.PlaySound("start");
        UiManager.SetStartUi(false);
        
        state = GameState.playWait;
        StartCoroutine(StartCutSceneCr(drillMoveDuration));
    }    

    IEnumerator StartCutSceneCr(float duration)
    {
        SetGameSpeed(2);        

        float t = 0;
        drill.moveDrill = false;
        Vector3 startPos = drill.centerPos;
        while (true)
        {
            t += Time.fixedDeltaTime / duration;
            if (t > 1) t = 1;

            drill.centerPos = Vector3.Lerp(startPos, drill.farPos, t);

            if (t == 1) break;

            drill.centerPos -= Vector3.right * Time.fixedDeltaTime / duration * drillMove;            

            yield return new WaitForFixedUpdate();
        }

        //yield return new WaitForSeconds(duration);
        SetGameSpeed(1);

        StartGame();
    }

    public void PlayClearCutScene()
    {
        if (state == GameState.clearWait)
        {
            Debug.Log("alreadt in state : " + state);
            return;
        }
        state = GameState.clearWait;

        SoundManager.PlaySound("clear");
        SetGameSpeed(0);

        float clearWait = 3f;
        StartCoroutine(ClearCutSceneCr(clearWait));
        Invoke(nameof(GameClear), clearWait);        
    }

    IEnumerator ClearCutSceneCr(float duration)
    {
        float move = 12f;

        while (true)
        {
            repeatGround.Elevator.Translate(0, move * Time.deltaTime / duration, 0);
            yield return null;
        }                
    }

    #endregion 컷신연출

    #region 드릴이동
    bool drillOnMove = false;

    public void SetDrillDistance(DrillDistanceState _drillDist, float duration = 1)
    {
        //Debug.Log("SetDrillDistance");

        if (_drillDist == drillDist)
        {
            Debug.Log("already int drill dist state : " + drillDist);
            return;
        }

        drillDist = _drillDist;
        progressBar.MoveDrillIconSmooth(_drillDist, duration);

        StartCoroutine(MoveDrillCr(duration, _drillDist));
        
    }

    IEnumerator MoveDrillCr(float duration, DrillDistanceState dist)
    {
        Debug.Log("MoveDrillCr : " + dist);

        float t = 0;
        drill.moveDrill = true;

        Vector3 startPos = drill.centerPos;
        Vector3 movePos;
        if (dist == DrillDistanceState.far) movePos = drill.farPos;
        else if (dist == DrillDistanceState.close) movePos = drill.closePos;
        else if (dist == DrillDistanceState.impact) movePos = drill.centerPos + Vector3.right * 100;
        else
        {
            Debug.Log("undefined dist!");
            yield break;
        }

        drillOnMove = true;
        while (true)
        {
            t += Time.fixedDeltaTime / duration;
            if (t > 1) t = 1;

            drill.centerPos = Vector3.Lerp(startPos, movePos, t);

            if (t == 1) break;

            drill.centerPos -= Vector3.right * Time.fixedDeltaTime / duration * drillMove;

            yield return new WaitForFixedUpdate();
        }
        drillOnMove = false;
        //drill.moveDrill = true;

    }

    #endregion 드릴이동



}
