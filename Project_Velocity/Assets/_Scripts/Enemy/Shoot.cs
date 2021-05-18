using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{ // Update tomorrow. See why ball doesn't go forward. Turn into rigidbody transform.rotation * Vector3.forward
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
        transform.position += transform.rotation * Vector3.forward * speed * Time.deltaTime;
        

        StartCoroutine(JustDie());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyShot();
        }
    }

    public void DestroyShot()

    {
        Destroy(gameObject);
    }

    IEnumerator JustDie()
    {
        yield return new WaitForSeconds(2);

        DestroyShot();
    }
        
}