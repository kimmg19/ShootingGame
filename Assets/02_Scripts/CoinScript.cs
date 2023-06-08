using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs-Coin
public class CoinScript : MonoBehaviour
{ 
    public float speed = 1.3f; //코인의 속도-배경의 속도와 똑같이
    public float coinSize = 1;
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}