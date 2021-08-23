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

    public float moveSpeed;

    public Vector3 startPong;

    public Vector3 endPong;

    public bool spinOfDeath;

    public float spinSpeed;

    public bool doorOpen;

    private bool updateConstantly = false;

    private bool doorActivate;


    [Header("Enemy # Death")]
    public List<GameObject> enemyInArea = new List<GameObject>();
    
    public bool killEnemyToProceed;

    [Header("Trigger Toggle")]
    public bool turnOnTrigger;

    private Vector3 ogPos;
    private Vector3 endPos;

    [Header("Misc.")]

    public bool breakPod;

    private bool explodeOnce = false;



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

        if (updateConstantly == true)

        {
            OnObstacleActivate(id);
        }
    }

    private void OnObstacleActivate(int id)
    {
        if (id == this.id) // checks if id matches
        {
            if (laser == true) // Laser
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (movingPlatform == true) // Moving platform
            {
                transform.localPosition = Vector3.Lerp(startPong, endPong, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
            }

            if (spinOfDeath == true) //Spinning
            {
                transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
                //Add damage eventually
            }

            if (doorOpen == true) // Dpor Open
            {
                doorActivate = true;
            
                StartCoroutine(OpenDoor());
          

            }


            if(killEnemyToProceed == true)
            {
                updateConstantly = true;
                StartCoroutine(SpawnShield());

                foreach(GameObject missing in enemyInArea)
                {
                    if(missing == null)
                    {
                        enemyInArea.Remove(missing);
                    }
                }

                if(enemyInArea.Count == 0)
                {

                    StartCoroutine(CleanUp());
                    this.gameObject.GetComponent<Renderer>().enabled = false;
                    this.gameObject.GetComponent<Collider>().enabled = false;
                  
                    if (explodeOnce == false) // Plays sound once
                    {
                        this.gameObject.GetComponent<AudioSource>().Play();

                        explodeOnce = true;

                    }

                    //transform.localPosition = Vector3.Lerp(transform.localPosition, endPosOfLocked, 4 * Time.deltaTime);
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

    public IEnumerator SpawnShield()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;

        Debug.Log("Working");

        yield return new WaitForSeconds(0);
    }

    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
