using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // Use this for initialization
    void Start() {

        SoundFile = GetComponent<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update() {

        
        IsPaused = GameObject.FindObjectOfType<Game>().IsPaused;

        if (!IsPaused)
        {
            checkUserInput();            
            UpdateTetriminoScore();
            fallSpeed = GameObject.FindObjectOfType<Game>().fallSpeed;
        }

        DetectWallKick = transform.position;
        //Debug.Log(CanWallkick());

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
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            HorizontalTimer = 0;
            VerticalTimer = 0;
            ButtonDownWaitTimer = 0;
            movedImmediateHorizontal = false;
            movedImmediateVertical = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateLeft();

        } else if (Input.GetKeyDown(KeyCode.X))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveInstantDown();
        }
        
    }

    void MoveLeft()
    {
        if (movedImmediateHorizontal)
        {
            if (ButtonDownWaitTimer < ButtonDownWaitMax)
            {
                ButtonDownWaitTimer += Time.deltaTime;
                return;
            }

            if (HorizontalTimer < HorizontalSpeed)
            {
                HorizontalTimer += Time.deltaTime;
                return;
            }
            HorizontalTimer = 0;
        }

        if (!movedImmediateHorizontal)
        {
            movedImmediateHorizontal = true;
        }

        
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

    void MoveRight()
    {
        if (movedImmediateHorizontal)
        {
            if (ButtonDownWaitTimer < ButtonDownWaitMax)
            {
                ButtonDownWaitTimer += Time.deltaTime;
                return;
            }

            if (HorizontalTimer < HorizontalSpeed)
            {
                HorizontalTimer += Time.deltaTime;
                return;
            }
            HorizontalTimer = 0;
        }

        if (!movedImmediateHorizontal)
        {
            movedImmediateHorizontal = true;
        }
            
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
        //FindObjectOfType<Game>().UpdateHighScore();



    }

    void RotateLeft()
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
            //Debug.Log("CanWallkick");
            return true;
        }
        
        return false;
    }

    bool CanWallkickRight()
    {

        if (DetectWallKick.x == 9)
        {
            //Debug.Log("CanWallkick");
            return true;
        }
        return false;

    }

    bool CanHalfWallkickRight()
    {

        if (DetectWallKick.x == 8)
        {
            //Debug.Log("CanWallkick");
            return true;
        }
        return false;

    }

    bool CanHalfWallkickLeft()
    {

        if (DetectWallKick.x == 1)
        {
            //Debug.Log("CanWallkick");
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
