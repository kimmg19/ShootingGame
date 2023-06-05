using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearScript : MonoBehaviour
{
    public float time = 1.0f;
    void Start()
    {
        //time초 후에 폭발효과 삭제
        Destroy(gameObject, time);
    }
}