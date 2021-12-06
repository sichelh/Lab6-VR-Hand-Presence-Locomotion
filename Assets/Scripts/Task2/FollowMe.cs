using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour
{
    //trashtray follow camera
    public float speed = 2.0F;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(movement);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.transform.position.x, 0, Player.transform.position.z), step);
    }

}
