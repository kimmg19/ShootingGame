using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int type ;
    public int hp = 3;
    public float speed = 4;         //적 이동속도
    public float coin = 0;
    public float time;
    public GameObject enemyShot;
    public float maxShotTime;       //적 발사체의 빈도 수
    public float shotSpeed;         //적 발사체의 속도
    void Start()
    {
        //type은 유니티에서 불러옴.
        switch (type)
        {
            case 0:
                hp = 10; speed = 1.4f; coin = 3; maxShotTime = 3; shotSpeed = 3;
                break;
            case 1:
                hp = 20; speed = 1.3f; coin = 4; maxShotTime = 2; shotSpeed = 4;
                break;
            case 2:
                hp = 50; speed = 1.2f; coin = 5; maxShotTime = 1; shotSpeed = 5;
                break;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxShotTime)
        {    
            //적 발사체 생성,오브젝트 받아서->오브젝트의 스크립트받아서->스크립트의 speeed 선언
            GameObject shotObj = Instantiate(enemyShot, transform.position, Quaternion.identity);
            //EnemyShotScript shotScript= shotObj.GetComponent<EnemyShotScript>();
            //shotScript.speed = shotSpeed;
            shotObj.GetComponent<EnemyShotScript>().speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);     //적 이동
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
