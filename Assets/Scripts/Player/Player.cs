using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 子弹的方向
public enum BulletDirection
{
    Forward,
    Up,
}
public class Player : MonoBehaviour
{
    private readonly float speed = 300f;
    private readonly float jumpForce = 400f;
    private Rigidbody2D rigidbodySelf;
    private PlayerAnimation playerAnimation;
    private bool isOnGround = true;

    // 子弹发射的位置
    private readonly List<Transform> points = new();
    private Transform currentPoint;

    private readonly int maxHealth = 3;
    private int currentHealth;

    private UIManager uiManager;
    private readonly float meleeAttackDistance = 2f;

    private EnemyGroup enemyGroup;
    private Rocker rocker;

    private bool isAndroid = false;

    private void Awake()
    {
        rigidbodySelf = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();

        points.Add(transform.Find("PointBulletForward"));
        points.Add(transform.Find("PointBulletUp"));
        points.Add(transform.Find("PointThrow"));

        currentHealth = maxHealth;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        enemyGroup = GameObject.Find("EnemyGroup").GetComponent<EnemyGroup>();
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroid = true;
            rocker = GameObject.Find("Rocker").GetComponent<Rocker>();
        }
    }
    private void Update()
    {
        PlayerControl();
    }
    private void PlayerControl()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.K))
            Jump();
        if (Input.GetKeyDown(KeyCode.J))
            BulletAttack(BulletDirection.Forward);
        if (Input.GetKeyDown(KeyCode.U))
            MeleeAttack();
        if (Input.GetKeyDown(KeyCode.I))
            GrenadeAttack();
    }
    private void Move()
    {
        float h = isAndroid ? rocker.offset.x : Input.GetAxis("Horizontal");
        if (h != 0)
        {
            float targetSpeed = speed;
            // 在跳跃过程中，也可以以 1/2 的移动速度进行移动
            if (!isOnGround)
                targetSpeed /= 2;
            else
                playerAnimation.PlayWalkAnimation();
            transform.rotation = Quaternion.Euler(0, h > 0 ? 0 : 180, 0);
            rigidbodySelf.velocity = new Vector2(h * targetSpeed * Time.fixedDeltaTime, rigidbodySelf.velocity.y);
        }
        else
            playerAnimation.PlayIdleAnimation();
    }
    public void Jump()
    {
        if (!isOnGround)
            return;
        isOnGround = false;
        rigidbodySelf.AddForce(Vector2.up * jumpForce);
        playerAnimation.PlayJumpAnimation();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }
    public void BulletAttack(BulletDirection dir = BulletDirection.Forward)
    {
        SoundManager.PlayMusicByName("shoot");
        GameObject bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        switch (dir)
        {
            case BulletDirection.Forward:
                currentPoint = points[0];
                break;
            case BulletDirection.Up:
                currentPoint = points[1];
                break;
            default:
                break;
        }
        GameObject bullet = Instantiate(bulletPrefab, currentPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().InitDirection(dir);
        playerAnimation.PlayShootAnimation();
    }
    public void GrenadeAttack()
    {
        GameObject grenadePrefab = Resources.Load<GameObject>("Prefabs/Grenade");
        Instantiate(grenadePrefab, points[2].transform.position, Quaternion.identity);
    }
    public void MeleeAttack()
    {
        if (!isOnGround)
            return;
        List<GameObject> enemys = enemyGroup.GetEnemys();
        for (int i = 0; i < enemys.Count; i++)
        {
            float distance = Vector2.Distance(enemys[i].transform.position, transform.position);
            if (distance < meleeAttackDistance)
                enemys[i].GetComponent<Enemy>().Hurt();
        }
        playerAnimation.PlayAttackAnimation();
        SoundManager.PlayMusicByName("tieguo");
    }
    public void Hurt()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            uiManager.UpdateHealthPoint(currentHealth);
            SoundManager.PlayMusicByName("soliderDie");
        }
        else
            SceneManager.LoadScene(0);
    }
}
