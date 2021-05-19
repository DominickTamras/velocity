using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject deathParticle;

    public void Die()
    {
        Instantiate(deathParticle);
        Destroy(gameObject);
    }
}
