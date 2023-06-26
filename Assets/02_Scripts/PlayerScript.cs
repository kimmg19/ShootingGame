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

        //카메라 왼쪽 아래 구석
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //카메라 오른쪽 위 구석
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        colSize = GetComponent<BoxCollider2D>().size;
        //캐릭터 콜라이더의 사이즈 /2의 값
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
    //플레이어 이동 함수
    void Move() {
        float x = Input.GetAxisRaw("Horizontal");   //1,-1 출력
        float y = Input.GetAxisRaw("Vertical");     //1,-1 출력

        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position += dir * Time.deltaTime * speed;    //방향*시간*속도=벡터

        float newX = transform.position.x;    //비행기의 x좌표
        float newY = transform.position.y;    //비행기의 y좌표

        //clamp 함수 밑의 주석문을 함수로 구현.
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public float shotDelay = 0;
    public float shotMax = 0.2f;

    //발사체 생성 함수
    void PlayerShot() {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)) {
            if (shotDelay > shotMax) {
                //발사체의 위치 지정.
                Vector3 vec = new Vector3(transform.position.x + 1.12f,
                    transform.position.y - 0.2f, transform.position.z);

                //발사체 생성,3번째거는 물체 회전 담당
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

    //충돌 검사 함수
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Item") //부딪힌 물체가 "item"인 경우
        {
            // GetComponent<CoinScript>()는 충돌한 게임 객체에서 CoinScript 컴포넌트를 가져옵니다.
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
            //플레이어는 한번 파괴되면 끝이기 때문에 오브젝트 풀 사용 안함.
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.instance.RetryPanelSetActiveAfter1sec();
            GameManager.instance.isAlive = false; //플레이어 사망 처리
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
