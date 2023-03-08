using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //싱글톤
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }

            // 싱글톤 오브젝트를 반환
            return instance;
        }
    }
    private static InputManager instance;    

    // 버튼 유지 시 계속 입력 값은 참
    public bool SlideInput => slideinput;
    bool slideinput = false;

    // 버튼 클릭한 프레임에만 입력 값이 참
    public bool JumpInput => jumpinput; // || Input.GetButtonDown("Jump");
    bool jumpinput = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        jumpinput = false;
    }

    public void SetSlideInput(bool active)
    {
        slideinput = active;
    }

    public void SetJumpInput(bool active)
    {
        jumpinput = active;
    }
}
