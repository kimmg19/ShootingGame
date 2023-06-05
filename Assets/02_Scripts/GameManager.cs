using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text coinText;                   //유니티에서 text 연결.
    public GameObject pausePanel;
    public GameObject asteroid;
    public float time = 0;
    public float maxTime = 2;
    public List<GameObject> enemies;        //유니티에서 프리팹 연결.
    public static GameManager instance;
    public float coinInGame;
    public float maxRight;
    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        coinInGame = 0;
        coinText.text = GameDataScript.instance.GetCoin().ToString();    //코인 0으로 초기화. coinText.text는 string 형이기 때문에 ToString을 통해 맞춰줌.
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;        //해상도가 변할 때 적 출현 위치 변하게.
    }

    void Update()
    {
        //2초에 한번씩 소행성,적 생성
        time += Time.deltaTime;
        if (time > maxTime)
        {
            int check = Random.Range(0, 2);
            if(check == 0)
            {
                //소행성 생성
                Vector3 vec = new Vector3(maxRight+2, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(asteroid, vec, Quaternion.identity);
            }
            else
            {
                //적 생성-0~2중 하나 받아 instantiate 함수를 통해 switch로 적 생성.
                int type=Random.Range(0, 3);
                Vector3 vec = new Vector3(maxRight+2, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(enemies[type], vec, Quaternion.identity);
            }
            
            
            time = 0;
        }
    }

    public void PauseAction()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeAction()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void MainMenuAction()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }



}
