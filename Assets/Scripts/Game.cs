using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static int gridWith = 10;
    public static int gridHeight = 20;
    public static Transform[,] grid = new Transform[gridWith, gridHeight];
    public int ScoreOneLine = 40;
    public int ScoreTwoLine = 100;
    public int ScoreThreeLine = 300;
    public int ScoreFourLine = 1200;

    public float fallSpeed = 1.0f;
    public int CurrentLevel = 10;
    private int numLinesCleared = 0;

    private int NumbersOfLinesThisTurn = 0;
    public static int CurrentScore = 0;
    
    public Text hud_score;
    public Text hud_level;
    public Text hud_lines;
    private GameObject previewTetromino;
    private GameObject nextTetris;
    private bool gameStarted = false;
    private Vector2 previewTetrominoPosition = new Vector2(20f, 5f);

    private int startingHighScore;
    private int startingHighScore2;
    private int startingHighScore3;
    private int startingHighScore4;
    private int startingHighScore5;

    public bool IsPaused;
    static bool IsPausedinit = false;
    public GameObject Pause_Canvas;
    public Button ResumeButton;
    private bool ResumeClick;
    private string TetrisName;
    public bool IsAnI;

    private Vector2 DefaultSpawn = new Vector2 (5,19f);
        
    public Button PauseBtn;
    

    
    private bool PauseBtnClick;





    private void Awake()
    {
        IsPaused = IsPausedinit;
        Time.timeScale = 1;
        

    }

    // Use this for initialization
    void Start () {

        
        ResumeButton.GetComponent<Button>();
        spawnNextTetris();
        //GameObject.FindGameObjectWithTag("Tetris_I").transform.position = new Vector2(5, 19.5f);
        hud_score = GameObject.Find("ScorePoints").GetComponent<Text>();
        hud_score.text = CurrentScore.ToString();
        hud_level = GameObject.Find("TetrisLevel").GetComponent<Text>();
        hud_level.text = CurrentLevel.ToString();
        hud_lines = GameObject.Find("LinesNumber").GetComponent<Text>();
        hud_lines.text = numLinesCleared.ToString();
        CurrentScore = 0;
        startingHighScore = PlayerPrefs.GetInt("HighScore");
        startingHighScore2 = PlayerPrefs.GetInt("HighScore2");
        startingHighScore3 = PlayerPrefs.GetInt("HighScore3");
        startingHighScore4 = PlayerPrefs.GetInt("HighScore4");
        startingHighScore5 = PlayerPrefs.GetInt("HighScore5");
        
    }

    

    public void UpdateScore()
    {
        if (NumbersOfLinesThisTurn > 0)
        {
            if (NumbersOfLinesThisTurn == 1)
            {
                ClearedOneLine();
                

            }
            else if (NumbersOfLinesThisTurn == 2)
            {
                ClearedTwoLines();
                
            }
            else if (NumbersOfLinesThisTurn == 3)
            {
                ClearedThreeLines();
                
            }
            else if (NumbersOfLinesThisTurn == 4)
            {
                ClearedFourLines();
                
            }

            NumbersOfLinesThisTurn = 0;
            
        }

    }

    

    public void ClearedOneLine()
    {
        CurrentScore += ScoreOneLine + (CurrentLevel * 20);
        numLinesCleared++;

    }

    public void ClearedTwoLines()
    {
        CurrentScore += ScoreTwoLine + (CurrentLevel * 25);
        numLinesCleared += 2;

    }

    public void ClearedThreeLines()
    {
        CurrentScore += ScoreThreeLine + (CurrentLevel * 30);
        numLinesCleared += 3;

    }

    public void ClearedFourLines()
    {
        CurrentScore += ScoreFourLine+(CurrentLevel * 40);
        numLinesCleared += 4;

    }

    public void UpdateHighScore()
    {
        if ( CurrentScore > startingHighScore)
        {
            PlayerPrefs.SetInt("HighScore5", startingHighScore4);
            PlayerPrefs.SetInt("HighScore4", startingHighScore3);
            PlayerPrefs.SetInt("HighScore3", startingHighScore2);
            PlayerPrefs.SetInt("HighScore2", startingHighScore);
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        } else if ( CurrentScore > startingHighScore2)
        {
            PlayerPrefs.SetInt("HighScore5", startingHighScore4);
            PlayerPrefs.SetInt("HighScore4", startingHighScore3);
            PlayerPrefs.SetInt("HighScore3", startingHighScore2);
            PlayerPrefs.SetInt("HighScore2", CurrentScore);
        } else if ( CurrentScore > startingHighScore3)
        {
            PlayerPrefs.SetInt("HighScore5", startingHighScore4);
            PlayerPrefs.SetInt("HighScore4", startingHighScore3);
            PlayerPrefs.SetInt("HighScore3", CurrentScore);
        } else if ( CurrentScore > startingHighScore4)
        {
            PlayerPrefs.SetInt("HighScore5", startingHighScore4);
            PlayerPrefs.SetInt("HighScore4", CurrentScore);
        } else if ( CurrentScore > startingHighScore5)
        {
            PlayerPrefs.SetInt("HighScore5", CurrentScore);
        }

    }

    public void Update()
    {

        UpdateScore();
        UpdateLevel();
        UpdateSpeed();
        UpdateUI();
        CheckUserInput();
        ResumeClick = false;
        ResumeButton.onClick.AddListener(ClickResume);
        
        PauseBtnClick = false;        
        PauseBtn.onClick.AddListener(ClickPause);
        

    }

    

    



