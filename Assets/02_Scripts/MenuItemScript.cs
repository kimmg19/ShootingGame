using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//11-4.MenuScene/Canvas/Panel/Scroll View/Viewport/Content/MenuItem
public class MenuItemScript : MonoBehaviour
{
    public Button unlockButton;
    public int id;
    public Text shipNameText;
    public Text levelText;
    public Text dmgText;
    public Text nextDmgText;
    public Image shipImage;

    //11-5
    public void SetUI(string shipName,string shipLevel,
        string shipdmg,string shipNextDmg)
    {
        shipNameText.text = shipName;
        levelText.text = shipLevel;
        dmgText.text = shipdmg;
        nextDmgText.text = shipNextDmg;

    }

    public void UnlockAction()
    {
        print("UnlockAction");
        unlockButton.gameObject.SetActive(false);
    }
    public void PowerUpAction()
    {
        print("PowerUpAction");
    }
}
