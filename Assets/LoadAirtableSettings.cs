using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadAirtableSettings : MonoBehaviour
{
    public CreateRecord createRecord;

    public SceneAndScoreManager sceneAndScoreManager;

    public string airtableTitle, skeletalTime, muscleLearningTime, muscleTestingTime;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        SetAirtableTitle();
        SetSceneTimes();

    }

    public void SetAirtableTitle()
    {
        airtableTitle = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/AirtableTitle.txt");
        Debug.Log(airtableTitle);
        if (airtableTitle == "")
        {
            Debug.LogWarning("AirtableTitle.txt is empty or does not exsist!!");

        }
        else
        {
            createRecord.TableName = airtableTitle;
            Debug.Log("Airtable title set succesfully");
        }
    }

    public void SetSceneTimes()
    {
        skeletalTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/SkeletalSceneTime.txt");
        Debug.Log(skeletalTime);
        if (skeletalTime == "")
        {
            Debug.LogWarning("SkeletalSceneTime.txt is empty or does not exsist!!");
        }
        else
        {
            if(float.TryParse(skeletalTime, out sceneAndScoreManager.boneSceneMaxTime))
            {
                Debug.Log("Skeletal time set succesfully");
            }
        }


        muscleLearningTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleLearningTime.txt");
        Debug.Log(muscleLearningTime);
        if (muscleLearningTime == "")
        {
            Debug.LogWarning("MuscleLearningTime.txt is empty or does not exsist!!");
        }
        else
        {
            if (float.TryParse(muscleLearningTime, out sceneAndScoreManager.muscleLearningMaxTime))
            {
                Debug.Log("Muscle learning time set succesfully");
            }
        }

        muscleTestingTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleTestingTime.txt");
        Debug.Log(muscleTestingTime);
        if (muscleTestingTime == "")
        {
            Debug.LogWarning("MuscleTestingTime.txt is empty or does not exsist!!");
        }
        else
        {
            if (float.TryParse(muscleTestingTime, out sceneAndScoreManager.muscleTestingMaxTime))
            {
                Debug.Log("Muscle testing time set succesfully");
            }
        }
    }
}
