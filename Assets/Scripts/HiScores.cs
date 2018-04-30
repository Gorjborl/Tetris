using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScores : MonoBehaviour {

    public Text HighScoreText;
    public Text HighScoreText2;
    public Text HighScoreText3;
    public Text HighScoreText4;
    public Text HighScoreText5;
    public Text Score_tip;
    public Image CodeImage;
    public int SuccessScore;
    public int PointsToReach = 30000;
    
        

	// Use this for initialization
	void Start () {
        /*PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("HighScore2", 0);
        PlayerPrefs.SetInt("HighScore3", 0);
        PlayerPrefs.SetInt("HighScore4", 0);
        PlayerPrefs.SetInt("HighScore5", 0);*/
       
        CodeImage = GameObject.Find("CodeImage").GetComponent<Image>();
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString() + " Points";
        HighScoreText2.text = PlayerPrefs.GetInt("HighScore2").ToString() + " Points";
        HighScoreText3.text = PlayerPrefs.GetInt("HighScore3").ToString() + " Points";
        HighScoreText4.text = PlayerPrefs.GetInt("HighScore4").ToString() + " Points";
        HighScoreText5.text = PlayerPrefs.GetInt("HighScore5").ToString() + " Points";
        
        
        
    }
	
	// Update is called once per frame
	void Update () {

        TipTextUpdate();

    }

    public void TipTextUpdate()
    {
        Score_tip = GameObject.Find("tip_message").GetComponent<Text>();
        
        if (PlayerPrefs.GetInt("HighScore") < PointsToReach)
        {
            Score_tip.text = "tip: Reach " + PointsToReach.ToString() + " points";
            
        }

        else 
        {
            
            CodeImage.enabled = true;
            
        }

        
    }
}
