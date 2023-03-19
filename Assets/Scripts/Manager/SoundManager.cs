using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 하강 사운드 길이가 너무 긺
// 배경음악이 효과음과 분간 어려움
// 아이템 획득 사운드 볼륨이 작음

public enum SoundType
{
    Bgm, //반복재생 해야 됨. 배경음악
    Sfx, //짧게 1번만 재생됨.
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }
    private static SoundManager instance;

    [SerializeField] Sound[] sounds;
    [SerializeField] AudioSource[] sfxPlayer;
    [SerializeField] AudioSource[] bgmPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != this) Destroy(gameObject);
        Object.DontDestroyOnLoad(gameObject);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundName, SoundType type = SoundType.Sfx)
    {
        bool loopCheck = false; 
        AudioSource[] players =null;

        if (type == SoundType.Bgm)
        {
            players = bgmPlayer;
            loopCheck = true;
        }
        if (type == SoundType.Sfx)
        {
            players = sfxPlayer;
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (soundName == sounds[i].name)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    // Player에서 재생 중이지 않은 Audio Source를 발견했다면 
                    if (!players[j].isPlaying)
                    {
                        players[j].clip = sounds[i].clip;
                        players[j].Play();
                        players[j].loop = loopCheck;
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다. type : " + type);
                return;
            }
        }
        Debug.Log(soundName + " 이름의 효과음이 없습니다.");
        return;
    }

    // bgm만 해당
    public void StopSound(string soundName)
    {
        string clipName = null;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                clipName = sounds[i].clip.name;
            }
        }

        if(clipName == null) Debug.Log(soundName + " 이름의 효과음이 없습니다.");

        for (int i = 0; i < bgmPlayer.Length; i++)
        {
            if (bgmPlayer[i].clip?.name == clipName)
            {
                bgmPlayer[i].loop = false;
                //bgmPlayer[i].Stop();
            }
        }                    
    }
}
