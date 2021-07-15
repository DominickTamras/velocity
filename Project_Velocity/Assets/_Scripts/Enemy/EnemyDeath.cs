using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
public class EnemyDeath : MonoBehaviour
{
    public static int enemyCount;
    
    public GameObject deathObj;
    private VisualEffect deathExplosion;

    private void Awake()
    {
        enemyCount++;

        deathExplosion = deathObj.GetComponent<VisualEffect>();
    }

    public void Die()
    {
        deathExplosion.Play();
        Destroy(gameObject,0.15f);
        enemyCount--;
    }


}
