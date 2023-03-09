using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    public static CameraManager Instance
    {
        get
        {            
            if (instance == null)
            {                
                instance = FindObjectOfType<CameraManager>();
            }
            
            return instance;
        }
    }
    private static CameraManager instance;
    
    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {

    }

    public void Shake(float shakeDuration = 0.2f, float shakeAmount = 0.2f)
    {
        StopShake();
        originalPosition = transform.position;
        StartCoroutine(ShakeCr(shakeDuration, shakeAmount));
    }

    void StopShake()
    {
        StopAllCoroutines();
        transform.position = originalPosition;
    }

    IEnumerator ShakeCr(float shakeDuration, float shakeAmount)
    {        
        while(shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;            
 
            shakeDuration -= Time.deltaTime;
            yield return null;

        }
        
    }
}
