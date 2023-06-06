using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject item;
    public GameObject content;


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
    }
    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
