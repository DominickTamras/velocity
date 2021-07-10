using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour
{
    public static int enemyCount;

    private void Awake()
    {
        enemyCount++;
    }

    public void Die()
    {
        Destroy(gameObject);
        enemyCount--;
    }


}
