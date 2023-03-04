using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatObjects : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] objects;

    [SerializeField]
    protected float speed = 5f;

    [SerializeField]
    protected float width;
    

    void Update()
    {
        //if(GameManager.instance.isGameOver) retrun;
        
        foreach (GameObject go in objects)
        {
            go.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (go.transform.position.x <= -width)
            {
                Vector2 offset = new Vector2(width * 2, 0f);
                go.transform.position = (Vector2)go.transform.position + offset;
            }
        }        
    }

}
