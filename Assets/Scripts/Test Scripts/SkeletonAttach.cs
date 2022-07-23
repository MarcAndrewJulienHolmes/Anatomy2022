using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttach : MonoBehaviour
{
    public GameObject thisGameObject;

    public string thisGameObjectName;

    public string[] boneAttachString;

    public string primaryObjectString;

    public GameObject[] boneAttachObject;

    public GameObject selectedBoneAttachObject;

    public GameObject selectedBoneAttachConnectObject;

    public bool isConnected;

    //public GameObject connectPoint;

    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = transform.gameObject;

        thisGameObjectName = transform.name;

        if (boneAttachString != null)
        {
            for (int i = 0; i < boneAttachString.Length; i++)
            {
                boneAttachObject[i] = GameObject.Find(boneAttachString[i]);
            }
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        if (!isConnected)
        {
            for (int i = 0; i < boneAttachString.Length; i++)
            {
                if (other.name == boneAttachString[i] && other.name != primaryObjectString)
                {
                    if (boneAttachObject != null)
                    {
                        selectedBoneAttachObject = boneAttachObject[i];
                        thisGameObject.transform.parent = selectedBoneAttachObject.transform;
                        selectedBoneAttachObject.GetComponent<SelectedObject>().SetUpOutline();
                        selectedBoneAttachConnectObject = selectedBoneAttachObject.transform.Find(("Connect Point ") + thisGameObjectName).gameObject;
                        isConnected = true;
                        MoveToConnectPoint();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    public void MoveToConnectPoint()
    {
        thisGameObject.transform.position = selectedBoneAttachConnectObject.transform.position;
    }
}
