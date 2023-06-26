using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs-Coin
public class CoinScript : MonoBehaviour {
    public float speed = 1.3f; //코인의 속도-배경의 속도와 똑같이
    public float coinSize = 1;
    Transform playerTr;
    private void Start() {
        playerTr = GameObject.FindWithTag("Player").transform;
    }
    void Update() {
        if (playerTr == null) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        } else {
            transform.position=Vector3.Lerp(transform.position, playerTr.position, Time.deltaTime * 5);
        }
    }

    public void DestroyGameObject() {
        ObjectPoolManager.instance.coin.Destroy(gameObject);
    }
}