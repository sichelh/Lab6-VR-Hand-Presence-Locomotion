using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Prefab;
    public AudioSource audioSource;
    public AudioSource DieAudio;
    public GameObject ClearUI;
    public GameObject GameOverUI;

    public float minDistance = 30.0f;
    public int EnemyNum = 15;

    public Text ScoreText;
    public Text RemainText;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ClearUI.SetActive(false);
        GameOverUI.SetActive(false);

        for (int i = 0; i < EnemyNum; i++) //spawn enemy 15
        {
            Vector3 spawnPos = new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(0.0f, 3.0f), Random.Range(minDistance, 40.0f));
            int Index = Random.Range(0, Prefab.Length);
            Instantiate(Prefab[Index], spawnPos, Prefab[Index].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + score;

        int Remain = EnemyNum - score;
        RemainText.text = "Remain: " + Remain;

        if (score == EnemyNum)
        {
            ClearUI.SetActive(true);
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
            }
            audioSource.Play();
        }
    }
}
