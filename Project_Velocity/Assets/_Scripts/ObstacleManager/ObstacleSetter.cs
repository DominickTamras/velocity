using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleSetter : MonoBehaviour
{
    public int id;

    [Header("Obstacle Selection")]
    public bool laser; // List of obstacles

    public bool movingPlatform;

    public bool spinOfDeath;

    public bool doorOpen;

    private bool doorActivate;


    [Header("Enemy # Death")]
    public List<GameObject> enemyInArea = new List<GameObject>();
    
    public bool killEnemyToProceed;

    public Vector3 endPosOfLocked;

    [Header("Trigger Toggle")]
    public bool turnOnTrigger;

    private Vector3 ogPos;
    private Vector3 endPos;

    void Start()
    {
        endPos = new Vector3(transform.position.x, 3, transform.position.z);
        ogPos = transform.position;
        ObstacleManager.current.onObstacleTriggerEnter += OnObstacleActivate; // register event
        ObstacleManager.current.onObstacleTriggerExit += OnObstacleEnd;                                                                   
    }

    private void Update()
    {
        if (turnOnTrigger == false)

        {
            OnObstacleActivate(id);
        }
    }

    private void OnObstacleActivate(int id)
    {
        if (id == this.id) // checks if id matches
        {
            if (laser == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (movingPlatform == true)
            {
                transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, 4), Mathf.PingPong(Time.time * 2, 1.0f));
            }

            if (spinOfDeath == true)
            {
                transform.Rotate(Vector3.forward * 50 * Time.deltaTime);
                //Add damage eventually
            }

            if (doorOpen == true)
            {
                doorActivate = true;
            
                StartCoroutine(OpenDoor());
          

            }

            if(killEnemyToProceed == true)
            {   
                foreach(GameObject missing in enemyInArea)
                {
                    if(missing == null)
                    {
                        enemyInArea.Remove(missing);
                    }
                }

                if(enemyInArea.Count == 0)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, endPosOfLocked, 4 * Time.deltaTime);
                }
            }

        }
    }

     private void OnObstacleEnd(int id)
    {
     if(id == this.id)
     {
            if (doorOpen == true)
            {
                doorActivate = true;
        
                StartCoroutine(CloseDoor());
            
            }
        }
    }

    private void OnDestroy()
    {
        ObstacleManager.current.onObstacleTriggerEnter -= OnObstacleActivate; // register event
        ObstacleManager.current.onObstacleTriggerExit -= OnObstacleActivate;   
    }

    IEnumerator OpenDoor()
    {

        for (int i = 0; i < 50; i++)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, 4 * Time.deltaTime);

            yield return null;
        }

       
    }

    IEnumerator CloseDoor()
    {
       
        for(int i = 0; i < 50; i++)
        {
            transform.position = Vector3.Lerp(transform.position, ogPos, 4 * Time.deltaTime);

            yield return null;
        }


    }
}
