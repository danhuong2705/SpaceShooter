using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private InputField inputName;
    [SerializeField]
    private Button btnPause;
    [SerializeField]
    private Sprite imagePause;
    [SerializeField]
    private Sprite imagePlay;
    //  Slider slider = FightBoss.current.bloodSlider;
    // Use this for initialization
    void Awake()
    {
        current = this;
    }
    void Start()
    {
        GameObject img = btnPause.transform.Find("Image").gameObject;
        img.GetComponent<Image>().sprite = imagePause;
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
                if(score < 50)
                {
                    GetHazards(1);
                }
                else if(score >=50 && score <=100)
                {
                    GetHazards(2);
                }
                else
                {
                    GetHazards(3);
                }
                if (score >= 200)
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
              
                restart = true;
            }
        }
        else
        {
            CancelInvoke();
        }
    }
void GetHazards(int n)
    {
        for(int i = 0; i < n; i++)
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
        }
       

    }

void FightBoss()
    {
        //position.x = 0;
        //position.y = 0;
        //position.z = 16;
        //boss.transform.position = position;
        //Quaternion spawnRotation = Quaternion.identity;
      //  boss.transform.rotation = spawnRotation;
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
    public void Restart()
    {
        restartText.text = "Press 'R' for Restart";
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
     //   InputName();
        Restart(); 
    }
    void InputName()
    {
        List<HighScore> listHighScore = GetHighScores.current.GetHighScoreList();
        if (listHighScore.Count <= 0)
        {
            inputName.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < listHighScore.Count; i++)
            {
                if (score > listHighScore[i].Score)
                {
                    inputName.gameObject.SetActive(true);
                }
            }
        }
       
    }
    bool paused = false;

 
    public void OnBtnPauseClick()
    {
       
            paused = togglePause();
    }
    //void OnGUI()
    //{
    //    if (paused)
    //    {
    //        GUILayout.Label("Game is paused!");
    //        if (GUILayout.Button("Click me to unpause"))
    //            paused = togglePause();
    //    }
    //}

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            GameObject img = btnPause.transform.Find("Image").gameObject;
            img.GetComponent<Image>().sprite = imagePause;
            SceneManager.LoadScene("Main");
            return (false);
            

        }
        else
        {
            Time.timeScale = 0f;
            GameObject img = btnPause.transform.Find("Image").gameObject;
            img.GetComponent<Image>().sprite = imagePlay ;
            SceneManager.LoadScene("HomeScreen");
            return (true);
        }
    }
}
