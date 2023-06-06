using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class GameDataScript : MonoBehaviour
{
    TextAsset shipTextAsset;
    public ShipData[] ships;
    public static GameDataScript instance;
    public float coin;      //전체코인-TotalCoin
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
        shipTextAsset = Resources.Load<TextAsset>("ship");      //ship 이라는 엑셀 파일 로드하는 중.
        string[] lines = shipTextAsset.text.Split('\n');        //'\n' 엔터의미
        ships = new ShipData[lines.Length - 2];
        for(int i = 1;i< lines.Length-1; i++)
        {
            string[] rows = lines[i].Split("\t");
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            int chr_level = PlayerPrefs.GetInt("Chr_Level" + i.ToString(), 1);
            int locked = PlayerPrefs.GetInt("Chr_Locked" + i.ToString(), 1);
            if (locked == 1)
            {
                locked = 0;
            }
            else
            {
                //영상에서 오류 있다고 어쩌구 함.섹션10-10 - 7:40초
                locked=PlayerPrefs.GetInt("Chr_Locked"+i.ToString(), 1);
            }
            ships[i-1] =new ShipData(id,base_dmg, name, kName, chr_level, locked);
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
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
    }
}
