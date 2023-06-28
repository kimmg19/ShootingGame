using Game;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public Transform hpTransform;
    public int type;
    public double hp;
    public float speed;         //�� �̵��ӵ�
    public double coin;
    public float time;
    public GameObject enemyShot;
    public float maxShotTime;       //�� �߻�ü�� �� ��
    public float shotSpeed;         //�� �߻�ü�� �ӵ�
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

            //�� �߻�ü ����,������Ʈ �޾Ƽ�->������Ʈ�� ��ũ��Ʈ�޾Ƽ�->��ũ��Ʈ�� speed ����
            //GameObject shotObj = Instantiate(enemyShot,transform.position, Quaternion.identity);
            GameObject shotObj = ObjectPoolManager.instance.enemyShot.Create();
            shotObj.transform.position = transform.position;
            shotObj.transform.rotation = Quaternion.identity;
            //�� �߻�ü �ӵ� ����
            shotObj.GetComponent<EnemyShotScript>().speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);     //�� �̵�
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
                //�߻�ü�� �� �ı��� exlosion ����.
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

                //�� �ı��� coin ����

                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                coinObj.GetComponent<CoinScript>().coinSize = coin;

                //hp<=0�� �� �� ����
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
