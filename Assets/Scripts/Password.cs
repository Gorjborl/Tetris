using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour {

    private int counter1 = 0;
    public Button button1;
    public Button button2;
    public Text Number1;

    private int counter2 = 0;
    public Button button3;
    public Button button4;
    public Text Number2;

    private int counter3 = 0;
    public Button button5;
    public Button button6;
    public Text Number3;

    private int counter4 = 0;
    public Button button7;
    public Button button8;
    public Text Number4;

    public bool successPass;
    public string Password1;
    public string PasswordAttempt;
    public Image GameUnlockImage;
    public float AdviceTimeCounter=0f;
        
    // Use this for initialization
    void Start () {

        Password1 = "5656";

        
        //Number1
        Button btn1 = button1.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick1);

        Button btn2 = button2.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick2);

        Number1 = GameObject.Find("Item1Text").GetComponent<Text>();
        Number1.text = counter1.ToString();

        //Number2
        Button btn3 = button3.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick3);

        Button btn4 = button4.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick4);

        Number2 = GameObject.Find("Item2Text").GetComponent<Text>();
        Number2.text = counter2.ToString();

        //Number3
        Button btn5 = button5.GetComponent<Button>();
        btn5.onClick.AddListener(TaskOnClick5);

        Button btn6 = button6.GetComponent<Button>();
        btn6.onClick.AddListener(TaskOnClick6);

        Number3 = GameObject.Find("Item3Text").GetComponent<Text>();
        Number3.text = counter3.ToString();

        //Number4
        Button btn7 = button7.GetComponent<Button>();
        btn7.onClick.AddListener(TaskOnClick7);

        Button btn8 = button8.GetComponent<Button>();
        btn8.onClick.AddListener(TaskOnClick8);

        Number4 = GameObject.Find("Item4Text").GetComponent<Text>();
        Number4.text = counter4.ToString();
        
    }
	
	// Update is called once per frame
	void Update () {
        
        Number1.text = counter1.ToString();
        Number2.text = counter2.ToString();
        Number3.text = counter3.ToString();
        Number4.text = counter4.ToString();
        PasswordAttempt = Number1.text + Number2.text + Number3.text + Number4.text;
        Password1Success(PasswordAttempt);
        passSuccess();
    }

    private void TaskOnClick1()
    {
        if ( counter1 < 9)
        {
            counter1++;
            
        }
              
    }

    private void TaskOnClick2()
    {
        
            counter1--;

        if (counter1 < 0)
        {
            counter1 = 0;
        }
        
    }

    private void TaskOnClick3()
    {
        if (counter2 < 9)
        {
            counter2++;

        }
        
    }

    private void TaskOnClick4()
    {

        counter2--;

        if (counter2 < 0)
        {
            counter2 = 0;
        }
        
    }

    private void TaskOnClick5()
    {
        if (counter3 < 9)
        {
            counter3++;

        }
        
    }

    private void TaskOnClick6()
    {

        counter3--;

        if (counter3 < 0)
        {
            counter3 = 0;
        }
        
    }

    private void TaskOnClick7()
    {
        if (counter4 < 9)
        {
            counter4++;

        }

    }

    private void TaskOnClick8()
    {

        counter4--;

        if (counter4 < 0)
        {
            counter4 = 0;
        }

    }

    public void Password1Success( string y )
    {
        if (y == Password1)
        {
            successPass = true;
        } else
        {
            successPass = false;
        }
    }

    public void passSuccess()
    {
        if (successPass)
        {
            PlayerPrefs.SetString("UnlockCarGame", "Enabled");
            GameUnlockImage.gameObject.SetActive(true);
            
            
        }
        if (!successPass)
        {
            PlayerPrefs.SetString("UnlockCarGame", "Disabled");
        }
    }

}
