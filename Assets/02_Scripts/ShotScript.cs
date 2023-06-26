using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assets/04_Prefabs/PlayerShot.prefab
public class ShotScript : MonoBehaviour
{
    public float speed = 10;
    public GameObject shotEffect;
    public GameObject coin;        //Coin ������ ����
    public GameObject explosion;
    public float dmg;
    void Update()
    {
        //�߻�ü �̵�
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //�߻�ü���� �浹�˻�
    private void OnTriggerEnter2D(Collider2D collision) 
    {
         //collision.tag-�߻�ü�� �浹�� ��ü tag ������.
        if (collision.gameObject.tag == "Asteroid")
        { 
            //AsteroidScrpit���� hp �� ��������
            AsteroidScript asteroidScript=collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= dmg;

            //�߻�ü�� ���༺�� ���߽� ����ȿ�� shotEfffect ����.
            //Instantiate(shotEffect,transform.position,Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (asteroidScript.hp <= 0)
            {
                //���༺ �߻�ü�� �ı��� exlosion ����.
                //Instantiate(explosion,transform.position,Quaternion.identity) ;
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //���༺ �ı��� coin ����
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                //������ ��ġ ����. PlayerScript���� ����.
                coinObj.GetComponent<CoinScript>().coinSize = asteroidScript.coin;
                //Destroy(collision.gameObject);  //hp<=0�� �� ���༺ ����              
                asteroidScript.DestroyGameObject();
            }
            //�߻�ü�� ���� ���߽� �߻�ü ����
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);

        }else if (collision.gameObject.tag == "Enemy")
        {
            //EnemyScript���� hp �� ��������
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= dmg;

            //�߻�ü�� ���� ���߽� ����ȿ�� shotEfffect ����.
            //Instantiate(shotEffect, transform.position, Quaternion.identity);
            GameObject shotEffectObj = ObjectPoolManager.instance.shotEffect.Create();
            shotEffectObj.transform.position = transform.position;
            shotEffectObj.transform.rotation= Quaternion.identity;
            ShotEffectScript shotEffectScript=shotEffectObj.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (enemyScript.hp <= 0)
            {
                //�߻�ü�� �� �ı��� exlosion ����.
                //Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //�� �ı��� coin ����
                
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinObj.GetComponent<CoinScript>().coinSize = enemyScript.coin;

                //hp<=0�� �� �� ����
                //Destroy(collision.gameObject);  
                enemyScript.DestroyGameObject();
            }
            //�߻�ü�� ���� ���߽� �߻�ü ����
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
    }
    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }
    //ȭ�� ������ ����� ����, SetActive(false) ��쿡�� �����
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}