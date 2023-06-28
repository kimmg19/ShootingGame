
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;
using UnityEngine.UI;

//MenuScene/MenuManager
public class MenuManager : MonoBehaviour {
    public GameObject item; //캐릭터 선택 창
    public GameObject content;
    public GameObject addButtonObj;
    public GameObject clearButtonObj;
    public Text coinText;
    public Image coinImage;
    public static MenuManager Instance;
    public Button startButton;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        startButton.onClick.AddListener(GoGameScene);
        int shipLength = GameDataScript.instance.ships.Length;
        for (int i = 0; i < shipLength; i++) {
            //11-5
            ShipData ship = GameDataScript.instance.ships[i];
            GameObject obj = Instantiate(item, transform.position, Quaternion.identity);    //메뉴 하나의 아이템이 만들어짐.
            MenuItemScript curItem = obj.GetComponent<MenuItemScript>();
            //아이템(선택 창)의 UI적용
            curItem.SetUI(ship.name, ship.chr_level.ToString(), ship.dmg.ToString(), ship.nextDmg.ToString(), ship.locked,
                ship.unlockCoin, ship.upgradeCoin);
            curItem.id = ship.id;       //MenuItemScript의 id 를 여기서 바꿈.0~2
            obj.name = i.ToString();
            obj.transform.SetParent(content.transform, false);
            obj.SetActive(true);

            //11-6
            curItem.shipImage.sprite = Resources.Load<Sprite>(
                ship.GetImageName());
            GetComponent<ScrollViewSnap>().item.Add(obj);
        }
        if (GameDataScript.instance.GetCoin() == 0) {
            coinText.gameObject.SetActive(false);
            coinImage.gameObject.SetActive(false);
        } else {
            coinText.gameObject.SetActive(true);
            coinImage.gameObject.SetActive(true);
            coinText.text = GameDataScript.instance.GetCoin().ToString();

        }
        AudioManagerScript.instance.PlayMusic(Music.Menu);
    }

    public void GoGameScene() {
        if (GameDataScript.instance.CanSelect()) {
            SceneManager.LoadScene("GameScene");
        } else {
            GameDataScript.instance.select = 0;
            SceneManager.LoadScene("GameScene");

        }
    }
    public void Quit() {
        Application.Quit();
    }
    public void AddTestCoin() {
        GameDataScript.instance.AddCoinInMenu(10000);
        coinText.text = GameDataScript.instance.GetCoin().ToString();
    }
    public void ClearPrefAction() {
        PlayerPrefs.DeleteAll();
    }
}
