using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton : MonoBehaviour {

    private CheatMenu code;
    
    public Button PasswordButtonOn;

    private int SucceedPoints;
    void Awake()
    {
        code = GetComponent<CheatMenu>();
        
    }

    private void Start()
    {
        SucceedPoints = GameObject.FindObjectOfType<HiScores>().PointsToReach;
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("HighScore")>SucceedPoints)
        {
            if (code.success)
            {
                PasswordButtonOn.gameObject.SetActive(true);
            }
        }
            
    }

    
}