void ClickPause()
{
    PauseBtnClick = true;
}


void ClickResume()
    {
        ResumeClick = true;
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || ResumeClick || PauseBtnClick)
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

    void UpdateUI()
    {
        hud_score.text = CurrentScore.ToString();
        hud_level.text = CurrentLevel.ToString();
        hud_lines.text = numLinesCleared.ToString();
    }



    void UpdateLevel()
    {
        CurrentLevel = numLinesCleared / 10;
        
    }

    void UpdateSpeed()
    {
        fallSpeed = 1.0f - ((float)CurrentLevel * 0.1f);
        
    }



    public bool CheckAboveGrid(Tetrimino tetrimino)
    {
        for (int x = 0; x< gridWith; ++x)
        {
            foreach (Transform mino in tetrimino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
	
	public bool IsFullRowAt (int y)
    {
        for ( int x = 0; x < gridWith; ++x)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        NumbersOfLinesThisTurn++;
        return true;
    }

    public void DeleteMinoAt (int y)
    {
        for (int x = 0; x < gridWith; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveRowDown (int y)
    {
        for (int x = 0; x < gridWith; ++x)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown (int y)
    {
        for (int i = y; i< gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y+1);
                --y;
                

            }
        }
    }

   


    public void UpdateGrid(Tetrimino tetrimino)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for ( int x = 0; x < gridWith; ++x)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x, y].parent == tetrimino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
                
            }
        }

        foreach (Transform mino in tetrimino.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        if (pos.y > gridHeight -1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void spawnNextTetris()
    {
        if (!gameStarted)
        {

            gameStarted = true;
            nextTetris = (GameObject)Instantiate(Resources.Load(GetRandomTetris(), typeof(GameObject)), DefaultSpawn, Quaternion.identity);
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetris(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            TetrisName = nextTetris.name;
            previewTetromino.GetComponent<Tetrimino>().enabled = false;

            if (TetrisName == "Tetris_I(Clone)")
            {

                IsAnI = true;
                

            }
            else
            {
                IsAnI = false;
            }



        } else
        {
            
            previewTetromino.transform.localPosition = DefaultSpawn;
            nextTetris = previewTetromino;
            nextTetris.GetComponent<Tetrimino>().enabled = true;
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetris(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            TetrisName = nextTetris.name;
            previewTetromino.GetComponent<Tetrimino>().enabled = false;

        }
        
        if (TetrisName == "Tetris_I(Clone)")
        {

            IsAnI = true;
            
            
        }
        else
        {
            IsAnI = false;
        }

        
    }

       

    public bool CheckIsInsideGrid (Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWith && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetris()
    {
        int randomTetris = Random.Range(1, 8);
        string randomTetrisName = "Tetris_T";
        switch (randomTetris)
        {
            case 1:
                randomTetrisName = "Tetris_T";
                break;
            case 2:
                randomTetrisName = "Tetris_J";
                break;
            case 3:
                randomTetrisName = "Tetris_L";
                break;
            case 4:
                randomTetrisName = "Tetris_O";
                break;
            case 5:
                randomTetrisName = "Tetris_S";
                break;
            case 6:
                randomTetrisName = "Tetris_Z";
                break;
            case 7:
                randomTetrisName = "Tetris_I";
                break;
        }

        return randomTetrisName;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("LastScore", CurrentScore);
        UpdateHighScore();        
        SceneManager.LoadScene("GameOver");
        
    }

    
}
