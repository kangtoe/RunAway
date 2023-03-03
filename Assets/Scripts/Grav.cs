using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grav : MonoBehaviour
{
    [SerializeField]
    Vector2 grav = new Vector2(0, -9.8f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = grav;
    }
}
