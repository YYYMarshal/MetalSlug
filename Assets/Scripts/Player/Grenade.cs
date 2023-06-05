using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody2D rigidbodySelf;
    private Animator animator;
    private bool isTouched = false;

    private void Start()
    {
        rigidbodySelf = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Init();
    }
    private void Init()
    {
        animator.enabled = false;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        //根据玩家朝向给手榴弹添加相应的力
        if (player.rotation.y == 0)
            rigidbodySelf.AddForce(new Vector2(200f, 300f));
        else
            rigidbodySelf.AddForce(new Vector2(-200f, 300f));
    }

    private void Update()
    {
        if (!isTouched)
            rigidbodySelf.transform.Rotate(new Vector3(0, 0, 1), Space.Self);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().Hurt();
        isTouched = true;

        rigidbodySelf.velocity = Vector2.zero;
        rigidbodySelf.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        animator.enabled = true;
        SoundManager.PlayMusicByName("GrenadeExplosion");
    }
    public void DestoryGrenade()
    {
        Destroy(gameObject);
    }
}
