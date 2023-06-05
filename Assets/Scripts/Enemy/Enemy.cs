using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int maxHealth = 1;
    private int currentHealth;
    private EnemyAnimation enemyAnimation;

    // Enemy 与 Player 之间的距离：> 6 idle, 1.5 - 6 walk
    public float idleDistance = 6f;
    public float walkDistance = 1.5f;

    private bool isAttacking = false;
    private bool isDied = false;

    private float timer = 0;
    private readonly float attackTime = 1f;

    private GameObject player;
    private EnemyGroup enemyGroup;
    private UIManager uiManager;

    private void Awake()
    {
        player = GameObject.Find("Player");
        enemyAnimation = GetComponent<EnemyAnimation>();
        currentHealth = maxHealth;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        enemyGroup = GameObject.Find("EnemyGroup").GetComponent<EnemyGroup>();
    }

    private void Update()
    {
        if (IsMoveToPlayer())
            enemyAnimation.PlayWalkAnimation();
        else if (!isAttacking)
            Attack();
        else
            enemyAnimation.PlayIdleAnimation();
    }

    public void Hurt()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            isDied = true;
            SoundManager.PlayMusicByName("enemyDie");
            enemyAnimation.PlayDieAnimation();
            enemyGroup.RemoveEnemy(gameObject);
            if (enemyGroup.GetEnemys().Count == 0)
                uiManager.ShowMissionComplete();
        }
    }
    /// <summary>
    /// 给 Die 动画的事件用的函数
    /// </summary>
    public void DieAnimationEvent()
    {
        Destroy(gameObject);
    }

    private bool IsMoveToPlayer()
    {
        if (player != null)
        {
            Transform playerTransform = player.transform;
            Vector2 direction = new(0, 0);
            if ((playerTransform.transform.position.x - transform.position.x) > 0)
                direction.y = 180;
            transform.rotation = Quaternion.Euler(direction);

            if (Mathf.Abs(playerTransform.position.x - transform.position.x) > idleDistance)
                return false;
            if (Mathf.Abs(playerTransform.position.x - transform.position.x) < walkDistance)
                return false;
            if (!isDied)
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime);
            return true;
        }
        return false;
    }

    private void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            if ((transform.position - player.transform.position).magnitude <= 1.7f)
            {
                isAttacking = true;
                SoundManager.PlayMusicByName("knife");
                enemyAnimation.PlayKillAnimation();
                StartCoroutine(AttackSecondHalf());
            }
        }
    }
    private IEnumerator AttackSecondHalf()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().Hurt();
        timer = 0;
        isAttacking = false;
    }
}
