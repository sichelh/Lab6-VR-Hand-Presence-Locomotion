using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashManager : MonoBehaviour
{
    public GameObject[] Prefab;
    public AudioSource audioSource;
    public Text ScoreText;
    public Text RemainText;

    public GameObject EndUI;

    public int TrashNum = 15;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        EndUI.SetActive(false);
        for (int i =0; i< TrashNum; i++) //spawn trash
        {
            Vector3 spawnPos = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 3.0f), Random.Range(3.0f, 8.0f));
            int Index = Random.Range(0, Prefab.Length);
            Instantiate(Prefab[Index], spawnPos, Prefab[Index].transform.rotation);
        }
    }

    void Update()
    {
        ScoreText.text = "Score: " + score;

        int Remain = TrashNum - score;
        RemainText.text = "Remain: " + Remain;

        if (score == TrashNum) //Level Cleared
        {
            EndUI.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision) //Trash collide TrashTray
    {
        if (collision.gameObject.tag == "Trash")
        {
            Destroy(collision.gameObject);
            score++;
            audioSource.Play();
        }
    }
    
}
