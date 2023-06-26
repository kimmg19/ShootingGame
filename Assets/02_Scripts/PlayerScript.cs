using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//GameScene/Player
public class PlayerScript : MonoBehaviour {
    public GameObject shot;
    public GameObject explosion;
    public Transform shotPointTr;
    float speed = 5;
    Vector3 min, max;
    Vector2 colSize;
    Vector2 chrSize;
    public float dmg;
    public SpriteRenderer spr;
    void Start() {

        //ī�޶� ���� �Ʒ� ����
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //ī�޶� ������ �� ����
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        colSize = GetComponent<BoxCollider2D>().size;
        //ĳ���� �ݶ��̴��� ������ /2�� ��
        chrSize = new Vector2(colSize.x / 2, colSize.y / 2);
        int select = GameDataScript.instance.select;
        ShipData shipData = GameDataScript.instance.ships[select];
        dmg = shipData.dmg;
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = Resources.Load<Sprite>(shipData.GetImageName());
    }


    void Update() {
        Move();
        PlayerShot();

    }
    //�÷��̾� �̵� �Լ�
    void Move() {
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

    public float shotDelay = 0;
    public float shotMax = 0.2f;

    //�߻�ü ���� �Լ�
    void PlayerShot() {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)) {
            if (shotDelay > shotMax) {
                //�߻�ü�� ��ġ ����.
                Vector3 vec = new Vector3(transform.position.x + 1.12f,
                    transform.position.y - 0.2f, transform.position.z);

                //�߻�ü ����,3��°�Ŵ� ��ü ȸ�� ���
                //GameObject shotObj= Instantiate(shot, vec, Quaternion.identity);
                GameObject shotObj = ObjectPoolManager.instance.playerShot.Create();
                shotObj.transform.position = vec;
                shotObj.transform.rotation = Quaternion.identity;

                ShotScript shotScript = shotObj.GetComponent<ShotScript>();
                shotScript.dmg = dmg;
                shotDelay = 0;
            }
        }
    }

    //�浹 �˻� �Լ�
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Item") //�ε��� ��ü�� "item"�� ���
        {
            // GetComponent<CoinScript>()�� �浹�� ���� ��ü���� CoinScript ������Ʈ�� �����ɴϴ�.
            CoinScript coinScript = collision.gameObject.GetComponent<CoinScript>();

            GameManager.instance.coinInGame += coinScript.coinSize;
            GameDataScript.instance.AddCoin(coinScript.coinSize);
            GameManager.instance.coinText.text = GameDataScript.instance.GetCoin().ToString();
            //Destroy(collision.gameObject);            
            coinScript.DestroyGameObject();

        } else if (collision.gameObject.tag == "Asteroid") {
            //Destroy(collision.gameObject);
            ObjectPoolManager.instance.asteroid.Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false;

        } else if (collision.gameObject.tag == "Enemy") {
            //Destroy(collision.gameObject);
            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
            enemyScript.DestroyGameObject();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false;

        } else if (collision.gameObject.tag == "EnemyShot") {
            //Destroy(collision.gameObject);
            EnemyShotScript enemyShotScript = collision.GetComponent<EnemyShotScript>();
            enemyShotScript.DestroyGameObject();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false;

        } else if (collision.gameObject.tag == "BossShot") {
            //Destroy(collision.gameObject);
            BossShotScript bossShotScript = collision.GetComponent<BossShotScript>();
            bossShotScript.DestroyGameObject();
            //�÷��̾�� �ѹ� �ı��Ǹ� ���̱� ������ ������Ʈ Ǯ ��� ����.
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false; //�÷��̾� ��� ó��
        } else if (collision.gameObject.tag == "Boss") {
            //Destroy(collision.gameObject);
            BossScript bossScript = collision.GetComponent<BossScript>();
            bossScript.DestroyGameObject();
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false;

        }


    }
}
