
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;
using UnityEngine.UI;

//MenuScene/MenuManager
public class MenuManager : MonoBehaviour
{
    public GameObject item;
    public GameObject content;
    public GameObject addButtonObj;
    public GameObject clearButtonObj;
    public Text coinText;
    public Image coinImage;
    private void Start()
    {
        int shipLength=GameDataScript.instance.ships.Length;
        for (int i = 0; i < shipLength; i++)
        {
            //11-5
            ShipData ship= GameDataScript.instance.ships[i];
            GameObject obj=Instantiate(item,transform.position,Quaternion.identity);
            MenuItemScript curItem= obj.GetComponent<MenuItemScript>();
            curItem.SetUI(ship.name, ship.chr_level.ToString(), ship.dmg.ToString(), ship.nextDmg.ToString());
            curItem.id = ship.id;
            obj.name=i.ToString();
            obj.transform.SetParent(content.transform,false);
            obj.SetActive(true);

            //11-6
            curItem.shipImage.sprite=Resources.Load<Sprite>(
                ship.GetImageName());
            GetComponent<ScrollViewSnap>().item.Add(obj);
        }
        if (GameDataScript.instance.GetCoin() == 0)
        {
            coinText.gameObject.SetActive(false);
            coinImage.gameObject.SetActive(false);
        }
        else
        {
            coinText.gameObject.SetActive(true);
            coinImage.gameObject.SetActive(true);
            coinText.text = GameDataScript.instance.GetCoin().ToString();

        }

    }
    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void AddTestCoin()
    {
        GameDataScript.instance.AddCoin(10000);
        coinText.text = GameDataScript.instance.GetCoin().ToString();
    }
    public void ClearPrefAction()
    {
        PlayerPrefs.DeleteAll();
    }
}
