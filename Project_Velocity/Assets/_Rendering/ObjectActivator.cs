using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField]
    private int distanceFromPlayer;

    private GameObject player;

    private List<ActivatorItem> activatorItems;

    public List<ActivatorItem> addList;

    bool spawnEffect;

    void Start()
    {
        player = GameObject.Find("Player");
        activatorItems = new List<ActivatorItem>();
        addList = new List<ActivatorItem>();

        AddToList();
    }

    void AddToList()
    {
        if (addList.Count > 0)
        {
            foreach (ActivatorItem item in addList)
            {
                if (item.item != null)
                {
                    activatorItems.Add(item);
                }
            }

            addList.Clear();
        }

        StartCoroutine("CheckActivation");
    }


    IEnumerator CheckActivation()
    {
        List<ActivatorItem> removeList = new List<ActivatorItem>();

        if (activatorItems.Count > 0)
        {
            foreach (ActivatorItem item in activatorItems)
            {
                if (item.item != null)
                {


                    if (Vector3.Distance(player.transform.position, item.item.transform.position) > distanceFromPlayer)
                    {
                        if (item.item == null)
                        {
                            removeList.Add(item);
                        }
                        else
                        {
                            item.item.SetActive(false);
                        }
                    }
                    else
                    {
                        if (item.item == null)
                        {
                            removeList.Add(item);
                        }
                        else
                        {
                            item.item.SetActive(true);
                            removeList.Remove(item);

                         
                            item.item.transform.GetChild(0).GetComponent<VisualEffect>().Play();

                        }

                    }

                    yield return new WaitForSeconds(0.01f);
                }
            }

            for (int i =  activatorItems.Count - 1; i >= 0; i-- ) // backwards into list to remove when spawns
                {
                    if(activatorItems[i].item.activeSelf)
                {
                    activatorItems.RemoveAt(i);
                }
                
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (ActivatorItem item in removeList)
            {
                activatorItems.Remove(item);
            }
        }

        

        yield return new WaitForSeconds(0.01f);

        AddToList();
    }
}

public class ActivatorItem
{
    public GameObject item;
}


