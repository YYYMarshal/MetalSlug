using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform player;
    private Transform leftWall;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(2.5f, 0, -10);
        leftWall = GameObject.Find("LeftWall").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float offsetX = player.position.x - transform.position.x;
            if (offsetX > 4f && transform.position.x <= 30f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 10, 0, transform.position.z), Time.deltaTime);
            }
            if (offsetX > 4f && transform.position.x > 30f && transform.position.x < 35f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 2.3f, transform.position.z), Time.deltaTime);
            }
            if (player.transform.position.x >= 33)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(35.9f, 3.2f, transform.position.z), Time.deltaTime);
            }
        }
        leftWall.position = new Vector3(transform.position.x - 8.1f, leftWall.position.y, leftWall.position.z);
    }
}
