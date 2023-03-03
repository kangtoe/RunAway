using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatObjects : MonoBehaviour
{
    [SerializeField]
    GameObject[] objects;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float width;

    void Start()
    {
        //Collider2D backGroundCollider = GetComponentInChildren<Collider2D>();
        //width = backGroundCollider.bounds.size.x;

        //Debug.Log("width : " + width);
    }

    void Update()
    {
        //if(GameManager.instance.isGameOver) retrun;
        
        foreach (GameObject go in objects)
        {
            go.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (go.transform.position.x <= -width)
            {
                Vector2 offset = new Vector2(width * 2f, 0f);
                go.transform.position = (Vector2)go.transform.position + offset;
            }
        }

        
    }

}
