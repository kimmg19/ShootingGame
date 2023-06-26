using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour {
    public List<GameObject> enemyWavePrefabs;
    public List<EnemyWave> enemyWaves;
    public int spawnIndex = 0;
    public Text coinText;                   //����Ƽ���� text ����.
    public GameObject pausePanel;
    public GameObject asteroid;
    public float time = 0;
    public float spawnTime = 2;
    public List<GameObject> enemies;        //����Ƽ���� ������ ����.
    public static GameManager instance;
    public float coinInGame;
    public float maxRight;
    public GameObject retryPanel;
    public Text coinInRetryText;
    public Text stageInRetryText;
    public Button retryButton;
    public Button mainMenuRetryButton;
    public GameObject coverPanel;
    public GameObject clearPanel;
    public Text stageInClearText;
    public Text coinInClearText;
    public Button adButton;
    public Button nextStageButton;
    public int spawnCount = 0;  //����ī��Ʈ�� �ƽ����� Ŀ����� Ŭ����
    public int spawnMax = 3;
    public int remainEnemy = 0;
    public bool stageClear = false;
    public int stageInGame;
    public bool isAlive = true;     //�÷��̾� ����ֳ�
    private void Awake() {
        instance = this;
    }

    private void Start() {
        isAlive = true;
        stageInGame = GameDataScript.instance.GetStage();
        coinInGame = 0;
        coinText.text = GameDataScript.instance.GetCoin().ToString();           //���� 0���� �ʱ�ȭ. coinText.text�� string ���̱� ������ ToString�� ���� ������.
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;    //�ػ󵵰� ���� �� �� ���� ��ġ ���ϰ�.
        retryButton.onClick.AddListener(RetryAction);
        mainMenuRetryButton.onClick.AddListener(MainMenuInRetryAction);
        adButton.onClick.AddListener(AdAction);
        nextStageButton.onClick.AddListener(NextStageInClearAction);
        enemyWaves=GameDataScript.instance.GetStageWave(stageInGame);
        for(int i=0; i<enemyWaves.Count; i++) {
            enemyWaves[i].Show();
        }
        /*enemyWaves = new List<EnemyWave>();
        enemyWaves.Add(new EnemyWave(0, 0, 2));
        enemyWaves.Add(new EnemyWave(1, 1, 3));
        enemyWaves.Add(new EnemyWave(2, 1, 2));
        enemyWaves.Add(new EnemyWave(3, 0, 3));
        enemyWaves.Add(new EnemyWave(4, 0, 2));*/
        remainEnemy = 0;
        spawnIndex = 0;
        spawnTime = 0;

        SpawnEnemyWave();
    }
    public float asteroidTime = 0;
    public float asteroidSpawnTime = 3;

    void Update() {
        time += Time.deltaTime;
        asteroidTime += Time.deltaTime;
        if (time > spawnTime) {
            if (spawnIndex < enemyWaves.Count) {
                SpawnEnemyWave();
            } else {
                if (remainEnemy <= 0 && stageClear == false && isAlive == true) {
                    stageClear = true;
                    ClearPanelActiveAfter1sec();
                }
            }
        } else if (asteroidTime > asteroidSpawnTime && spawnIndex < enemyWaves.Count) {
            Vector3 vec = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
            GameObject obj = ObjectPoolManager.instance.asteroid.Create();
            obj.transform.position = vec;
            obj.transform.rotation = Quaternion.identity;
            asteroidTime = 0;
        }
        //2�ʿ� �ѹ��� ���༺,�� ����
        /* time += Time.deltaTime;
         if (time > spawnTime){
             if (spawnCount >= spawnMax){
                 if (remainEnemy <= 0 && stageClear==false && isAlive == true)
                 {
                     stageClear = true;
                     ClearPanelActiveAfter1sec();
                 }
             }
             else
             {
                 int check = Random.Range(0, 2);
                 if (check == 0){
                     //���༺ ����
                     Vector3 vec = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
                     //Instantiate(asteroid, vec, Quaternion.identity);
                     GameObject obj = ObjectPoolManager.instance.asteroid.Create();
                     obj.transform.position = vec;
                     obj.transform.rotation = Quaternion.identity;
                 }
                 else{
                     //�� ����-0~2�� �ϳ� �޾� instantiate �Լ��� ���� switch�� �� ����.
                     int type = Random.Range(0, 3);
                     Vector3 vec = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
                     //Instantiate(enemies[type], vec, Quaternion.identity);
                     GameObject obj=ObjectPoolManager.instance.enemies[type].Create();
                     obj.transform.position=vec;
                     obj.transform.rotation = Quaternion.identity;   
                 }
                 spawnCount++;
                 remainEnemy++;
             }
             time = 0;
         }*/
    }
    public void SpawnEnemyWave() {
        int type = enemyWaves[spawnIndex].type;
        spawnTime += enemyWaves[spawnIndex].time;
        int count = enemyWavePrefabs[type].transform.childCount;
        Vector3 vec = new Vector3(0, Random.Range(-3.0f, 3.0f), 0);
        //Instantiate(enemyWavePrefabs[type], vec, Quaternion.identity);
        //14-13
        for (int i = 0; i < count; i++) {
            Transform tr = enemyWavePrefabs[type].transform.GetChild(i).transform;
            EnemyScript enemyPrefabScript = tr.GetComponent<EnemyScript>();
            int enemyType = enemyPrefabScript.type;
            GameObject enemyObj = ObjectPoolManager.instance.enemies[enemyType].Create();
            enemyObj.transform.position = tr.position + vec;
            enemyObj.transform.rotation = Quaternion.identity;
        }
        remainEnemy += count;
        spawnIndex++;
    }
    public void PauseAction() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeAction() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void MainMenuAction() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        print("MainMenuAction");
        SceneManager.LoadScene("MenuScene");
    }
    void RetryAction() {
        retryPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void AdAction() {
        print("AdAction");
    }
    void NextStageInClearAction() {
        SceneManager.LoadScene("GameScene");
    }
    public void MainMenuInRetryAction() {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    public void RetryPanelSetActiveAfter1sec() {
        coverPanel.SetActive(true);
        Invoke("RetryPanelSetActive", 1);//1���� RetryPanelSetActive�Լ� ����
    }
    public void RetryPanelSetActive() {
        coverPanel.SetActive(false);
        stageInRetryText.text = stageInGame.ToString();
        coinInRetryText.text = coinInGame.ToString();
        retryPanel.SetActive(true);
    }

    public void ClearPanelActiveAfter1sec() {
        print("ClearPanelActiveAfter1sec");
        coverPanel.SetActive(true);
        GameDataScript.instance.AddStage();
        Invoke("ClearPanelActive", 1);//1���� ClearPanelActive �Լ� ����
    }
    public void ClearPanelActive() {
        coverPanel.SetActive(false);
        stageInClearText.text = stageInGame.ToString();
        coinInClearText.text = coinInGame.ToString();
        clearPanel.SetActive(true);
    }

}
