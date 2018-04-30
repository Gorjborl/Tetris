using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarGame : MonoBehaviour {

    public GameObject PlayerCar;
    private GameObject EnemyCar;
    private GameObject EnemyCar2;
    public Text ScoreText;
    public Text LevelText;
    float Score = 0;
    float ScorePerLevel = 0;
    int ScorePerLevelRounded = 0;
    float ScorePerSpeed = 0;
    int ScorePerSpeedRounded = 0;
    int ScoreRounded = 0;
    public int Level = 0;
    float ScoreNeeded = 0;
    int Multiplier = 0;
    int Multiplier2 = 0;
    int ScoreToShow = 0;

    private int CurrentScore = 0;
    private int startingHighScore;
    private int startingHighScore2;
    private int startingHighScore3;
    private int startingHighScore4;
    private int startingHighScore5;

    private bool IsPaused;
    private bool IsPausedinit = false;
    public Button ResumeButton;
    private bool ResumeClick;
    public Canvas Pause_Canvas;
    private float EnemySpeed;


    private void Awake()
    {
        IsPaused = IsPausedinit;
        Time.timeScale = 1;


    }

    // Use this for initialization
    void Start () {

        SpawnEnemy();
        //SpawnEnemy2();
        ScoreText.text = ScoreRounded.ToString();
        LevelText.text = Level.ToString();

    

        startingHighScore = PlayerPrefs.GetInt("HighScoreCar");
        startingHighScore2 = PlayerPrefs.GetInt("HighScoreCar2");
        startingHighScore3 = PlayerPrefs.GetInt("HighScoreCar3");
        startingHighScore4 = PlayerPrefs.GetInt("HighScoreCar4");
        startingHighScore5 = PlayerPrefs.GetInt("HighScoreCar5");


    }
	
	// Update is called once per frame
	void Update () {

        CheckUserInput();
        UpdateScore();
        UpdateScorePerLevel();
        UpdateScorePerSpeed();
        UpdateCorrentScore();
        ScoreText.text = ScoreToShow.ToString();
        LevelText.text = Level.ToString();
        EnemySpeed = FindObjectOfType<EnemyScript>().Speed;
        ResumeClick = false;
        ResumeButton.onClick.AddListener(ClickResume);


    }

    void ClickResume()
    {
        ResumeClick = true;
    }



    public void UpdateHighScore()
    {
        
        if (CurrentScore > startingHighScore)
        {
            PlayerPrefs.SetInt("HighScoreCar5", startingHighScore4);
            PlayerPrefs.SetInt("HighScoreCar4", startingHighScore3);
            PlayerPrefs.SetInt("HighScoreCar3", startingHighScore2);
            PlayerPrefs.SetInt("HighScoreCar2", startingHighScore);
            PlayerPrefs.SetInt("HighScoreCar", CurrentScore);
        }
        else if (CurrentScore > startingHighScore2)
        {
            PlayerPrefs.SetInt("HighScoreCar5", startingHighScore4);
            PlayerPrefs.SetInt("HighScoreCar4", startingHighScore3);
            PlayerPrefs.SetInt("HighScoreCar3", startingHighScore2);
            PlayerPrefs.SetInt("HighScoreCar2", CurrentScore);
        }
        else if (CurrentScore > startingHighScore3)
        {
            PlayerPrefs.SetInt("HighScoreCar5", startingHighScore4);
            PlayerPrefs.SetInt("HighScoreCar4", startingHighScore3);
            PlayerPrefs.SetInt("HighScoreCar3", CurrentScore);
        }
        else if (CurrentScore > startingHighScore4)
        {
            PlayerPrefs.SetInt("HighScoreCar5", startingHighScore4);
            PlayerPrefs.SetInt("HighScoreCar4", CurrentScore);
        }
        else if (CurrentScore > startingHighScore5)
        {
            PlayerPrefs.SetInt("HighScoreCar5", CurrentScore);
        }

    }

    void CheckUserInput()
    {
        

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (PlayerCar.transform.position != new Vector3 (1, 1, 0))
            {
                PlayerCar.transform.position += new Vector3(-3, 0, 0);
            }

        } else 
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
            
        {
            if (PlayerCar.transform.position != new Vector3(7, 1, 0))
            {
                PlayerCar.transform.position += new Vector3(3, 0, 0);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) || ResumeClick)
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
                Pause_Canvas.gameObject.SetActive(true);

            }
            else
            {
                ResumeGame();
                Pause_Canvas.gameObject.SetActive(false);
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }


    public void SpawnEnemy()
    {
        EnemyCar = (GameObject)Instantiate(Resources.Load(GetRandomEnemy(), typeof(GameObject)), GetRandomEnemyPosition(), Quaternion.identity);
    }

    public void SpawnEnemy2()
    {
        EnemyCar2 = (GameObject)Instantiate(Resources.Load(GetRandomEnemy2(), typeof(GameObject)), GetRandomEnemyPosition2(), Quaternion.identity);
    }

    string GetRandomEnemy()
    {
        int randomEnemy = Random.Range(1, 3);
        string randomEnemyName = "EnemyCar1";
        switch (randomEnemy)
        {
            case 1:
                randomEnemyName = "EnemyCar1";
                break;
            case 2:
                randomEnemyName = "EnemyCar2";
                break;
            case 3:
                randomEnemyName = "EnemyCar3";
                break;
                
        }
        return randomEnemyName;
    }

    Vector3 GetRandomEnemyPosition()
    {
        int randomPosition = Random.Range(1, 4);
        Vector3 RandomPositionVector = new Vector3(1,25,0);
        switch (randomPosition)
        {
            case 1:
                RandomPositionVector = new Vector3(1, 25, 0);
                break;
            case 2:
                RandomPositionVector = new Vector3(4, 25, 0);
                break;
            case 3:
                RandomPositionVector = new Vector3(7, 25, 0);
                break;

        }
        return RandomPositionVector;
    }

    string GetRandomEnemy2()
    {
        int randomEnemy = Random.Range(4, 6);
        string randomEnemyName = "EnemyCar4";
        switch (randomEnemy)
        {
            case 1:
                randomEnemyName = "EnemyCar4";
                break;
            case 2:
                randomEnemyName = "EnemyCar5";
                break;
            case 3:
                randomEnemyName = "EnemyCar6";
                break;

        }
        return randomEnemyName;
    }

    Vector3 GetRandomEnemyPosition2()
    {
        int randomPosition = Random.Range(1, 4);
        Vector3 RandomPositionVector = new Vector3(1, 25, 0);
        switch (randomPosition)
        {
            case 1:
                RandomPositionVector = new Vector3(1, 25, 0);
                break;
            case 2:
                RandomPositionVector = new Vector3(4, 25, 0);
                break;
            case 3:
                RandomPositionVector = new Vector3(7, 25, 0);
                break;

        }
        return RandomPositionVector;
    }


    void UpdateScorePerLevel()
    {
        ScorePerLevel += 1.2f * Level;
        ScorePerLevelRounded = Mathf.RoundToInt(ScorePerLevel);
    }

    void UpdateScorePerSpeed()
    {
        ScorePerSpeed += -1.8f * EnemySpeed;
        ScorePerSpeedRounded = Mathf.RoundToInt(ScorePerSpeed);
    }

    
    void UpdateScore()
    {
        
        
        Score += (20 * Time.fixedDeltaTime);
        
        ScoreRounded = Mathf.RoundToInt(Score);

        
    }

    void UpdateCorrentScore()
    {
        CurrentScore = ScoreRounded + ScorePerLevelRounded + ScorePerSpeedRounded;
        

        if (Level < 4)
        {
            if (CurrentScore >= Multiplier * 500)
            {
                ScoreToShow = CurrentScore;
                Multiplier++;
            }
        } else if (Level >= 4)
        {
            if (CurrentScore >= Multiplier2 * 1500)
            {
                ScoreToShow = CurrentScore;
                Multiplier2++;
            }
        }
        

        UpdateLevel();
    }

    void UpdateLevel()
    {
        if (CurrentScore >= ScoreNeeded)
        {
            Level++;
            
            UpdateScoreNeeded();
            
        }
    }

    void UpdateScoreNeeded()
    {
        if (Level == 0)
        {
            ScoreNeeded = 200;
        } else
        {
            ScoreNeeded = 200 * Level * Level + 6 * Level + 100 + 100 * Level * Level * Level * 2;
        }
        
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("LastScoreCar", CurrentScore);
        UpdateHighScore();
        SceneManager.LoadScene("CarGameOver");

    }

}
