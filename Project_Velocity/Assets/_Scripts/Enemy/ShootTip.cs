using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTip : MonoBehaviour
{
    private float timeBetweenShots;

    public float startTimeBetweenShots;

    private Transform enemyTransform;

    public GameObject projectile;
    void Start()
    {
        enemyTransform = GetComponentInParent<Transform>();
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0)
        {
         

            Instantiate(projectile, gameObject.transform.position, enemyTransform.rotation);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
