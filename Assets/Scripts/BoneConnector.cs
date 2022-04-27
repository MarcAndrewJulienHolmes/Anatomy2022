using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneConnector : MonoBehaviour
{
    public string thisGameObjectName, upstreamConnectObjectName;

    public string[] downstreamConnectObjectName;

    public GameObject thisGameObject, replaceMeshes, originalMeshes, connectPointObject, selectedDownstreamConnectObject;

    public GameObject[] downstreamConnectObject;

    public bool isConnected;

    private void Awake()
    {
        thisGameObject = transform.gameObject;

        replaceMeshes = thisGameObject.transform.Find("Replace Meshes").gameObject;
        originalMeshes = thisGameObject.transform.Find("Original Meshes").gameObject;
        connectPointObject = thisGameObject.transform.Find("Connect Point").gameObject;
        if(downstreamConnectObjectName != null)
        {
            for(int i = 0; i < downstreamConnectObjectName.Length; i++)
            {
                downstreamConnectObject[i] = GameObject.Find(downstreamConnectObjectName[i]);
            }            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        replaceMeshes.SetActive(false);
        originalMeshes.SetActive(true);

        isConnected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            MoveDownstreamToConnectPoint();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isConnected)
        {
            for (int i = 0; i < downstreamConnectObjectName.Length; i++)
            {
                if (other.name == downstreamConnectObjectName[i])
                {
                    if (downstreamConnectObject != null)
                    {
                        replaceMeshes.SetActive(true);
                        originalMeshes.SetActive(false);
                        downstreamConnectObject[i].transform.parent = thisGameObject.transform;
                        selectedDownstreamConnectObject = downstreamConnectObject[i];
                        MoveDownstreamToConnectPoint();
                        isConnected = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
 

        //if (other.name == upstreamConnectObjectName)
        //{
        //    thisGameObject.SetActive(false);

        //    GameObject upstreamReplaceMeshes = GameObject.Find(upstreamConnectObjectName + "/Drop Point");
        //    GameObject upstreamOriginalMeshes = GameObject.Find(upstreamConnectObjectName + "/Meshes");

        //    upstreamReplaceMeshes.SetActive(true);
        //    upstreamOriginalMeshes.SetActive(false);

        //    Debug.LogError("mesh replace triggered");
        //}


    }

    public void MoveDownstreamToConnectPoint()
    {
        if (selectedDownstreamConnectObject.transform.position != connectPointObject.transform.position)
        {
            selectedDownstreamConnectObject.transform.position = connectPointObject.transform.position;
        }

    }

}
