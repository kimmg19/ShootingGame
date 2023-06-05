using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float speed = 4.5f;     //소행성 이동 속도
    public float rotSpeed = 50; //소행성 회전 속도
    public float coin = 2;
    public int hp = 10;
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; //이동-벡터 이용
        transform.Rotate(new Vector3(0,0,Time.deltaTime*rotSpeed));  //회전-Rotate 함수 이용
    }

    //소행성이 화면 밖으로 나갈 떄 파괴.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
