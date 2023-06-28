using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public enum Music { Menu,Play};
    public enum Sound { PlayerShot, Explosion,Coin };

    public delegate void Onclick();
    //12-16~17
    public class Util {
        public static void SetDouble(string key, double value) {
            PlayerPrefs.SetString(key, DoubleToString(value));
        }
        public static double GetDouble(string key, double defaultValue) {
            string defaultVal = DoubleToString(defaultValue);
            return StringToDouble(PlayerPrefs.GetString(key, defaultVal));
        }
        public static double GetDouble(string key) {
            return GetDouble(key, 0d);
        }

        private static string DoubleToString(double target) {
            return target.ToString("G17");
        }
        private static double StringToDouble(string target) {
            if (string.IsNullOrEmpty(target))
                return 0d;

            return double.Parse(target);
        }
        static string[] digit = new string[] { "", "k", "m", "g", "t", "p", "e", "z", "y",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

        public static string GetBigNumber(double number) {
            if (number < 1000) {
                return number.ToString();
            }
            double expNum;
            int powNum;
            string numStr = number.ToString("E");
            //1.000000E+003
            string[] parts = numStr.Split('+');
            if (parts.Length < 2) {
                return "";
            } else {
                string expPart = parts[0].Remove(parts[0].Length - 1);
                string powPart = parts[1];
                expNum = double.Parse(expPart);
                powNum = int.Parse(powPart);
                int index = powNum / 3;
                int multiple = powNum % 3;
                expNum = expNum * Mathf.Pow(10, multiple);
                string firstStr = string.Format("{0:n3}", expNum);
                string secondStr = digit[index];
                string result = string.Concat(firstStr, secondStr);
                return result;
            }
        }
        public static void CreatePopup(string title, string detail,
            Onclick yesAction, Onclick noAction) {
            GameObject popupObj = Resources.Load<GameObject>("Popup");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject popup = GameObject.Instantiate(popupObj, canvas.transform, false);
            PopupScript popupScript = popup.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
            popupScript.SetNoListener(noAction);
        }
        public static void CreatePopup(string title, string detail,
            Onclick yesAction) {
            GameObject popupObj = Resources.Load<GameObject>("PopupOkay");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject popup = GameObject.Instantiate(popupObj, canvas.transform, false);
            PopupScript popupScript = popup.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
        }
    }//여까지

    public struct ShipData {
        public int id; public double base_dmg; public string name;
        public string kName;
        public int chr_level;
        public int locked;      //비행기 하나하나의 해금 관리
        public double dmg;
        public double nextDmg;
        public double unlockCoin;
        public double upgradeCoin;
        public double base_upgradecoin;
        public ShipData(int id, double base_dmg, string name, string kName,
            double unlockCoin, double base_upgradecoin,
            int chr_level = 1, int locked = 1, double dmg = 1, double nextDmg = 1, double upgradeCoin = 100) {
            this.id = id;
            this.base_dmg = base_dmg;
            this.name = name;
            this.kName = kName;
            this.unlockCoin = unlockCoin;
            this.base_upgradecoin = base_upgradecoin;
            this.chr_level = chr_level;
            this.locked = locked;
            this.dmg = dmg;
            this.nextDmg = nextDmg;
            this.upgradeCoin = upgradeCoin;
        }
        //11-6
        public string GetImageName() {
            return "Character/" + id.ToString() + "/0";
        }
        //chr_level 추가시 이 함수 꼭 실행.-레벨 증가할때
        public void SetDamage() {
            this.dmg = chr_level * base_dmg;
            this.nextDmg = (chr_level + 1) * base_dmg;
        }
        public void SetUpgradeCoin() {
            this.upgradeCoin = chr_level * base_upgradecoin;
        }
        public void show() {
            Debug.Log("id: " + id + " base_dmg: " + base_dmg + " name: " + name +
                " kName: " + kName + "unlockCoin: " + unlockCoin + " chr_level: " + chr_level + " locked: " + locked
                + " dmg: " + dmg + " base_upgradecoin: " + base_upgradecoin + " upgradeCoin: " + upgradeCoin);
        }
        public void SetLock(int locked) {
            if (id == 0) {
                locked = 0;
            }
            this.locked = locked;
            PlayerPrefs.SetInt("Chr_Locked" + id.ToString(), locked);
        }
        public int GetLock() {
            if (id == 0) {
                return 0;
            } else {
                this.locked = PlayerPrefs.GetInt("Chr_Locked" + id.ToString(), 1);
                return this.locked;
            }

        }
        public void AddChrLevel() {
            chr_level++;
            PlayerPrefs.SetInt("Chr_Level" + id.ToString(), chr_level);
            SetDamage();
            SetUpgradeCoin();
        }
    }
    //아래 내용이 유니티 인스펙터 뷰에서 보인다.
    [System.Serializable]
    public struct EnemyWave {
        public int stage;
        public int type;
        public float time;
        public EnemyWave(int stage, int type, float time) {
            this.stage = stage;
            this.type = type;
            this.time = time;
        }
        public void Show() {
            Debug.Log("stage : " + stage + "type : " + type + "time : " + time);
        }
    }
    [System.Serializable]
    public struct Enemy {
        public int id;
        public string name;
        public double hp;
        public float speed;
        public float maxShotTime;
        public float shotSpeed;
        public double coin;
        public Enemy(int id, string name, double hp, float speed, float maxShotTime,
            float shotSpeed, double coin) {
            this.id = id;
            this.name = name;
            this.hp = hp;
            this.speed = speed;
            this.maxShotTime = maxShotTime;
            this.shotSpeed = shotSpeed;
            this.coin = coin;
        }
        public void Show() {
            Debug.Log("id : " + id + "name : " + name + "hp : " + hp + "speed : " + speed +
                "maxShotTime : " + maxShotTime + "shotSpeed : " + shotSpeed + "coin : " + coin);
        }
    }
}