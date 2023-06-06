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
            asteroidScript.hp -= 3;

            //�߻�ü�� ���༺�� ���߽� ����ȿ�� shotEfffect ����.
            Instantiate(shotEffect,transform.position,Quaternion.identity);
            if (asteroidScript.hp <= 0)
            {
                //���༺ �߻�ü�� �ı��� exlosion ����.
                Instantiate(explosion,transform.position,Quaternion.identity) ;
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //���༺ �ı��� coin ����
                /*
                 coin �������� �����Ͽ� coinObj��� ���ο� ���� ������Ʈ�� �����ϰ�, �̸� ���� coinObj�� �Ҵ��մϴ�.
                 coinObj���� CoinScript ������Ʈ�� �����ͼ� coinScript ������ �Ҵ��մϴ�. 
                 coinScript�� coinSize ������ asteroidScript�� coin ���� �Ҵ��մϴ�.
                 */
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);

                //������ ��ġ ����. PlayerScript���� ����.
                coinObj.GetComponent<CoinScript>().coinSize = asteroidScript.coin;
                Destroy(collision.gameObject);  //hp<=0�� �� ���༺ ����                
            }
            //�߻�ü�� ���� ���߽� �߻�ü ����
            Destroy(gameObject);

        }else if (collision.gameObject.tag == "Enemy")
        {
            //EnemyScript���� hp �� ��������
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= 3;

            //�߻�ü�� ���� ���߽� ����ȿ�� shotEfffect ����.
            Instantiate(shotEffect, transform.position, Quaternion.identity);
            if (enemyScript.hp <= 0)
            {
                //�߻�ü�� �� �ı��� exlosion ����.
                Instantiate(explosion, transform.position, Quaternion.identity);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //�� �ı��� coin ����
                /*
                 coin �������� �����Ͽ� coinObj��� ���ο� ���� ������Ʈ�� �����ϰ�, �̸� ���� coinObj�� �Ҵ��մϴ�.
                 coinObj���� CoinScript ������Ʈ�� �����ͼ� coinScript ������ �Ҵ��մϴ�. 
                 coinScript�� coinSize ������ enemyScript�� coin ���� �Ҵ��մϴ�.
                 */
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                coinObj.GetComponent<CoinScript>().coinSize = enemyScript.coin;
                
                //hp<=0�� �� �� ����
                Destroy(collision.gameObject);  

            }
            //�߻�ü�� ���� ���߽� �߻�ü ����
            Destroy(gameObject);
        }
    }

    //ȭ�� ������ ����� ����
    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}