using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableIfFarAway : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    private GameObject itemActivatorObject;
    private ObjectActivator activationScript;

    // --------------------------------------------------

    void Start()
    {
        itemActivatorObject = GameObject.Find("ObjectActivatorManager");
        activationScript = itemActivatorObject.GetComponent<ObjectActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.addList.Add(new ActivatorItem { item = this.gameObject });
    }
}
