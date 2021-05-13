using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    public float maxSpeed;

    public float minSpeed;

    public float finalTime;

    public Transform target; // Replace with event system. (Or turn into scriptable object.)

    public bool isIn;

    public bool isOut;

    private float dist;

    private float enemyPos;

    private Quaternion ogRotation;


    void Awake()
    {
        // isIn = true;

        ogRotation = gameObject.transform.rotation;

    }

    // Update is called once per frame
    void Update() // Add all this shit in numerator
    {
      enemyPos = transform.position.x;
      
      dist = Vector3.Distance(target.position, gameObject.transform.position);
      
      Debug.Log(dist);
        if(isIn)
        {
            StartCoroutine(LookAtCor());
            StartCoroutine(LookAtCor2());
        }

        if(isOut)
        {
            ReturnPos();
        }


    }



    void LookAt()
    {
        Vector3 direction = target.position - gameObject.transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotation, finalTime * Time.deltaTime);
    }

    void ReturnPos()
    {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, ogRotation, Time.deltaTime * 4.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isOut = false;
            isIn = true;
            Debug.Log("Working");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            finalTime = 0;
            isIn = false;
            isOut = true;
            Debug.Log("Working");
        }
    }

    private IEnumerator LookAtCor()
    {   
         float lerpTime = 0;
        float currentTime = finalTime;
        LookAt();

        while (dist > 5)
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

        while (dist < 5)
        {

            //lerpTime += Time.deltaTime;
            finalTime = Mathf.Lerp(currentTime, maxSpeed, lerpTime += Time.deltaTime * 0.2f);
            yield return null;

        }




    }

    //Speed calc down here, wait for David.

}


