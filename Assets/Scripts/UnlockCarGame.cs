using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockCarGame : MonoBehaviour {

    private Password code;

    public Button CarButtonOn;

    public string Pass1;

    public string success1;

    void Awake()
    {
        //PlayerPrefs.SetString("UnlockCarGame", "Disabled");
    }

    private void Start()
    {
        Pass1 = "Enabled";
        success1 = PlayerPrefs.GetString("UnlockCarGame");
        Debug.Log(PlayerPrefs.GetString("UnlockCarGame"));
        checkPass1();
    }

    

    public void checkPass1()
    {
        if (success1 == Pass1)
        {
            CarButtonOn.gameObject.SetActive(true);
        }
    }
}
