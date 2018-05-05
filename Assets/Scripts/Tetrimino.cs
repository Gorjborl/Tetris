using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetrimino : MonoBehaviour {

    float fall = 0;
    private float fallSpeed; 
    public bool allowRotation = true;
    public bool limitRotation = false;
    public int TetriminoScore = 100;
    public float TetriminoScoreTime;
    private float HorizontalSpeed = 0.1f;
    private float HorizontalTimer = 0;
    private float VerticalSPeed = 0.05f;
    private float VerticalTimer = 0;
    private float ButtonDownWaitMax = 0.2f;
    private float ButtonDownWaitTimer = 0;
    private bool movedImmediateHorizontal = false;
    private bool movedImmediateVertical = false;
    public AudioClip moveSound;
    public AudioClip rotateSound;
    public AudioClip landSound;
    public AudioClip DestroyRow;
    private AudioSource SoundFile;
    private bool IsPaused;

    private Vector3 DetectWallKick;

    private Button LeftButton;
    private bool LeftBtnClick;

    private Button RightButton;
    private bool RightBtnClick;

    private Button RotateButton;
    private bool RotateBtnClick;

    private Button DropButton;
    private bool DropBtnClick;

    private Button DownButton;
    private bool DownBtnClick;

    // Use this for initialization
    void Start() {

        SoundFile = GetComponent<AudioSource>();


        LeftButton = GameObject.Find("Button_Left").GetComponent<Button>();
        RightButton = GameObject.Find("Button_Right").GetComponent<Button>();
        RotateButton = GameObject.Find("Button_Rotate").GetComponent<Button>();
        DropButton = GameObject.Find("Button_Drop").GetComponent<Button>();
        DownButton = GameObject.Find("Button_Down").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {


        IsPaused = GameObject.FindObjectOfType<Game>().IsPaused;

        if (!IsPaused)
        {
            checkUserInput();
            UpdateTetriminoScore();
            fallSpeed = GameObject.FindObjectOfType<Game>().fallSpeed;
        }

        DetectWallKick = transform.position;
        LeftBtnClick = false;
        LeftButton.onClick.AddListener(ClickOnLeftBtn);

        RightBtnClick = false;
        RightButton.onClick.AddListener(ClickOnRightBtn);

        RotateBtnClick = false;
        RotateButton.onClick.AddListener(ClickOnRotateBtn);

        DropBtnClick = false;
        DropButton.onClick.AddListener(ClickOnDropBtn);

        DownBtnClick = false;
        DownButton.onClick.AddListener(ClickOnDownBtn);
    }

    void ClickOnLeftBtn()
    {
        LeftBtnClick = true;
    }

    void ClickOnRightBtn()
    {
        RightBtnClick = true;
    }

    void ClickOnRotateBtn()
    {
        RotateBtnClick = true;
    }

    void ClickOnDropBtn()
    {
        DropBtnClick = true;
    }

    void ClickOnDownBtn()
    {
        DownBtnClick = true;
    }

    void UpdateTetriminoScore()
    {
        if (TetriminoScoreTime < 1)
        {
            TetriminoScoreTime += Time.deltaTime;

        } else
        {
            TetriminoScoreTime = 0;
            TetriminoScore = Mathf.Max(TetriminoScore - 10, 0);
        }
    }
    

    void checkUserInput()
    {
        
        if (Input.GetKey(KeyCode.RightArrow) || RightBtnClick )
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || LeftBtnClick )
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow) || RotateBtnClick )
        {
            RotateLeft();

        } else if (Input.GetKeyDown(KeyCode.X))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed || DownBtnClick )
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || DropBtnClick )
        {
            MoveInstantDown();
        }
        
    }

    public void MoveLeft()
    {        
        
            transform.position += new Vector3(-1, 0, 0);
           
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
                PlayMoveAudio();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        
        
    }

    public void MoveRight()
    {
                    
            transform.position += new Vector3(1, 0, 0);
            
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
                PlayMoveAudio();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }      
    }

    void MoveDown()
    {
        if (movedImmediateVertical)
        {
            if (ButtonDownWaitTimer < ButtonDownWaitMax)
            {
                ButtonDownWaitTimer += Time.deltaTime;
                return;
            }

            if (VerticalTimer < VerticalSPeed)
            {
                VerticalTimer += Time.deltaTime;
                return;
            }
            VerticalTimer = 0;
        }

        if (!movedImmediateVertical)
        {
            movedImmediateVertical = true;
        }

        transform.position += new Vector3(0, -1, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayMoveAudio();
            }
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            FindObjectOfType<Game>().DeleteRow();
            SoundFile.PlayOneShot(DestroyRow);
            if (FindObjectOfType<Game>().CheckAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }
            enabled = false;
            PlayLandingAudio();
            FindObjectOfType<Game>().spawnNextTetris();
            Game.CurrentScore += TetriminoScore;
            //FindObjectOfType<Game>().UpdateHighScore();

        }

        fall = Time.time;
    }

    void MoveInstantDown()
    {
        for ( int j = 0 ; j < 20; j++)
        {
            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                break;
                

            }
        }
        FindObjectOfType<Game>().DeleteRow();
        SoundFile.PlayOneShot(DestroyRow);
        if (FindObjectOfType<Game>().CheckAboveGrid(this))
        {
            FindObjectOfType<Game>().GameOver();
        }
        enabled = false;
        PlayLandingAudio();
        FindObjectOfType<Game>().spawnNextTetris();
        Game.CurrentScore += TetriminoScore;
        



    }

   public void RotateLeft()
    {
        if (allowRotation)
        {

            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, 90);
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
            }
            else
            {
                transform.Rotate(0, 0, -90);

            }
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
                PlayRotateAudio();
            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            if (CanWallkickLeft())
            {
                if (FindObjectOfType<Game>().IsAnI)
                {

                    transform.position += new Vector3(1, 0, 0);
                    transform.position += new Vector3(1, 0, 0);
                    transform.Rotate(0, 0, -90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);

                    }

                }
                else
                {
                    transform.position += new Vector3(1, 0, 0);
                    transform.Rotate(0, 0, -90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);

                    }
                }

            }

            if (CanWallkickRight())
            {
                if (FindObjectOfType<Game>().IsAnI)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    transform.Rotate(0, 0, -90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);

                    }

                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0);
                    transform.Rotate(0, 0, -90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }


            }

            if (CanHalfWallkickLeft())
            {
                if (FindObjectOfType<Game>().IsAnI)
                {
                    transform.position += new Vector3(1, 0, 0);
                    transform.Rotate(0, 0, -90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);

                    }

                }

            }

        }

        Debug.Log(transform.rotation.eulerAngles.z);

    }


    void RotateRight()
    {
        if (allowRotation)
        {

            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
            else
            {
                transform.Rotate(0, 0, 90);

            }
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
                PlayRotateAudio();
            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
            }

            if (CanWallkickLeft())
                {
                    if (FindObjectOfType<Game>().IsAnI)
                    {                
                     
                        transform.position += new Vector3(1, 0, 0);
                        transform.Rotate(0, 0, 90);
                    

                    

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);

                    }

                    } else
                    {
                        transform.position += new Vector3(1, 0, 0);
                        transform.Rotate(0, 0, 90);

                        if (CheckIsValidPosition())
                        {
                            FindObjectOfType<Game>().UpdateGrid(this);
                            PlayRotateAudio();
                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);

                        }
                }
                    
                }

                if (CanWallkickRight())
                {
                if (FindObjectOfType<Game>().IsAnI)
                {
                    transform.position += new Vector3(-2, 0, 0);                    
                    transform.Rotate(0, 0, 90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);

                    }

                } else
                {
                    transform.position += new Vector3(-1, 0, 0);
                    transform.Rotate(0, 0, 90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
                
                
                }

            if (CanHalfWallkickRight())
            {
                if (FindObjectOfType<Game>().IsAnI)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    transform.Rotate(0, 0, 90);

                    if (CheckIsValidPosition())
                    {
                        FindObjectOfType<Game>().UpdateGrid(this);
                        PlayRotateAudio();
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);

                    }

                }
                
            }

        }
        Debug.Log(transform.rotation.eulerAngles.z);

    }

    bool CanWallkickLeft()
    {
                
        if (DetectWallKick.x == 0)
        {
            
            return true;
        }
        
        return false;
    }

    bool CanWallkickRight()
    {

        if (DetectWallKick.x == 9)
        {
            
            return true;
        }
        return false;

    }

    bool CanHalfWallkickRight()
    {

        if (DetectWallKick.x == 8)
        {
            
            return true;
        }
        return false;

    }

    bool CanHalfWallkickLeft()
    {

        if (DetectWallKick.x == 1)
        {
            
            return true;
        }
        return false;

    }

    void PlayMoveAudio()
    {
        SoundFile.PlayOneShot(moveSound);
    }

    void PlayRotateAudio()
    {
        SoundFile.PlayOneShot(rotateSound);
    }

    void PlayLandingAudio()
    {
        SoundFile.PlayOneShot(landSound);
    }


    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);

            if (FindObjectOfType<Game>().CheckIsInsideGrid (pos) == false)
            {
               return false;
            }

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
