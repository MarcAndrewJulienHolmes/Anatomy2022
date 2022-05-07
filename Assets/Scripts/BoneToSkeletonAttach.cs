using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneToSkeletonAttach : MonoBehaviour
{
    public GameObject thisGameObject;

    public string thisGameObjectName;

    public GameObject skeletonAttachObject;

    public GameObject skeletonReplaceObject;

    public string skeletonAttachObjectName;

    public AudioSource audioSource;


    private void Awake()
    {
        thisGameObject = transform.gameObject;

        thisGameObjectName = transform.name;

        skeletonAttachObject = GameObject.Find(thisGameObjectName + " Attach");

        skeletonReplaceObject = GameObject.Find(thisGameObjectName + " Replace");

        skeletonAttachObjectName = skeletonAttachObject.name;

        audioSource = skeletonReplaceObject.GetComponent<AudioSource>();

        skeletonReplaceObject.SetActive(false);
    }



    public void OnTriggerEnter(Collider other)
    {        
        if (other.name == skeletonAttachObject.name)
        {

            thisGameObject.SetActive(false);

            skeletonAttachObject.SetActive(false);

            skeletonReplaceObject.SetActive(true);

            audioSource.Play();

        }
        else        
        {        
            return;          
        }
    }
}
