using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

//04_Prefabs-Asteroid01
public class AsteroidScript : MonoBehaviour {
    public Transform hpTransform;
    public Transform hpBackTransform;
    public float speed = 4.5f;      //소행성 이동 속도
    public float rotSpeed = 50;     //소행성 회전 속도
    public double coin = 2;          //소행성 파괴시 얻는 코인
    public double hp = 10;
    public double maxHp;
    Vector3 hpTargetScale;
    Vector3 hpOrigin;

    //hp가 0이 되기 전에 파괴되지 않도록 수정
    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.3f;

    private void Awake() {
        hpOrigin = hpTransform.localPosition;
    }

    void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime; //이동-벡터 이용
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));  //회전-Rotate 함수 이용
        if (hp < 0) {
            hp = 0;
        }
        double result = hp / maxHp;        
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.localScale = Vector3.Lerp(hpTransform.localScale, hpTargetScale, Time.deltaTime * 3);
        
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
        hpTransform.position = transform.position + hpOrigin;
        hpBackTransform.position = transform.position + hpOrigin;
        if (destroyFlag == true) {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime) {
                destroyFlag = false;
                ObjectPoolManager.instance.asteroid.Destroy(gameObject);

                //소행성 발사체로 파괴시 exlosion 생성.
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                string str = Util.GetBigNumber(maxHp);
                GameManager.instance.CreateFloatingText(str, transform.position);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //소행성 파괴시 coin 생성
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                //코인의 가치 설정. PlayerScript에서 사용됨.
                coinObj.GetComponent<CoinScript>().coinSize = coin;
                //Destroy(collision.gameObject);  //hp<=0일 때 소행성 제거              
                AudioManagerScript.instance.PlaySound(Sound.Explosion);
            }
        }
    }
    public void DestroyGameObject(int type=0) {
        if(type == 0) {
            ObjectPoolManager.instance.asteroid.Destroy(gameObject);
        } else {
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }        
    }
    public void Init(double hp, double coin) {
        this.hp = hp;
        this.coin = coin;
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
        hpTransform.position = transform.position + hpOrigin;
        hpBackTransform.position = transform.position + hpOrigin;
        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
    }
}
