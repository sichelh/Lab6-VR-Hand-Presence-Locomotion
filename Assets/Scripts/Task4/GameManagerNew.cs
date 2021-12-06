using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class GameManagerNew : MonoBehaviour
{
    public GameObject[] Prefab;
    public AudioSource audioSource;
    public AudioSource DieAudio;
    public AudioSource ClearAudio;
    public GameObject ClearUI;
    public GameObject GameOverUI;

    private GameObject[] Enemy;

    public int EnemyNum = 5;
    private int EnemyNow;
    public int EnemyMax = 30;

    public Text ScoreText;
    public Text RemainText;

    private int score = 0;
    private int Remain;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        ClearUI.SetActive(false);
        GameOverUI.SetActive(false);
        timerIsRunning = true;

        for (int i = 0; i < EnemyNum; i++) 
        {
            Vector3 spawnPosR = new Vector3(Random.Range(-20.0f, -40.0f), Random.Range(0.0f, 0.0f), Random.Range(0.0f, 200.0f));
            Vector3 spawnPosL = new Vector3(Random.Range(20.0f, 40.0f), Random.Range(0.0f, 0.0f), Random.Range(0.0f, 200.0f));
            int Index = Random.Range(0, Prefab.Length);
            Instantiate(Prefab[Index], spawnPosR, Prefab[Index].transform.rotation);
            Instantiate(Prefab[Index], spawnPosL, Prefab[Index].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + score;
        
        //count enemy number now
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyNow = Enemy.Length;
        
        Remain = EnemyNow;
        RemainText.text = "Remain: " + Remain;

        //timer
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOverUI.SetActive(true);
                Time.timeScale = 0;
                DieAudio.Play();
            }
        }
    }

    void DisplayTime(float timeToDisplay) //display timer
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SpawnEnemy() //spawn enemy
    {
        if (EnemyNow < EnemyMax)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 spawnPosR = new Vector3(Random.Range(-40.0f, -80.0f), Random.Range(0.0f, 0.0f), Random.Range(0.0f, 200.0f));
                Vector3 spawnPosL = new Vector3(Random.Range(40.0f, 80.0f), Random.Range(0.0f, 0.0f), Random.Range(0.0f, 200.0f));
                int Index = Random.Range(0, Prefab.Length);
                Instantiate(Prefab[Index], spawnPosR, Prefab[Index].transform.rotation);
                Index = Random.Range(0, Prefab.Length);
                Instantiate(Prefab[Index], spawnPosL, Prefab[Index].transform.rotation);
            }

        }
    }

    void OnCollisionEnter(Collision collision) //enemy collide player, game over
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
            DieAudio.Play();
        }
    }

    public void HandleHoverEnter(HoverEnterEventArgs args) //hover on enemy
    {
        if (args.interactable.CompareTag("Enemy"))
        {
            Debug.Log("Hovering over an enemy: " + args.interactable.gameObject.name);
        }
    }

    public void HandleSelectEnter(SelectEnterEventArgs args) //shot gun and hit enemy
    {
        if (args.interactable.CompareTag("Enemy"))
        {
            //check hp
            //Debug.Log("Enemy: " + args.interactable.gameObject + " | Hp: " + args.interactable.gameObject.GetComponent<EnemyController>().hp);

            //each enemy has different HP
            args.interactable.gameObject.GetComponent<EnemyController>().hp -= 1;
            
            if (args.interactable.gameObject.GetComponent<EnemyController>().hp <= 0) //hp is 0 destroy enemy
            {
                Destroy(args.interactable.gameObject);
                score++;
                SpawnEnemy();
            }
            audioSource.Play();
        }
    }

    public void UDTSelectEnter(SelectEnterEventArgs args) //UDT grab Destroy all enemy
    {
        if (args.interactable.CompareTag("UTS"))
        {
            
            for (int i=0; i<Enemy.Length; i++)
            {
                Destroy(Enemy[i]);
            }
            score = EnemyNum;
            Remain = 0;
            ClearUI.SetActive(true);
            ClearAudio.Play();
        }
    }
    
}
