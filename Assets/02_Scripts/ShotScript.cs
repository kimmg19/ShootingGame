using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public float speed = 10;
    public GameObject shotEffect;
    public GameObject coin;
    public GameObject explosion;
    void Update()
    {
        //발사체 이동
        transform.Translate(Vector3.right *Time.deltaTime* speed);
    }

    //발사체에서 충돌검사
    private void OnTriggerEnter2D(Collider2D collision) 
    {
         //collision.tag-발사체와 충돌된 물체 tag 가져옴.
        if (collision.gameObject.tag == "Asteroid")
        {
            //AsteroidScrpit에서 hp 값 가져오기
            AsteroidScript asteroidScript=collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= 3;

            //발사체가 소행성에 적중시 shotEfffect 생성.
            Instantiate(shotEffect,transform.position,Quaternion.identity);
            if (asteroidScript.hp <= 0)
            {
                //발사체가 소행성에 적중시 exlosion 생성.
                Instantiate(explosion,transform.position,Quaternion.identity) ;
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //소행성 파괴시 coin 생성
                /*
                 coin 프리팹을 복제하여 coinObj라는 새로운 게임 오브젝트를 생성하고, 이를 변수 coinObj에 할당합니다.
                 coinObj에서 CoinScript 컴포넌트를 가져와서 coinScript 변수에 할당합니다. 
                 coinScript의 coinSize 변수에 asteroidScript의 coin 값을 할당합니다.
                 */
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = asteroidScript.coin;

                Destroy(collision.gameObject);  //hp<=0일 때 소행성 제거                
            }
            //발사체가 적에 적중시 발사체 제거
            Destroy(gameObject);

        }else if (collision.gameObject.tag == "Enemy")
        {
            //EnemyScript에서 hp 값 가져오기
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= 3;

            //발사체가 적에 적중시 shotEfffect 생성.
            Instantiate(shotEffect, transform.position, Quaternion.identity);
            if (enemyScript.hp <= 0)
            {
                //발사체가 적에 적중시 exlosion 생성.
                Instantiate(explosion, transform.position, Quaternion.identity);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //적 파괴시 coin 생성
                /*
                 coin 프리팹을 복제하여 coinObj라는 새로운 게임 오브젝트를 생성하고, 이를 변수 coinObj에 할당합니다.
                 coinObj에서 CoinScript 컴포넌트를 가져와서 coinScript 변수에 할당합니다. 
                 coinScript의 coinSize 변수에 enemyScript의 coin 값을 할당합니다.
                 */
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = enemyScript.coin;
                //hp<=0일 때 적 제거
                Destroy(collision.gameObject);  

            }
            //발사체가 적에 적중시 발사체 제거
            Destroy(gameObject);
        }
    }

    //화면 밖으로 벗어나면 삭제
    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}