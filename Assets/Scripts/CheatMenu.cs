using System.Collections;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    private const float WaitTime = 0.5f;

    
    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        
    };

    
    public bool success;
    private bool SuccessHiscore;

    IEnumerator Start()
    {
        float timer = 0f;
        int index = 0;

        

       
            while (true)
            {
                if (Input.GetKeyDown(keys[index]))
                {
                    //Debug.Log(index.ToString());
                    index++;

                    if (index == keys.Length)
                    {
                        success = true;
                        timer = 0f;
                        index = 0;
                    }
                    else
                    {
                        timer = WaitTime;
                    }
                }
                else if (Input.anyKeyDown)
                {
                    // print("Wrong key in sequence.");
                    timer = 0;
                    index = 0;
                }

                if (timer > 0)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        timer = 0;
                        index = 0;
                    }
                }

                yield return null;
            }

    }

}

