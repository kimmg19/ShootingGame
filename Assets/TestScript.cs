using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TestScene-Triangle
public class TestScript : MonoBehaviour
{

    float p;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position=Vector3.zero;
        }
        Vector3 a = transform.position;
         /* Mathf.Lerp 함수는 두 개의 값 사이에서 선형적으로 보간된 값을 반환하는 함수입니다.
         첫 번째 매개변수는 현재 값(a.x),
         두 번째 매개변수는 목표 값(10)이며,
         세 번째 매개변수(Time.deltaTime * 10)는 보간 속도를 결정하는 데 사용됩니다.
         */
        a.x= Mathf.Lerp(a.x, 10, Time.deltaTime * 10);
        transform.position = a;
    }
}
