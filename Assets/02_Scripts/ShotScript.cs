using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assets/04_Prefabs/PlayerShot.prefab
public class ShotScript : MonoBehaviour
{
    public float speed = 10;
    public GameObject shotEffect;
    public GameObject coin;        //Coin 프리팹 연결
    public GameObject explosion;
    public float dmg;
    void Update()
    {
        //발사체 이동
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //발사체에서 충돌검사
    private void OnTriggerEnter2D(Collider2D collision) 
    {
         //collision.tag-발사체와 충돌된 물체 tag 가져옴.
        if (collision.gameObject.tag == "Asteroid")
        { 
            //AsteroidScrpit에서 hp 값 가져오기
            AsteroidScript asteroidScript=collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= dmg;

            //발사체가 소행성에 적중시 적중효과 shotEfffect 생성.
            //Instantiate(shotEffect,transform.position,Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (asteroidScript.hp <= 0)
            {
                //소행성 발사체로 파괴시 exlosion 생성.
                //Instantiate(explosion,transform.position,Quaternion.identity) ;
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //소행성 파괴시 coin 생성
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                //코인의 가치 설정. PlayerScript에서 사용됨.
                coinObj.GetComponent<CoinScript>().coinSize = asteroidScript.coin;
                //Destroy(collision.gameObject);  //hp<=0일 때 소행성 제거              
                asteroidScript.DestroyGameObject();
            }
            //발사체가 적에 적중시 발사체 제거
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);

        }else if (collision.gameObject.tag == "Enemy")
        {
            //EnemyScript에서 hp 값 가져오기
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= dmg;

            //발사체가 적에 적중시 적중효과 shotEfffect 생성.
            //Instantiate(shotEffect, transform.position, Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation= Quaternion.identity;
            ShotEffectScript shotEffectScript=shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (enemyScript.hp <= 0)
            {
                //발사체로 적 파괴시 exlosion 생성.
                //Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //적 파괴시 coin 생성
                
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinObj.GetComponent<CoinScript>().coinSize = enemyScript.coin;

                //hp<=0일 때 적 제거
                //Destroy(collision.gameObject);  
                enemyScript.DestroyGameObject();
            }
            //발사체가 적에 적중시 발사체 제거
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
    }
    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }
    //화면 밖으로 벗어나면 삭제, SetActive(false) 경우에도 실행됨
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}