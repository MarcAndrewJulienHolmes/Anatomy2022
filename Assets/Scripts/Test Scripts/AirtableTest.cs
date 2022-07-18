using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirtableTest : MonoBehaviour
{
    public CreateRecord createRecord;

    public string JSONRequest;

    public bool recordData;

    public string JSONString, dateTime, skeletonTime, skeletonScore;

    private void Start()
    {
        dateTime = "18.07.2022";
        skeletonTime = "12.25";
        skeletonScore = "30";
    }

    public void Update()
    {
        if (recordData)
        {
            BeginCoroutine();
            if (recordData)
            {
                recordData = false;
            }
        }
    }

    public void BeginCoroutine()
    {
        //StartCoroutine(SendToAirtable());
        JSONString = "{\"fields\": {\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Skeleton Time\":\"" + skeletonTime + "\", " +
                                    "\"Skeleton Score\":\"" + skeletonScore + "\"}}";
        createRecord.NewRecordJson = JSONString;
        createRecord.CreateAirtableRecord();


    }


    public IEnumerator SendToAirtable()
    {
        JSONString = "{\"fields\": {\"Date and Time\":\"" + dateTime + "\"}}";
        createRecord.NewRecordJson = JSONString;
        createRecord.CreateAirtableRecord();
        yield return new WaitForSeconds(0.25f);

        JSONString = "{\"fields\": {\"Skeleton Time\":\"" + skeletonTime + "\"}}";
        createRecord.NewRecordJson = JSONString;
        createRecord.CreateAirtableRecord();
        yield return new WaitForSeconds(0.25f);

        JSONString = "{\"fields\": {\"Skeleton Score\":\"" + skeletonScore + "\"}}";
        createRecord.NewRecordJson = JSONString;
        createRecord.CreateAirtableRecord();
        yield return new WaitForSeconds(0.25f);
    }
}