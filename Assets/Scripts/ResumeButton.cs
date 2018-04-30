using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour {

    public Button ResumeBtn;
    public GameObject Pause_Canvas;
    private bool IsPaused;
    public GameObject GameObjct;

    
    private void Start()
    {
        Button ResBtn = ResumeBtn.GetComponent<Button>();
        ResBtn.onClick.AddListener(onClickResumeBtn);

         
        
    }
    public void onClickResumeBtn()
    {
        //myObject.GetComponent<MyScript>().MyFunction();


    }
}
