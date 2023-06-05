using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    public float speed = 1.3f;      //�޹�� �̵� �ӵ�.
    SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>(); //SpriteRenderer ������Ʈ ������.
    }

    
    void Update()
    {
        //Vector.left=��������
        //transform.position += Vector3.left * Time.deltaTime * speed; 
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        Vector3 pos=transform.position;


        //spr.bounds.size.x-�ٿ�� �ڽ��� ���� ��
        if (pos.x + spr.bounds.size.x / 2 < -8)
        {
            float size=spr.bounds.size.x * 2;
            pos.x += size;
            transform.position = pos;
        }
    }
}