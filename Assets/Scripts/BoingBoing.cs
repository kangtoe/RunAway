using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingBoing : MonoBehaviour
{
    Vector3 scaleOrigin;    

    [SerializeField]
    Vector3 boingAmount = Vector3.zero;

    [SerializeField]
    float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        scaleOrigin = transform.localScale;        
        StartCoroutine(BoingCr());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BoingCr()
    {        
        float changeTIme = 1;
        float leftTime = changeTIme;

        while (true)
        {
            transform.localScale = scaleOrigin;

            while (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                transform.localScale += boingAmount * speed * Time.deltaTime;
                yield return null;
            }

            leftTime = changeTIme;

            while (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                transform.localScale -= boingAmount *  Time.deltaTime;
                yield return null;
            }

            leftTime = changeTIme;
        }
    }
}
