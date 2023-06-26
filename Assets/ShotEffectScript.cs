using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffectScript : MonoBehaviour {
    public float maxTime = 1;
    public float time = 0;
    public void InitTime() {
        time = 0;
    }

    void Update() {
        time += Time.deltaTime;
        if (time > maxTime) {
            DestroyGameObject();
        }
    }
    public void DestroyGameObject() {
        ObjectPoolManager.instance.shotEffect.Destroy(gameObject);
    }
}
