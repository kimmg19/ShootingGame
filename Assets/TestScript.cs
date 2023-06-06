using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float p;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position=Vector3.zero;
        }
        Vector3 a = transform.position;
        a.x= Mathf.Lerp(a.x, 10, Time.deltaTime*10);
        transform.position = a;
    }
}
