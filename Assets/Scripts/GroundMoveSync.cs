using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoveSync : MonoBehaviour
{
    RepeatGround ground;
    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        ground = FindObjectOfType<RepeatGround>();
        move = Vector3.right * GameManager.Instance.GameSpeed * ground.Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= move;
    }
}
