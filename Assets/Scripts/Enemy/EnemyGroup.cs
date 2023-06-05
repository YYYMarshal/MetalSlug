using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private readonly List<GameObject> enemys = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject enemy = transform.GetChild(i).gameObject;
            enemys.Add(enemy);
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemys.Remove(enemy);
    }
    public List<GameObject> GetEnemys()
    {
        return enemys;
    }
}
