using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCar : MonoBehaviour {

    public Text LastScore;
    public Text NewHighScore;
    
	// Use this for initialization
	void Start () {

        LastScore.text = PlayerPrefs.GetInt("LastScoreCar").ToString() + "Points";
        UpdateNewHighScore();

    }
	
	// Update is called once per frame
	void Update () {

        
    }

    void UpdateNewHighScore()
    {
        if ( PlayerPrefs.GetInt("LastScoreCar") >= PlayerPrefs.GetInt("HighScoreCar5") || PlayerPrefs.GetInt("LastScoreCar") >= PlayerPrefs.GetInt("HighScoreCar4") || PlayerPrefs.GetInt("LastScoreCar") >= PlayerPrefs.GetInt("HighScoreCar3")  || PlayerPrefs.GetInt("LastScoreCar") >= PlayerPrefs.GetInt("HighScoreCar2") || PlayerPrefs.GetInt("LastScoreCar") >= PlayerPrefs.GetInt("HighScoreCar"))
        {
            NewHighScore.text = "New Highscore!!";
        }
    }
}
