using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int type;
    public int hp;
    public float speed;         //�� �̵��ӵ�
    public float coin;
    public float time;
    public GameObject enemyShot;
    public float maxShotTime;       //�� �߻�ü�� �� ��
    public float shotSpeed;         //�� �߻�ü�� �ӵ�
    void Start()
    {
        //type�� ����Ƽ���� �ҷ���.
        switch (type)
        {
            case 0:
                hp = 10; speed = 1.4f; coin = 3; maxShotTime = 3; shotSpeed = 3;
                break;
            case 1:
                hp = 20; speed = 1.3f; coin = 4; maxShotTime = 2; shotSpeed = 4;
                break;
            case 2:
                hp = 40; speed = 1.2f; coin = 5; maxShotTime = 1.3f; shotSpeed = 5;
                break;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxShotTime)
        {
            
            //�� �߻�ü ����,������Ʈ �޾Ƽ�->������Ʈ�� ��ũ��Ʈ�޾Ƽ�->��ũ��Ʈ�� speed ����
            GameObject shotObj = Instantiate(enemyShot,transform.position, Quaternion.identity);
            //�� �߻�ü �ӵ� ����
            shotObj.GetComponent<EnemyShotScript>().speed = shotSpeed;            
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);     //�� �̵�
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
