using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 종료 시 오브젝트 파괴
public class EffectAnimation : MonoBehaviour
{        
    Animator anim;
    Animator Anim { 
        get {
            if(!anim) anim = GetComponent<Animator>();
            return anim;
        }
    }    

    // Start is called before the first frame update
    void Start()
    {        
        //if (!anim) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)    
        {                        
            Destroy(gameObject);
        }
    }
}
