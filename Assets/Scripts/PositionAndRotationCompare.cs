using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PositionAndRotationCompare : MonoBehaviour
{
    [Header("Scripts")]
    public RightHandPointer rightHandPointer;
    public GameObject rightHand;
    public MuscleTestingScenarioSetup muscleTestingScenarioSetup;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;
    public GameObject refAttachCube;

    [Header("Muscle Attach Object")]
    public string attachObjectName;
    public GameObject attachObject;

    [Header("Position And Rotation Compare Logic")]
    public Vector3 thisGameObjectRotation;
    public Vector3 thisGameObjectPosition;
    public Vector3 attachObjectRotation;
    public Vector3 attachObjectPosition;
    public Vector3 refAttachCubePosition;
    public Vector3 refAttachCubeRotation;


    [Header("Audio Feedback")]
    public GameObject audiosourceHolder;
    public AudioSource audioSource;



    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        muscleTestingScenarioSetup = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<MuscleTestingScenarioSetup>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.name;
        refAttachCube = GameObject.Find(thisGameObjectName + " RefAttachCube");

        attachObjectName = thisGameObjectName + " Ref";
        attachObject = GameObject.Find(attachObjectName);

        audiosourceHolder = GameObject.FindGameObjectWithTag("AttachSFX");
        audioSource = audiosourceHolder.GetComponent<AudioSource>();

        attachObjectRotation = attachObject.transform.eulerAngles;
        attachObjectPosition = attachObject.transform.position;

        thisGameObject.transform.parent = attachObject.transform;
    }

    private void Update()
    {
        thisGameObjectPosition = thisGameObject.transform.position;
        thisGameObjectRotation = thisGameObject.transform.eulerAngles;

        refAttachCubePosition = refAttachCube.transform.position;
        refAttachCubeRotation = refAttachCube.transform.eulerAngles;

        if(refAttachCubePosition == attachObjectPosition && refAttachCubeRotation == attachObjectRotation)
        {
            audioSource.Play();
            Debug.LogError(thisGameObjectName + " Position + Rotation Achieved");
        }

        MusclePositionAndRotationCompareToMainModel();
    }



    public void MusclePositionAndRotationCompareToMainModel()
    {
        //Debug.LogError(thisGameObjectName + Vector3.Distance(thisGameObjectPosition, attachObjectPosition));

        //if (Vector3.Distance(thisGameObjectPosition.x, attachObjectPosition.x) > 1)
        //{

        //}

        //if(thisGameObjectPosition.x == attachObjectPosition.x) // || thisGameObjectPosition.x - 10 >= attachObjectPosition.x)
        //{
        //    Debug.LogError(thisGameObjectName + " x position within 10 above points");
        //}

        //if(thisGameObjectPosition.x - 10 >= attachObjectPosition.x)
        //{
        //    Debug.LogError(thisGameObjectName + " x position within 10 below points");
        //}

        //if (thisGameObjectPosition.y + 10 <= attachObjectPosition.y || thisGameObjectPosition.y - 10 >= attachObjectPosition.y)
        //{
        //    Debug.LogError(thisGameObjectName + " y position within 10 points");
        //}

        //if (thisGameObjectPosition.z + 10 <= attachObjectPosition.z || thisGameObjectPosition.z - 10 >= attachObjectPosition.z)
        //{
        //    Debug.LogError(thisGameObjectName + " z position within 10 points");
        //}

        //if (thisGameObjectRotation.x + 10 <= attachObjectRotation.x || thisGameObjectRotation.x - 10 >= attachObjectRotation.x)
        //{
        //    Debug.LogError(thisGameObjectName + " x rotation within 10 degrees");
        //}

        //if (thisGameObjectRotation.y + 10 <= attachObjectRotation.y || thisGameObjectRotation.y - 10 >= attachObjectRotation.y)
        //{
        //    Debug.LogError(thisGameObjectName + " y rotation within 10 degrees");
        //}

        //if (thisGameObjectRotation.z + 10 <= attachObjectRotation.z || thisGameObjectRotation.z - 10 >= attachObjectRotation.z)
        //{
        //    Debug.LogError(thisGameObjectName + " z rotation within 10 degrees");
        //}

    }
}
