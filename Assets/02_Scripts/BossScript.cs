using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossScript : MonoBehaviour
{
    public enum MoveType { START,MOVE,ATTACK,RANDOM_MOVE}
    public struct State {
        public Vector3 pos;
        public float time;
        public MoveType type;
        public State(Vector3 pos, float time,MoveType type) {
            this.pos = pos;
            this.time = time;
            this.type = type;
        }
    }
    public enum RandomMoveType { START,MOVE};
    public Transform shotTr;
    public Transform hpTransform;
    public float shotDelay;
    public float shotMax = 0.1f;
    public List<State> orders;
    public float time;
    public int index;
    public double hp;
    public double coin;
    public double maxHp;
    public Vector3 hpTargetScale;
    RandomMoveType randomMoveState = RandomMoveType.START;
    Vector3 randomPos;

    //hp가 0이 되기 전에 파괴되지 않도록 수정
    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.6f;

    public void Init(double hp, double coin) {
        this.hp = hp;
        this.coin = coin;
        hpTargetScale = new Vector3(1, 1, 1);
        maxHp= hp;
        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
    }
    void Awake()
    {
        orders=new List<State>();
        time = 0;
        index = 0;
        orders.Add(new State(new Vector3(10, 0, 0), 1, MoveType.START));
        orders.Add(new State(new Vector3(6, 0, 0), 1, MoveType.MOVE));
        orders.Add(new State(new Vector3(6, 0, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, 3, 0), 1, MoveType.MOVE));
        orders.Add(new State(new Vector3(6, 3, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, 0, 0), 1, MoveType.MOVE));
        orders.Add(new State(new Vector3(6, 0, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.MOVE));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.ATTACK));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6, -3, 0), 1, MoveType.ATTACK));
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > orders[index].time) {
            time = 0;
            index++;
            if (index >= orders.Count) {
                index = 1;
            }
            shotDelay = 0;
            randomMoveState = RandomMoveType.START;
        } else {
            if (orders[index].type == MoveType.START) {
                transform.position = orders[index].pos;
            } else if (orders[index].type == MoveType.MOVE) {
                transform.position = Vector3.Lerp(transform.position, orders[index].pos, Time.deltaTime * 10);
            } else if (orders[index].type == MoveType.ATTACK) {
                shotDelay += Time.deltaTime;
                if (shotDelay > shotMax) {
                    GameObject shot = ObjectPoolManager.instance.bossShot.Create();
                    shot.transform.position = shotTr.position;
                    shot.transform.rotation = Quaternion.identity;
                    shotDelay = 0;
                }
            } else if (orders[index].type == MoveType.RANDOM_MOVE) {
                if (randomMoveState == RandomMoveType.START) {
                    randomPos = new Vector3(orders[index].pos.x, Random.Range(-3, 3), orders[index].pos.z);
                    randomMoveState = RandomMoveType.MOVE;
                }                
                transform.position = Vector3.Lerp(transform.position, randomPos, Time.deltaTime * 10);
            }
        }
        if (hp < 0) {
            hp = 0;
        }
        double result = hp / maxHp;
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.localScale = Vector3.Lerp(hpTransform.localScale, hpTargetScale, Time.deltaTime*5 );
        if (destroyFlag == true) {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime) {
                destroyFlag = false;
                Destroy(gameObject);
                //발사체로 적 파괴시 exlosion 생성.
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
   
    public void DestroyGameObject(int type = 0) {
        GameManager.instance.remainEnemy--;
        if (type == 0) {
            Destroy(gameObject);
        } else {
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }
    }
}
