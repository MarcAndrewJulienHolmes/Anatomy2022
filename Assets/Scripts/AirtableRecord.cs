using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirtableRecord : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;
    public SetEnvironment setEnvironment;
    public CreateRecord createRecord;

    public bool recordData, studentNumberTest;

    public string dateTime;

    public string JSONString;

    private void Awake()
    {
        sceneAndScoreManager = GetComponent<SceneAndScoreManager>();
        setEnvironment = GetComponent<SetEnvironment>();
        createRecord = GetComponent<CreateRecord>();
    }

    private void Start()
    {

    }

#if UNITY_EDITOR
    
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

        if (studentNumberTest)
        {
            SendStudentNumberTestToAirtable();
            if (studentNumberTest)
            {
                studentNumberTest = false;
            }
        }
    }
#endif

    public void AttemptConnect()
    {
        StartCoroutine(AttemptConnectCoroutine());
    }

    public IEnumerator AttemptConnectCoroutine()
    {
        setEnvironment.PrepareEnvironment();
        yield return new WaitForSeconds(1f);
        createRecord.CreateAirtableRecord();
    }

    public void SendToAirtable()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Bone Scene Time Remaining\":\"" + sceneAndScoreManager.boneSceneTime + "\", " +
                                    "\"Bone Scene Score\":\"" + sceneAndScoreManager.boneSceneScore + "\", " +
                                    "\"Bone Scene Max Score\":\"" + sceneAndScoreManager.boneSceneMaxScore + "\", " +
                                    "\"Muscle Learning Time Remaining\":\"" + sceneAndScoreManager.muscleLearningTime + "\", " +
                                    "\"Muscle Learning Score\":\"" + sceneAndScoreManager.muscleLearningScore + "\", " +
                                    "\"Muscle Learning Max Score\":\"" + sceneAndScoreManager.muscleLearningMaxScore + "\", " +
                                    "\"Muscle Testing Time Remaining\":\"" + sceneAndScoreManager.muscleTestingTime + "\", " +
                                    "\"Muscle Testing Score\":\"" + sceneAndScoreManager.muscleTestingScore + "\", " +
                                    "\"Muscle Testing Max Score\":\"" + sceneAndScoreManager.muscleTestingMaxScore + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        AttemptConnect();
    }

    public void SendStudentNumberTestToAirtable()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Student Number\":\"" + "9999999" + "\", " +
                                    "\"Test Time\":\"" + 123456 + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        AttemptConnect();
    }


}

