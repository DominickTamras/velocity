using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{ // Update tomorrow. See why ball doesn't go forward.
    public float speed;

 
   private Transform player;


    private Vector3 target;
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.forward * speed * Time.deltaTime;

        if (gameObject.transform.position.x == target.x && gameObject.transform.position.y == target.y && gameObject.transform.position.y == target.z)
        {
            DestroyShot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyShot();
        }
    }

    void DestroyShot()

    {
        Destroy(gameObject);
    }
        
}
