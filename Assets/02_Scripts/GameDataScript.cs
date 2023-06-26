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
    public float coin;      //��ü����-TotalCoin
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
    //������Ƽ ��� ����;
    //int selectedValue = select; ������Ƽ�� get �����ڸ� ����Ͽ� ���� ������
    //select = 2; ������Ƽ�� set �����ڸ� ����Ͽ� ���� ������
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
        shipTextAsset = Resources.Load<TextAsset>("ship");      //ship �̶�� ���� ���� �ε��ϴ� ��.
        string[] lines = shipTextAsset.text.Split('\n');        //'\n' �����ǹ�
        //62�ٿ��� �����ڸ� ���� ���� ��.
        ships = new ShipData[lines.Length - 2];                 //���� �ִ� ships �迭�� �޴��������� ��������.for�� ���鼭 �ϼ���.
        for (int i = 1; i < lines.Length - 1; i++) {
            string[] rows = lines[i].Split("\t");
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            float unlockCoin = float.Parse(rows[4]);
            float baseUpgradeCoin = float.Parse(rows[5]);
            //PlayerPrefs.GetInt-��������ҿ� ����,�� ��° �Ű������� �ش� Ű�� ����� ���� ���� ��� ��ȯ�� �⺻ ��           
            int chr_level = PlayerPrefs.GetInt("Chr_Level" + (i - 1).ToString(), 1);
            int locked;     //locked ���� â �ر����� ���� �ϴ� ����, 0�̸� �ر�.
            if (i == 1) {
                locked = 0;
            } else {
                locked = PlayerPrefs.GetInt("Chr_Locked" + (i - 1).ToString(), 1);
            }
            //for�� �ȿ��� ������ ������ ship[i-1]�� ����.
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
        enemyWaveTextAsset = Resources.Load<TextAsset>("enemyWave");      //ship �̶�� ���� ���� �ε��ϴ� ��.
        string[] lines = enemyWaveTextAsset.text.Split('\n');        //'\n' �����ǹ�
        //62�ٿ��� �����ڸ� ���� ���� ��.
        enemyWaves = new EnemyWave[lines.Length - 2];                 //���� �ִ� ships �迭�� �޴��������� ��������.for�� ���鼭 �ϼ���.
        for (int i = 1; i < lines.Length - 1; i++) {
            string[] rows = lines[i].Split("\t");
            int stage = int.Parse(rows[0]);
            int type = int.Parse(rows[1]);
            float time = int.Parse(rows[2]);

            //for�� �ȿ��� ������ ������ ship[i-1]�� ����.
            enemyWaves[i - 1] = new EnemyWave(stage, type, time);
        }
        
    }
    public float GetCoin() {
        this.coin = PlayerPrefs.GetFloat("TotalCoin", 0);
        return this.coin;
    }
    public void AddCoinInMenu(float coin) {
        //PlayerPrefs.SetFloat-��������ҿ��� �Ҿ��
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        MenuManager.Instance.coinImage.gameObject.SetActive(true);
        MenuManager.Instance.coinText.gameObject.SetActive(true);
    }
    public void AddCoin(float coin) {
        //PlayerPrefs.SetFloat-��������ҿ��� �Ҿ��
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
    }
    //11-7
    public bool CanUnlock(int id) {
        if (GetCoin() > ships[id].unlockCoin) {
            if (ships[id].GetLock() == 1) {
                return true;
            } else {
                print("������ ������ ���� ������");
                return false;
            }
        } else {
            print("������ ����");
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
