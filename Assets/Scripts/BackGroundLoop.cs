using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    float width;

    void Awake()
    {
        Initailize();
    }

    void Update()
    {
        RepositionControl();
    }

    void Initailize()
    {
        Collider2D backGroundCollider = GetComponent<Collider2D>();
        width = backGroundCollider.bounds.size.x;
    }

    void RepositionControl()
    {
        if (transform.position.x <= -width)
        {
            Vector2 offset = new Vector2(width * 2f, 0f);
            transform.position = (Vector2)transform.position + offset;
        }         
    }
}
