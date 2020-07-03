using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankBullet : MonoBehaviour
{
    [SerializeField]
    private int moveSpeed = 50;

    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

        foreach(var player in players)
        {
            if(Vector3.Distance(player.transform.position,transform.position) < 0.6f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
