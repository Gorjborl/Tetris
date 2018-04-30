using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHighScores : MonoBehaviour {

    public Text HighScoreText;
    public Text HighScoreText2;
    public Text HighScoreText3;
    public Text HighScoreText4;
    public Text HighScoreText5;

    // Use this for initialization
    void Start () {

        /*PlayerPrefs.SetInt("HighScoreCar", 100);
        PlayerPrefs.SetInt("HighScoreCar2", 50);
        PlayerPrefs.SetInt("HighScoreCar3", 30);
        PlayerPrefs.SetInt("HighScoreCar4", 20);
        PlayerPrefs.SetInt("HighScoreCar5", 10);*/

        HighScoreText.text = PlayerPrefs.GetInt("HighScoreCar").ToString() + " Points";
        HighScoreText2.text = PlayerPrefs.GetInt("HighScoreCar2").ToString() + " Points";
        HighScoreText3.text = PlayerPrefs.GetInt("HighScoreCar3").ToString() + " Points";
        HighScoreText4.text = PlayerPrefs.GetInt("HighScoreCar4").ToString() + " Points";
        HighScoreText5.text = PlayerPrefs.GetInt("HighScoreCar5").ToString() + " Points";

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
