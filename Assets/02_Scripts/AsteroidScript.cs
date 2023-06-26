using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs-Asteroid01
public class AsteroidScript : MonoBehaviour {
    public float speed = 4.5f;      //���༺ �̵� �ӵ�
    public float rotSpeed = 50;     //���༺ ȸ�� �ӵ�
    public float coin = 2;          //���༺ �ı��� ��� ����
    public float hp = 10;
    void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime; //�̵�-���� �̿�
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));  //ȸ��-Rotate �Լ� �̿�
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
