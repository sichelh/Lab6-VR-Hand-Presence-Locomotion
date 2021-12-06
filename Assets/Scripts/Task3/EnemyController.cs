using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //enemy comes to player
    public float speed = 2.0F; //set speed each enemy prefabs
    public int hp = 5; //set hp each enemy prefabs
    private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() 
    {
        transform.LookAt(Player.transform);

        Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(movement);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 1, transform.position.z), Player.transform.position, step);
    }
}
