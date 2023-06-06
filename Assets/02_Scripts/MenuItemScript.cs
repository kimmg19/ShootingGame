using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//11-4
public class MenuItemScript : MonoBehaviour
{
    public Button unlockButton;
    public int id;
    public Text shitNameText;
    public Text levelText;
    public Text dmgText;
    public Text nextDmgText;
    public Image shipImage;

    //11-5
    public void SetUI(string shipName,string shipLevel,
        string shipdmg,string shipNextDmg)
    {
        this.shitNameText.text = shipName;
        this.levelText.text = shipLevel;
        this.dmgText.text = shipdmg;
        this.nextDmgText.text = shipNextDmg;

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
