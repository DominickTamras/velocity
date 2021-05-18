using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float range = 100;

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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        bulletTrail.Play();
        muzzelFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            GameObject impactVFX = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 1);

            if(hit.collider.CompareTag("ReverseGravity"))
            {
                if(!reverseGravity)
                {
                    reverseGravity = true;
                }
                else
                {
                    reverseGravity = false;
                }
            }
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
