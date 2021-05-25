using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTip : MonoBehaviour
{
    public EnemyShoot_Settings settingFeature;

    /* private float timeBetweenShots;

     public float startTimeBetweenShots;

    //re-add if you remove scriptable object

     public GameObject projectile;*/

    private Transform enemyTransform;
    private float timeBetweenShots;
    void Start()
    {
        enemyTransform = GetComponentInParent<Transform>();
        timeBetweenShots = settingFeature.startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0)
        {
         

            Instantiate(settingFeature.projectile, gameObject.transform.position, enemyTransform.rotation);
            timeBetweenShots = settingFeature.startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
