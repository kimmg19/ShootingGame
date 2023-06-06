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
         /* Mathf.Lerp �Լ��� �� ���� �� ���̿��� ���������� ������ ���� ��ȯ�ϴ� �Լ��Դϴ�.
         ù ��° �Ű������� ���� ��(a.x),
         �� ��° �Ű������� ��ǥ ��(10)�̸�,
         �� ��° �Ű�����(Time.deltaTime * 10)�� ���� �ӵ��� �����ϴ� �� ���˴ϴ�.
         */
        a.x= Mathf.Lerp(a.x, 10, Time.deltaTime * 10);
        transform.position = a;
    }
}
