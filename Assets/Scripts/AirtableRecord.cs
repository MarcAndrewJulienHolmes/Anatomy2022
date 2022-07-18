using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirtableRecord : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;

    public CreateRecord createRecord;

    public string JSONRequest;

    public bool recordData;

    public string dateTime;

    public string JSONString;

    private void Awake()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();

    }

    private void Start()
    {

        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

    }

    public void Update()
    {
        if (recordData)
        {
            SendToAirtable();
            if (recordData)
            {
                recordData = false;
            }
        }
    }

    public void SendToAirtable()
    {
        //StartCoroutine(SendToAirtable());
        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Bone Scene Time\":\"" + sceneAndScoreManager.boneSceneTime + "\", " +
                                    "\"Bone Scene Score\":\"" + sceneAndScoreManager.boneSceneScore + "\", " +
                                    "\"Bone Scene Max Score\":\"" + sceneAndScoreManager.boneSceneMaxScore + "\", " +
                                    "\"Muscle Learning Time\":\"" + sceneAndScoreManager.muscleLearningTime + "\", " +
                                    "\"Muscle Learning Score\":\"" + sceneAndScoreManager.muscleLearningScore + "\", " +
                                    "\"Muscle Learning Max Score\":\"" + sceneAndScoreManager.muscleLearningMaxScore + "\", " +
                                    "\"Muscle Testing Time\":\"" + sceneAndScoreManager.muscleTestingTime + "\", " +
                                    "\"Muscle Testing Score\":\"" + sceneAndScoreManager.muscleTestingScore + "\", " +
                                    "\"Muscle Testing Max Score\":\"" + sceneAndScoreManager.muscleTestingMaxScore + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        createRecord.CreateAirtableRecord();


    }
}

