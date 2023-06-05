using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject shot;
    public GameObject explosion;
    public Transform shotPointTr;
    float speed = 5;
    Vector3 min, max;
    Vector2 colSize;
    Vector2 chrSize;
    void Start()
    {
          
        //ī�޶� ���� �Ʒ� ����
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //ī�޶� ������ �� ����
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        colSize=GetComponent<BoxCollider2D>().size;
        //�ݶ��̴��� ������ /2�� ��
        chrSize = new Vector2(colSize.x/2,colSize.y/2);   
    }


    void Update()
    {
        Move();
        PlayerShot();

    }
    //�÷��̾� �̵� �Լ�
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   //1,-1 ���
        float y = Input.GetAxisRaw("Vertical");     //1,-1 ���

        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position += dir * Time.deltaTime * speed;    //����*�ð�*�ӵ�=����

        float newX = transform.position.x;    //������� x��ǥ
        float newY = transform.position.y;    //������� y��ǥ

        //clamp �Լ� ���� �ּ����� �Լ��� ����.
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public float shotMax = 0.2f;
    public float shotDelay = 0;
    //�߻�ü ���� �Լ�
    void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(shotDelay > shotMax)
            {
                //�߻�ü�� ��ġ ����.
                Vector3 vec = new Vector3(transform.position.x + 1.12f,
                    transform.position.y - 0.17f, transform.position.z);

                //�߻�ü ����,3��°�Ŵ� ��ü ȸ�� ���
                Instantiate(shot, vec, Quaternion.identity);
                shotDelay = 0;
            }
        }        
    }

    //�浹 �˻� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") //�ε��� ��ü�� "item"�� ���
        {
            // GetComponent<CoinScript>()�� �浹�� ���� ��ü���� CoinScript ������Ʈ�� �����ɴϴ�.
            CoinScript coinScript=collision.gameObject.GetComponent<CoinScript>();
            GameManager.instance.coinInGame += coinScript.coinSize;
            GameDataScript.instance.AddCoin(coinScript.coinSize);
            print("Coin"+GameManager.instance.coinInGame);

            GameManager.instance.coinText.text = GameDataScript.instance.GetCoin().ToString();            

            Destroy(collision.gameObject);

        }else if (collision.gameObject.tag =="Asteroid"||
            collision.gameObject.tag =="Enemy"||
            collision.gameObject.tag == "EnemyShot") 
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(explosion,transform.position,Quaternion.identity);
        }
    }

    
}
