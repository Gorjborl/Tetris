using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour {

    public Text LastScore;
    public Text NewHighScore;
    
	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

        LastScore.text = PlayerPrefs.GetInt("LastScore").ToString() + "Points";
        UpdateNewHighScore();
    }

    void UpdateNewHighScore()
    {
        if ( PlayerPrefs.GetInt("LastScore") >= PlayerPrefs.GetInt("HighScore5") || PlayerPrefs.GetInt("LastScore") >= PlayerPrefs.GetInt("HighScore4") || PlayerPrefs.GetInt("LastScore") >= PlayerPrefs.GetInt("HighScore3")  || PlayerPrefs.GetInt("LastScore") >= PlayerPrefs.GetInt("HighScore2") || PlayerPrefs.GetInt("LastScore") >= PlayerPrefs.GetInt("HighScore"))
        {
            NewHighScore.text = "New Highscore!!";
        }
    }
}
