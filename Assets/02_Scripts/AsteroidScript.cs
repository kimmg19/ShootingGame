using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs-Asteroid01
public class AsteroidScript : MonoBehaviour {
    public float speed = 4.5f;      //소행성 이동 속도
    public float rotSpeed = 50;     //소행성 회전 속도
    public float coin = 2;          //소행성 파괴시 얻는 코인
    public float hp = 10;
    void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime; //이동-벡터 이용
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));  //회전-Rotate 함수 이용
    }

    public void DestroyGameObject() {
        //Destroy(gameObject);
        ObjectPoolManager.instance.asteroid.Destroy(gameObject);
    }
    public void Init(float hp, float coin) {
        this.hp = hp;
        this.coin = coin;
    }
}
