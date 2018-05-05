using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

    public bool Collider;
    private GameObject EnemyCar;
    private GameObject EnemyCar2;
    private Rigidbody2D EnemyBody;
    private Rigidbody2D EnemyBody2;
    public float Speed;
    private float GetLevel;

    

    // Use this for initialization
    void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {

        //EnemyBody.velocity = new Vector2(0, 5f);
        EnemyCar = GameObject.FindGameObjectWithTag("EnemyCar");
        EnemyCar2 = GameObject.FindGameObjectWithTag("EnemyCar");
        EnemyBody = EnemyCar.GetComponent<Rigidbody2D>();
        EnemyBody2 = EnemyCar2.GetComponent<Rigidbody2D>();
        EnemyBody.velocity = new Vector2(0, Speed);
        UpdateSpeed();
        
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "PlayerCar")
        {
            Collider = true;
            FindObjectOfType<CarGame>().GameOver();
            //SceneManager.LoadScene("CarGameOver");     
        }

        if (collision.gameObject.tag == "GridLimit")
        {
            Collider = true;
            DestroyObject(EnemyCar);
            FindObjectOfType<CarGame>().SpawnEnemy();     
        }  
        
        if (collision.gameObject.tag == "EnemyCar2")
        {
            Collider = false;
            //Destroy(EnemyCar2);

        }
    }

    public float UpdateSpeed()
    {
        GetLevel = FindObjectOfType<CarGame>().Level;

        Speed = (-2f * GetLevel) - 10;

        if (Input.GetKey(KeyCode.Z))
        {
            Speed = 1.55f * Speed;
            
        }

        return Speed;
        
    }
}
