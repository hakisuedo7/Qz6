using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{
    [SerializeField]
    private int moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = transform.position;
            pos.y += Time.deltaTime * moveSpeed;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = transform.position;
            pos.y -= Time.deltaTime * moveSpeed;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = transform.position;
            pos.x -= Time.deltaTime * moveSpeed;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = transform.position;
            pos.x += Time.deltaTime * moveSpeed;
            transform.position = pos;
        }
    }
}
