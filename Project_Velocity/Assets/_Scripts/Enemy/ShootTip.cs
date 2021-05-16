using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTip : MonoBehaviour
{
    private float timeBetweenShots;

    public float startTimeBetweenShots;

    public GameObject projectile;
    void Start()
    {
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0)
        {

            Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
