using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //ΩÃ±€≈Ê
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }

            // ΩÃ±€≈Ê ø¿∫Í¡ß∆Æ∏¶ π›»Ø
            return instance;
        }
    }
    private static InputManager instance;

    public bool SlideInput => slideinput;
    bool slideinput = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlideInput(bool active)
    {
        slideinput = active;
    }
}
