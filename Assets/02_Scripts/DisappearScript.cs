using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs/Explosion, shotEffect
public class DisappearScript : MonoBehaviour
{ 
    public float time = 1.0f;
    void Start()
    {
        //time�� �Ŀ� ����ȿ�� ����
        Destroy(gameObject, time);
    }
}