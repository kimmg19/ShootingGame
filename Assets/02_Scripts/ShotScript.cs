using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assets/04_Prefabs/PlayerShot.prefab
public class ShotScript : MonoBehaviour {
    public float speed = 10;
    public GameObject shotEffect;
    public GameObject coin;        //Coin ������ ����
    public GameObject explosion;
    public double dmg;
    void Update() {
        //�߻�ü �̵�
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //�߻�ü���� �浹�˻�
    private void OnTriggerEnter2D(Collider2D collision) {
        //collision.tag-�߻�ü�� �浹�� ��ü tag ������.
        if (collision.gameObject.tag == "Asteroid") {
            //AsteroidScrpit���� hp �� ��������
            AsteroidScript asteroidScript = collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= dmg;

            //�߻�ü�� ���༺�� ���߽� ����ȿ�� shotEfffect ����.
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
            //�߻�ü�� ���� ���߽� �߻�ü ����
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);

        } else if (collision.gameObject.tag == "Enemy") {
            //EnemyScript���� hp �� ��������
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= dmg;

            //�߻�ü�� ���� ���߽� ����ȿ�� shotEfffect ����.
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
            //�߻�ü�� ���� ���߽� �߻�ü ����
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        } else if (collision.gameObject.tag == "Boss") {
            //EnemyScript���� hp �� ��������
            BossScript bossScript = collision.gameObject.GetComponent<BossScript>();
            bossScript.hp -= dmg;

            //�߻�ü�� ���� ���߽� ����ȿ�� shotEfffect ����.
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
            //�߻�ü�� ���� ���߽� �߻�ü ����
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
    }
    public void DestroyGameObject() {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }
    //ȭ�� ������ ����� ����, SetActive(false) ��쿡�� �����
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}