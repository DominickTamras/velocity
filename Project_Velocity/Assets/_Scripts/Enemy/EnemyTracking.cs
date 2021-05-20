using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    public float maxSpeed;

    public float minSpeed;

    public float finalTime;

    public float thresholdCompare;

    public float speedThreshold;

    public GameObject target; // Replace with event system. (Or turn into scriptable object.)

    [HideInInspector]
    public bool isIn;

    public float rotateSpeed;

    public float predictBuffer;

    [HideInInspector]
    public bool isOut;

    private float dist;

    private float enemyPos;

    private Rigidbody rigid;

    Shoot shootSpeed;

    PlayerMovement instance;

    PlayerMovement currentSpeedz;

    private Vector3 predictPosition;

    private Quaternion ogRotation;





    void Awake()
    {   
        // isIn = true;
        rigid = target.GetComponent<Rigidbody>();
        ogRotation = gameObject.transform.rotation;
        instance = FindObjectOfType<PlayerMovement>();
        currentSpeedz = FindObjectOfType<PlayerMovement>();
        shootSpeed = FindObjectOfType<Shoot>();

    }

    // Update is called once per frame
    void Update() // Add all this shit in numerator
    {
      

      enemyPos = transform.position.x;
      
      dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
      
     // Debug.Log(dist);
        if(isIn)
        {
            if (currentSpeedz.currentSpeed < speedThreshold && dist > thresholdCompare)
            {
                LookAt2();
            }

            else
            {
                StartCoroutine(LookAtCor());
                StartCoroutine(LookAtCor2());
            }

        }

        if(isOut)
        {
            ReturnPos();
        }



    }

    void LookAt()
    {
       
        Vector3 direction = target.transform.position - gameObject.transform.position;
   
        Quaternion rotation = Quaternion.LookRotation(direction);

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotation, finalTime * Time.deltaTime * rotateSpeed);
    }

    void LookAt2()
    {

        //Vector3 direction = target.transform.position - gameObject.transform.position;
        Vector3 predictPosition = instance.CalcFuturePost(Vector3.Distance(transform.position, instance.transform.position) * predictBuffer);
        
        Quaternion rotation = Quaternion.LookRotation(predictPosition);

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotation, finalTime * Time.deltaTime * rotateSpeed);
    }

    void ReturnPos()
    {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, ogRotation, Time.deltaTime * 4.0f);
    }


    private IEnumerator LookAtCor()
    {   
        float lerpTime = 0;
        float currentTime = finalTime;
        LookAt();

        while (dist > thresholdCompare)
        {

            //lerpTime += Time.deltaTime;
            finalTime = Mathf.Lerp(currentTime, minSpeed, lerpTime += Time.deltaTime * 0.2f);
            yield return null;

        }

    }

    private IEnumerator LookAtCor2()
    {
        float lerpTime = 0;
        float currentTime = finalTime;
        LookAt();

        while (dist < thresholdCompare)
        {

            //lerpTime += Time.deltaTime;
            finalTime = Mathf.Lerp(currentTime, maxSpeed, lerpTime += Time.deltaTime * 0.2f);
            yield return null;

        }

    }

 

   /* private IEnumerator StartOthers()
    {
        while (currentSpeedz.currentSpeed > 12f)
        {
            StartCoroutine(LookAtCor());
            StartCoroutine(LookAtCor2());
            yield return null;
        }
    }*/

    //Speed calc down here, wait for David.

}


