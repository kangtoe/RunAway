using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : RepeatObjects
{    
    [SerializeField]
    GameObject[] GroundPrefabs;

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.instance.isGameOver) retrun;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (objects[i].transform.position.x <= -width)
            {
                Debug.Log("set ground to new pos");

                Vector2 offset = new Vector2(width * 2, 0f);
                objects[i].transform.position = (Vector2)objects[i].transform.position + offset;

                // 그라운드 패턴 교체
                GameObject nextGround = Instantiate(GetRandomGround(), transform);
                nextGround.transform.position = objects[i].transform.position;
                Destroy(objects[i]);
                objects[i] = nextGround;
            }
        }
    }

    GameObject GetRandomGround()
    {
        int idx = Random.Range(0, GroundPrefabs.Length);

        return GroundPrefabs[idx];
    }
}
