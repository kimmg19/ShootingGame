using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float maxTime = 1;
    public float time = 0;
    public void InitTime()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            DestroyGameObject();
        }
    }
    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.explosion.Destroy(gameObject);
    }
}
