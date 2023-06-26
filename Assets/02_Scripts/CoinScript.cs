using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//04_Prefabs-Coin
public class CoinScript : MonoBehaviour
{ 
    public float speed = 1.3f; //������ �ӵ�-����� �ӵ��� �Ȱ���
    public float coinSize = 1;
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }

    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.coin.Destroy(gameObject);
    }
}