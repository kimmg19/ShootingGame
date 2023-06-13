using Game;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

//11-4.MenuScene/Canvas/Panel/Scroll View/Viewport/Content/MenuItem
public class MenuItemScript : MonoBehaviour
{
    //유니티에서 UI와 연결됨.
    public Button unlockButton;
    public Text unlockCoinText;
    public int id;
    public Text shipNameText;
    public Text levelText;
    public Text dmgText;
    public Text nextDmgText;
    public Image shipImage;
    public GameObject popupObj;

    //11-5
    public void SetUI(string shipName,string shipLevel,
        string shipdmg,string shipNextDmg,int locked,float unlockCoin)
    {
        this.shipNameText.text = shipName;
        this.levelText.text = shipLevel;
        this.dmgText.text = shipdmg;
        this.nextDmgText.text = shipNextDmg.ToString()+" Coin";
        unlockCoinText.text=unlockCoin.ToString();

        if (locked == 1)
        {
            unlockButton.gameObject.SetActive(true);//잠김
            unlockCoinText.gameObject.SetActive(true);
        }
        else
        {
            unlockButton.gameObject.SetActive(false);//풀림
            unlockCoinText.gameObject.SetActive(false);
        }
    }

    public void UnlockAction()
    {
        print("UnlockAction");
        if (GameDataScript.instance.CanUnlock(id))
        {
            //12-16
            Util.CreatePopup("구매", GameDataScript.instance.ships[id].kName + "을(를) 구매하시겠습니까?",
                ItemYesAction, ItemNoAction);
        }
        else
        {
            Util.CreatePopup("확인", "코인이 부족합니다.", CoinLackAction);
        }
    }
    public void CoinLackAction()
    {

    }
    public void ItemYesAction()
    {
        GameDataScript.instance.ExcuteUnlock(id);
        unlockButton.gameObject.SetActive(false);
        unlockCoinText.gameObject.SetActive(false);
        MenuManager.Instance.coinText.text = GameDataScript.instance.GetCoin().ToString();
    }

    public  void ItemNoAction()
    {

    }
    public void PowerUpAction()
    {
        print("PowerUpAction");
    }
}
