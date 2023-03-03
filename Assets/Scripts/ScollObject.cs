using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScollObject : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float width = 20;
    

    void Start()
    {
        //Collider2D backGroundCollider = GetComponentInChildren<Collider2D>();
        //width = backGroundCollider.bounds.size.x;

        Debug.Log("width : " + width);
    }

    void Update()
    {
        //if(GameManager.instance.isGameOver) retrun;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= -width)
        {            
            Vector2 offset = new Vector2(width * 2f, 0f);
            transform.position = (Vector2)transform.position + offset;
        }
    }

}
