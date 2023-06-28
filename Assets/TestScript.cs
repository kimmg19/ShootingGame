using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform hpTransform;
    public Vector3 hpTargetScale;
    public double maxHp;
    public double hp;
    public double result;
    void Start()
    {
        hp = 100;
        maxHp = 100;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            hp -= 10;
        }
        result = hp / maxHp;
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.localScale = Vector3.Lerp(hpTransform.localScale, hpTargetScale, Time.deltaTime);
    }
}
