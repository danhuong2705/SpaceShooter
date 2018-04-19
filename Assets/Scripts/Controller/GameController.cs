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
    private int score;
    private bool gameOver;
    private bool restart;
    Vector3 position;
    bool isBossAppear;
    // Use this for initialization
    void Start()
    {
        gameOver = false;
        restart = false;
        isBossAppear = false;
        UpdateScore();
        InvokeRepeating("SpawnWaves", 2, Random.Range(1, 3));
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
    }


    int countSpawnTimes;
    void SpawnWaves()
    {
        countSpawnTimes++;
        if (countSpawnTimes < 10000)
        {
            if (!gameOver&& !isBossAppear)
            {
                GameObject hazard = NewObjectPooled.current.GetEnemys();
                position.x = Random.Range(-spawnValue.x, spawnValue.x);
                position.y = spawnValue.y;
                position.z = spawnValue.z;
                Vector3 spawnPosition = position;
             //   Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                hazard.transform.position = spawnPosition;
                hazard.transform.rotation = spawnRotation;
                hazard.SetActive(true);
                if(score > 200)
                {
                    GameObject hazard1 = NewObjectPooled.current.GetEnemys();
                    position.x = Random.Range(-spawnValue.x, spawnValue.x);
                    position.y = spawnValue.y;
                    position.z = spawnValue.z;
                    Vector3 spawnPosition1 = position;
                    //   Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                    Quaternion spawnRotation1 = Quaternion.identity;
                    hazard1.transform.position = spawnPosition1;
                    hazard1.transform.rotation = spawnRotation1;
                    hazard1.SetActive(true);
                }
            }
            if(score > 400)
            {
                isBossAppear = true;
               
                
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
