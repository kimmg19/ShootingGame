using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class GameDataScript : MonoBehaviour
{
    TextAsset shipTextAsset;
    public ShipData[] ships;
    public static GameDataScript instance;
    public float coin;      //��ü����-TotalCoin
    private int _select;
    public int select
    {
        get
        {
            _select = PlayerPrefs.GetInt("ChrSelect", 0);
            return _select;
        }
        set
        {
            _select=value;
            PlayerPrefs.SetInt("ChrSelect",_select);
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        shipTextAsset = Resources.Load<TextAsset>("ship");      //ship �̶�� ���� ���� �ε��ϴ� ��.
        string[] lines = shipTextAsset.text.Split('\n');        //'\n' �����ǹ�
        //62�ٿ��� �����ڸ� ���� ���� ��.
        ships = new ShipData[lines.Length - 2];                 //���� �ִ� ships �迭�� �޴��������� ��������.for�� ���鼭 �ϼ���.
        for(int i = 1;i< lines.Length-1; i++)
        {
            string[] rows = lines[i].Split("\t");
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            float unlockCoin = float.Parse(rows[4]);
            //PlayerPrefs.GetInt-��������ҿ� ����,�� ��° �Ű������� �ش� Ű�� ����� ���� ���� ��� ��ȯ�� �⺻ ��           
            int chr_level = PlayerPrefs.GetInt("Chr_Level" + (i - 1).ToString(), 1);
            int locked;     //locked ���� â �ر����� ���� �ϴ� ����, 0�̸� �ر�.
            if (i == 1)
            {
                locked = 0;
            }
            else
            {
                locked=PlayerPrefs.GetInt("Chr_Locked"+(i-1).ToString(), 1);
            }
            //for�� �ȿ��� ������ ������ ship[i-1]�� ����.
            ships[i-1] =new ShipData(id,base_dmg, name, kName,unlockCoin, chr_level, locked);
            ships[i - 1].SetDamage();
        }
    }
    public float GetCoin()
    {
        this.coin=PlayerPrefs.GetFloat("TotalCoin", 0);
        return this.coin;
    }
    public void AddCoin(float coin)
    {
        //PlayerPrefs.SetFloat-��������ҿ��� �Ҿ��
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        MenuManager.Instance.coinImage.gameObject.SetActive(true);
        MenuManager.Instance.coinText.gameObject.SetActive(true);

    }
    //11-7
    public bool CanUnlock(int id)
    {
        if (GetCoin() > ships[id].unlockCoin)
        {
            if (ships[id].GetLock() == 1)
            {
                return true;
            }
            else
            {
                print("������ ������ ���� ������");
                return false;
            }
        }
        else
        {
            print("������ ����");
            return false;
        }
    }

    public void ExcuteUnlock(int id)
    {
        AddCoin(-ships[id].unlockCoin);
        ships[id].SetLock(0);
    }
}
