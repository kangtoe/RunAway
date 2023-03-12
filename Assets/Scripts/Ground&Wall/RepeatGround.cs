using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : RepeatObjects
{
    [SerializeField]
    ItemSpwaner itemSpwaner;

    [SerializeField]
    GameObject[] GroundPrefabs;

    [SerializeField]
    GameObject ClearGroundPrefab;
    [SerializeField]
    Transform elevator;
    public Transform Elevator => elevator;

    [SerializeField]
    public bool isRepeating = true;

    [SerializeField]
    int spwanedGroundCount = 0;
    [Header("해당 N번째 프리팹 마다 아이템 등장")]
    [SerializeField]
    int ItemSpwanInterval = 3;

    GameManager GameManager => GameManager.Instance;    

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.instance.isGameOver) retrun;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.Translate(Vector3.left * GameManager.Instance.GameSpeed * speed * Time.deltaTime);

            if (isRepeating && objects[i].transform.position.x <= -width)
            {
                //Debug.Log("set ground to new pos");

                Vector2 offset = new Vector2(width * 2, 0f);
                objects[i].transform.position = (Vector2)objects[i].transform.position + offset;

                if (GameManager.IsPlaying)
                {
                    // 그라운드 패턴 교체                         
                    GameObject nextGround = Instantiate(GetRandomGround(), transform);
                    nextGround.transform.position = objects[i].transform.position;
                    Destroy(objects[i]);
                    objects[i] = nextGround;

                    // 아이템 생성
                    if (spwanedGroundCount != 0 && spwanedGroundCount % ItemSpwanInterval == 0)
                    {
                        Transform spwanPoint;
                        float ran = Random.Range(0f, 1f);
                        if (ran > 0.5f) spwanPoint = nextGround.transform.Find("Itempoint1");
                        else spwanPoint = nextGround.transform.Find("Itempoint2");
                        if (spwanPoint == null) Debug.Log("spwanPoint is null!");
                        itemSpwaner.SpwanItem(spwanPoint);
                    }                    

                    spwanedGroundCount++;
                }

                if(GameManager.IsInPreClearWait)
                {                    
                    GameObject nextGround = Instantiate(ClearGroundPrefab, transform);
                    elevator = nextGround.transform.Find("Tilemap-elevator");
                    Destroy(objects[i]);
                    objects[i] = nextGround;                    

                    isRepeating = false;                                        
                }                
            }
        }
    }

    GameObject GetRandomGround()
    {
        int idx = Random.Range(0, GroundPrefabs.Length);

        return GroundPrefabs[idx];
    }
}
