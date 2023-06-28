using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assets/04_Prefabs/PlayerShot.prefab
public class ShotScript : MonoBehaviour {
    public float speed = 10;
    public GameObject shotEffect;
    public GameObject coin;        //Coin 프리팹 연결
    public GameObject explosion;
    public double dmg;
    void Update() {
        //발사체 이동
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //발사체에서 충돌검사
    private void OnTriggerEnter2D(Collider2D collision) {
        //collision.tag-발사체와 충돌된 물체 tag 가져옴.
        if (collision.gameObject.tag == "Asteroid") {
            //AsteroidScrpit에서 hp 값 가져오기
            AsteroidScript asteroidScript = collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= dmg;

            //발사체가 소행성에 적중시 적중효과 shotEfffect 생성.
            //Instantiate(shotEffect,transform.position,Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (asteroidScript.hp <= 0) {
                asteroidScript.hp = 0;
                asteroidScript.DestroyGameObject(1);
            }
            //발사체가 적에 적중시 발사체 제거
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);

        } else if (collision.gameObject.tag == "Enemy") {
            //EnemyScript에서 hp 값 가져오기
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= dmg;

            //발사체가 적에 적중시 적중효과 shotEfffect 생성.
            //Instantiate(shotEffect, transform.position, Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (enemyScript.hp <= 0) {
                enemyScript.hp = 0; 
                enemyScript.DestroyGameObject(1);

            }
            //발사체가 적에 적중시 발사체 제거
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        } else if (collision.gameObject.tag == "Boss") {
            //EnemyScript에서 hp 값 가져오기
            BossScript bossScript = collision.gameObject.GetComponent<BossScript>();
            bossScript.hp -= dmg;

            //발사체가 적에 적중시 적중효과 shotEfffect 생성.
            //Instantiate(shotEffect, transform.position, Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (bossScript.hp <= 0) {
                bossScript.hp = 0;
                bossScript.DestroyGameObject(1);

            }
            //발사체가 적에 적중시 발사체 제거
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
    }
    public void DestroyGameObject() {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }
    //화면 밖으로 벗어나면 삭제, SetActive(false) 경우에도 실행됨
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}