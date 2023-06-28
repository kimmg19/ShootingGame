using Game;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public Transform hpTransform;
    public int type;
    public double hp;
    public float speed;         //적 이동속도
    public double coin;
    public float time;
    public GameObject enemyShot;
    public float maxShotTime;       //적 발사체의 빈도 수
    public float shotSpeed;         //적 발사체의 속도
    public string enemyName;
    public double maxHp;
    Vector3 hpTargetScale;

    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.3f;

    public void Init(int type,string name, double hp, float speed, float maxShotTime,
        float shotSpeed, double coin) {
        this.type = type;
        this.enemyName = name;
        this.hp = hp;
        this.speed = speed;
        this.maxShotTime = maxShotTime;
        this.shotSpeed = shotSpeed;
        this.coin = coin;
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;

    }
    void Awake() {
        maxHp = hp;
    }

    void Update() {
        time += Time.deltaTime;
        if (time > maxShotTime) {

            //적 발사체 생성,오브젝트 받아서->오브젝트의 스크립트받아서->스크립트의 speed 선언
            //GameObject shotObj = Instantiate(enemyShot,transform.position, Quaternion.identity);
            GameObject shotObj = ObjectPoolManager.instance.enemyShot.Create();
            shotObj.transform.position = transform.position;
            shotObj.transform.rotation = Quaternion.identity;
            //적 발사체 속도 지정
            shotObj.GetComponent<EnemyShotScript>().speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);     //적 이동
        if (hp < 0) {
            hp = 0;
        }
        double result = hp / maxHp;
        hpTargetScale = new  Vector3((float)result, 1, 1);
        hpTransform.localScale = Vector3.Lerp(hpTransform.localScale, hpTargetScale, Time.deltaTime * 3);
        if (destroyFlag == true) {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime) {
                destroyFlag = false;
                ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
                //발사체로 적 파괴시 exlosion 생성.
                //Instantiate(explosion, transform.position, Quaternion.identity);
                GameObject explosionObj = ObjectPoolManager.instance.explosion.Create();
                explosionObj.transform.position = transform.position;
                explosionObj.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObj.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                string str = Util.GetBigNumber(maxHp);
                GameManager.instance.CreateFloatingText(str, transform.position);

                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f), 0);

                //적 파괴시 coin 생성

                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinObj.GetComponent<CoinScript>().coinSize = coin;

                //hp<=0일 때 적 제거
                //Destroy(collision.gameObject);  
                AudioManagerScript.instance.PlaySound(Sound.Explosion);
            }
        }
    }
    public void DestroyGameObject(int type=0) {
        if(type==0) {
            ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
        } else {
            GameManager.instance.remainEnemy--;
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }        
    }
}
