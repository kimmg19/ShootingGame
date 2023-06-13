using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Game
{
    public delegate void Onclick();
    //12-16~17
    public class Util
    {
        public static void CreatePopup(string title, string detail,
            Onclick yesAction,Onclick noAction)
        {
            GameObject popupObj= Resources.Load<GameObject>("Popup");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject popup = GameObject.Instantiate(popupObj, canvas.transform, false);
            PopupScript popupScript = popup.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
            popupScript.SetNoListener(noAction);
        }
        public static void CreatePopup(string title, string detail,
            Onclick yesAction)
        {
            GameObject popupObj = Resources.Load<GameObject>("PopupOkay");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject popup = GameObject.Instantiate(popupObj, canvas.transform, false);
            PopupScript popupScript = popup.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
        }
    }//여까지
    public struct ShipData
    {
        public int id; public float base_dmg; public string name;
        public string kName;
        public int chr_level;
        public int locked;      //비행기 하나하나의 해금 관리
        public float dmg;
        public float nextDmg;
        public float unlockCoin;
        public ShipData(int id, float base_dmg, string name, string kName,
            float unlockCoin,
            int chr_level = 1,int locked = 1, float dmg = 1,float nextDmg=1)
        {
            this.id = id;
            this.base_dmg = base_dmg;
            this.name = name;
            this.kName = kName;
            this.unlockCoin = unlockCoin;
            this.chr_level = chr_level;
            this.locked = locked;
            this.dmg = dmg;
            this.nextDmg = nextDmg;
            
        }
        //11-6
        public string GetImageName()
        {
            return "Character/" + id.ToString() + "/0";
        }
        //chr_level 추가시 이 함수 꼭 실행.-레벨 증가할때
        public void SetDamage()
        {
            this.dmg = chr_level * base_dmg;
            this.nextDmg = (chr_level + 1) * base_dmg;
        }

        public void show()
        {
            Debug.Log("id: " + id + " base_dmg: " + base_dmg + " name: " + name +
                " kName: " + kName+"unlockCoin: "+unlockCoin+" chr_level: "+chr_level+" locked: "+locked 
                +" dmg: "+dmg);
        }
        public void SetLock(int locked)
        {
            if (id == 0)
            {
                locked = 0;
            }
            this.locked= locked;
            PlayerPrefs.SetInt("Chr_Locked" + id.ToString(),locked);
        }
        public int GetLock()
        {
            if (id == 0)
            {
                return 0;
            }
            else {
                this.locked = PlayerPrefs.GetInt("Chr_Locked" + id.ToString(), 1);
                return this.locked;
            }
            
        }
    }
}