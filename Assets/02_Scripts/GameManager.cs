using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text coinText;                   //����Ƽ���� text ����.
    public GameObject pausePanel;
    public GameObject asteroid;
    public float time = 0;
    public float maxTime = 2;
    public List<GameObject> enemies;        //����Ƽ���� ������ ����.
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
        coinText.text = GameDataScript.instance.GetCoin().ToString();    //���� 0���� �ʱ�ȭ. coinText.text�� string ���̱� ������ ToString�� ���� ������.
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;        //�ػ󵵰� ���� �� �� ���� ��ġ ���ϰ�.
    }

    void Update()
    {
        //2�ʿ� �ѹ��� ���༺,�� ����
        time += Time.deltaTime;
        if (time > maxTime)
        {
            int check = Random.Range(0, 2);
            if(check == 0)
            {
                //���༺ ����
                Vector3 vec = new Vector3(maxRight+2, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(asteroid, vec, Quaternion.identity);
            }
            else
            {
                //�� ����-0~2�� �ϳ� �޾� instantiate �Լ��� ���� switch�� �� ����.
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
