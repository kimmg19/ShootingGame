using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBGScript : MonoBehaviour
{
     
    float speed = 0.6f;
    SpriteRenderer spr;
    void Start()
    {
        spr=GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed) ;
        Vector3 pos=transform.position;
        //spr.bounds.size.x / 2 -> starBG�� ���� ������ ����
        if (pos.x + spr.bounds.size.x / 2 < -8)
        {
            pos.x += spr.bounds.size.x * 3;
            transform.position= pos;    
        }
    }
}