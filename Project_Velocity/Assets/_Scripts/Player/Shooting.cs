using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using EZCameraShake;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] MeleeAttack ma;
    [SerializeField] GameObject bulletIndicator;
    [SerializeField] float range = 100;
    public bool hasBullet = true;
    public BlackHoleGunSway kickback;
    public BlackHoleGunSway armKickBack;

    [Header("Particles")]
    [SerializeField] ParticleSystem bulletTrail;
    [SerializeField] ParticleSystem muzzelFlash;
    [SerializeField] GameObject hitEffect;

    [Header("Reverse Gravity")]
    [SerializeField] float flipCamSpeed;
    public bool reverseGravity = false;
    float zVel;

    [Header("Camera Shake")]
    [SerializeField] float magnitude = 4f;
    [SerializeField] float roughness = 4f;
    [SerializeField] float fadeInTime = 0.1f;
    [SerializeField] float fadeOutTime = 0.5f;

    [Header("VFX")]

    public GameObject gunEffectCharge;
    public GameObject gunEffectFlash;
    public GameObject gunEffectCircle;
    public GameObject gunImpactEffect;

    private VisualEffect bulletCharge;
    private VisualEffect bulletFlash;
    private VisualEffect gunCircle;



    CameraLook cl;
    PlayerMovement pm;

    private void Start()
    {
        cl = GetComponent<CameraLook>();
        pm = GetComponent<PlayerMovement>();

        bulletCharge = gunEffectCharge.GetComponent<VisualEffect>();
        bulletFlash = gunEffectFlash.GetComponent<VisualEffect>();
        gunCircle = gunEffectCircle.GetComponent<VisualEffect>(); 
       


    }

    private void Update()
    {
        FlipView();

        if (!MenuManager.GameIsPaused)
        {
            BulletIndicator();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !ma.isAttacking)
            {
                if (hasBullet)
                {
                    Shoot();
                    kickback.GunKickBack();
                    armKickBack.GunKickBack();
                }
            }
        }
    }

    void Shoot()
    {
        bulletCharge.Play();
        gunCircle.Play();
        bulletFlash.Play();
       // bulletTrail.Play();
        hasBullet = false;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            //VFX
            GameObject impactVFX = Instantiate(gunImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 1);

            //Shooting Enemy
            EnemyDeath enemy = hit.transform.GetComponent<EnemyDeath>();
            if(enemy != null)
            {
                enemy.Die();
                hasBullet = true;
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
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
            if (currentRotation.z < 179.9f)
            {
                currentRotation.z = Mathf.SmoothDampAngle(currentRotation.z, 180f, ref zVel, flipCamSpeed);
            }
            else
            {
                currentRotation.z = 180f;
                zVel = 0f;
            }
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
        else if(!reverseGravity)
        {
            Vector3 currentRotation = transform.eulerAngles;
            if(currentRotation.z > 0.1f)
            {
                currentRotation.z = Mathf.SmoothDampAngle(currentRotation.z, 0f, ref zVel, flipCamSpeed);
            }
            else
            {
                currentRotation.z = 0f;
                zVel = 0f;
            }
            cl.zRotation = currentRotation.z;
            transform.eulerAngles = currentRotation;
        }
    }
}
