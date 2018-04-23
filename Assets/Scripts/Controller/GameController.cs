using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hazards;
    [SerializeField]
    private Vector3 spawnValue;
    [SerializeField]
    private int hazardCount;
    [SerializeField]
    private float spawnWait;
    [SerializeField]
    private float startWait;
    [SerializeField]
    private float wavesWait;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text restartText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject boss;
    private int score;
    public bool gameOver;
    private bool restart;
    Vector3 position;
    bool isBossAppear;
    public static GameController current;
   
    //  Slider slider = FightBoss.current.bloodSlider;
    // Use this for initialization
    void Awake()
    {
        current = this;
    }
    void Start()
    {
      
        gameOver = false;
        restart = false;
        isBossAppear = false;
        UpdateScore();

        InvokeRepeating("SpawnWaves", 2, Random.Range(1, 3));
       // FightBoss();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart = true;
            
            if (restart)
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                countSpawnTimes = 0;
            }
        }
        if (!boss.activeInHierarchy)
        {
            slider.gameObject.SetActive(false);
        }
    }


    int countSpawnTimes;
    bool isBossFight = false;
    void SpawnWaves()
    {
       
        countSpawnTimes++;
        if (countSpawnTimes < 10000)
        {
            if (!gameOver && !isBossAppear)
            {
                GameObject hazard = NewObjectPooled.current.GetEnemys();
                position.x = Random.Range(-spawnValue.x, spawnValue.x);
                position.y = spawnValue.y;
                position.z = spawnValue.z;
                Vector3 spawnPosition = position;
        
                Quaternion spawnRotation = Quaternion.identity;
                hazard.transform.position = spawnPosition;
                hazard.transform.rotation = spawnRotation;
                hazard.SetActive(true);
                if (score >= 50)
                {
                    GameObject hazard1 = NewObjectPooled.current.GetEnemys();
                    position.x = Random.Range(-spawnValue.x, spawnValue.x);
                    position.y = spawnValue.y;
                    position.z = spawnValue.z;
                    Vector3 spawnPosition1 = position;
                
                    Quaternion spawnRotation1 = Quaternion.identity;
                    hazard1.transform.position = spawnPosition1;
                    hazard1.transform.rotation = spawnRotation1;
                    hazard1.SetActive(true);
                }
                if (score >= 100)
                {
                    CancelInvoke();

                    isBossAppear = true;
                   
                    
                   
                }

                if (isBossAppear)
                {
                    Invoke("FightBoss", 5);
                }

            }

            if (gameOver)
            {
                CancelInvoke();
                restartText.text = "Press 'R' for Restart";
                restart = true;
            }
        }
        else
        {
            CancelInvoke();
        }
    }


void FightBoss()
    {
        position.x = Random.Range(-spawnValue.x, spawnValue.x);
        position.y = spawnValue.y;
        position.z = spawnValue.z;
        Vector3 spawnPosition1 = position;
        Quaternion spawnRotation1 = Quaternion.identity;
        boss.transform.position = position;
        boss.transform.rotation = spawnRotation1;
        slider.gameObject.SetActive(true);
        boss.gameObject.SetActive(true);
      
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

}
