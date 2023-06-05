using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float speed = 20f;
    private Rigidbody2D rigidbodySelf;

    private void Awake()
    {
        rigidbodySelf = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    // 初始化子弹
    public void InitDirection(BulletDirection dir)
    {
        // 根据开火方向 判断方向 设置朝向 速度
        switch (dir)
        {
            case BulletDirection.Forward:
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                transform.rotation = Quaternion.Euler(0, player.rotation.y == 0 ? 180 : 0, 0);
                rigidbodySelf.velocity = new Vector2(player.rotation.y == 0 ? speed : -speed, 0);
                break;
            case BulletDirection.Up:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                rigidbodySelf.velocity = new Vector2(0, speed);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().Hurt();
        Destroy(gameObject);
    }


}
