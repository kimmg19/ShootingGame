using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class GameDataScript : MonoBehaviour {
    TextAsset enemyWaveTextAsset;
    TextAsset shipTextAsset;
    public ShipData[] ships;
    public EnemyWave[] enemyWaves;
    public static GameDataScript instance;
    public float coin;      //전체코인-TotalCoin
    private int stage;
    public int GetStage() {
        stage = PlayerPrefs.GetInt("Stage", 1);
        return stage;
    }
    public void AddStage() {
        stage = PlayerPrefs.GetInt("Stage", 1);
        stage++;
        PlayerPrefs.SetInt("Stage", stage);
    }
    private int _select;
    //프로퍼티 사용 예시;
    //int selectedValue = select; 프로퍼티의 get 접근자를 사용하여 값을 가져옴
    //select = 2; 프로퍼티의 set 접근자를 사용하여 값을 설정함
    public int select {
        get {
            _select = PlayerPrefs.GetInt("ChrSelect", 0);
            return _select;
        }
        set {
            _select = value;
            PlayerPrefs.SetInt("ChrSelect", _select);
        }
    }
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        shipTextAsset = Resources.Load<TextAsset>("ship");      //ship 이라는 엑셀 파일 로드하는 중.
        string[] lines = shipTextAsset.text.Split('\n');        //'\n' 엔터의미
        //62줄에서 생성자를 통해 대입 함.
        ships = new ShipData[lines.Length - 2];                 //여기 있는 ships 배열이 메뉴아이템의 데이터임.for문 돌면서 완성됨.
        for (int i = 1; i < lines.Length - 1; i++) {
            string[] rows = lines[i].Split("\t");
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            float unlockCoin = float.Parse(rows[4]);
            float baseUpgradeCoin = float.Parse(rows[5]);
            //PlayerPrefs.GetInt-로컬저장소에 저장,두 번째 매개변수는 해당 키로 저장된 값이 없을 경우 반환할 기본 값           
            int chr_level = PlayerPrefs.GetInt("Chr_Level" + (i - 1).ToString(), 1);
            int locked;     //locked 선택 창 해금할지 말지 하는 변수, 0이면 해금.
            if (i == 1) {
                locked = 0;
            } else {
                locked = PlayerPrefs.GetInt("Chr_Locked" + (i - 1).ToString(), 1);
            }
            //for문 안에서 지정된 세팅을 ship[i-1]에 지정.
            ships[i - 1] = new ShipData(id, base_dmg, name, kName, unlockCoin, baseUpgradeCoin, chr_level, locked);
            ships[i - 1].SetDamage();
            ships[i - 1].SetUpgradeCoin();

        }
        //for (int i=0;i<ships.Length;i++)
        //{
        //    ships[i].show();
        //}
        LoadEnemyWave();
    }
    public void LoadEnemyWave() {
        enemyWaveTextAsset = Resources.Load<TextAsset>("enemyWave");      //ship 이라는 엑셀 파일 로드하는 중.
        string[] lines = enemyWaveTextAsset.text.Split('\n');        //'\n' 엔터의미
        //62줄에서 생성자를 통해 대입 함.
        enemyWaves = new EnemyWave[lines.Length - 2];                 //여기 있는 ships 배열이 메뉴아이템의 데이터임.for문 돌면서 완성됨.
        for (int i = 1; i < lines.Length - 1; i++) {
            string[] rows = lines[i].Split("\t");
            int stage = int.Parse(rows[0]);
            int type = int.Parse(rows[1]);
            float time = int.Parse(rows[2]);

            //for문 안에서 지정된 세팅을 ship[i-1]에 지정.
            enemyWaves[i - 1] = new EnemyWave(stage, type, time);
        }
        
    }
    public float GetCoin() {
        this.coin = PlayerPrefs.GetFloat("TotalCoin", 0);
        return this.coin;
    }
    public void AddCoinInMenu(float coin) {
        //PlayerPrefs.SetFloat-로컬저장소에서 불어옴
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        MenuManager.Instance.coinImage.gameObject.SetActive(true);
        MenuManager.Instance.coinText.gameObject.SetActive(true);
    }
    public void AddCoin(float coin) {
        //PlayerPrefs.SetFloat-로컬저장소에서 불어옴
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
    }
    //11-7
    public bool CanUnlock(int id) {
        if (GetCoin() > ships[id].unlockCoin) {
            if (ships[id].GetLock() == 1) {
                return true;
            } else {
                print("코인은 있지만 락이 해제됨");
                return false;
            }
        } else {
            print("코인이 없음");
            return false;
        }
    }

    public void ExcuteUnlock(int id) {
        AddCoinInMenu(-ships[id].unlockCoin);
        ships[id].SetLock(0);
    }
    public bool CanUpgrade(int id) {
        if (GetCoin() > ships[id].unlockCoin) {
            return true;
        } else {
            return false;
        }
    }
    public void UpgradeAction(int id) {
        AddCoinInMenu(-ships[id].upgradeCoin);
        ships[id].AddChrLevel();
    }
    public List<EnemyWave> GetStageWave(int stage) {
        List<EnemyWave> list = new List<EnemyWave>();
        for (int i = 0; i < enemyWaves.Length; i++) {
            if (enemyWaves[i].stage >= 3) {
                list.Add(enemyWaves[i]);
            }
        }
        return list;
    }
}
