using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] MeleeAttack ma;
    [SerializeField] GameObject bulletIndicator;
    [SerializeField] float range = 100;
    public bool hasBullet = true;

    [Header("Particles")]
    [SerializeField] ParticleSystem bulletTrail;
    [SerializeField] ParticleSystem muzzelFlash;
    [SerializeField] GameObject hitEffect;

    [Header("Reverse Gravity")]
    [SerializeField] float flipCamSpeed;
    public bool reverseGravity = false;

    CameraLook cl;
    PlayerMovement pm;

    private void Start()
    {
        cl = GetComponent<CameraLook>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        FlipView();
        BulletIndicator();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !ma.isAttacking)
        {
            if(hasBullet)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        bulletTrail.Play();
        muzzelFlash.Play();
        hasBullet = false;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            //VFX
            GameObject impactVFX = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 1);

            //Shooting Enemy
            EnemyDeath enemy = hit.transform.GetComponent<EnemyDeath>();
            if(enemy != null)
            {
                enemy.Die();
                hasBullet = true;
            }

            //Reverse Gravity
            if(hit.collider.CompareTag("ReverseGravity"))
            {
                ReverseGravity();
                hasBullet = true;
            }
        }
    }
    
    void BulletIndicator()
    {
        if(hasBullet)
        {
            bulletIndicator.SetActive(true);
        }
        else if (!hasBullet)
        {
            bulletIndicator.SetActive(false);
        }
    }

    public void ReverseGravity()
    {
        if (!reverseGravity)
        {
            reverseGravity = true;
        }
        else
        {
            reverseGravity = false;
        }
    }

    void FlipView()
    {
        if (reverseGravity)
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.z = Mathf.Lerp(currentRotation.z, 180, Time.deltaTime * flipCamSpeed);
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
        else
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.z = Mathf.Lerp(currentRotation.z, 0, Time.deltaTime * flipCamSpeed);
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
    }
}
