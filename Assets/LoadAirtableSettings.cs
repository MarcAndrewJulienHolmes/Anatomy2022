using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


public class LoadAirtableSettings : MonoBehaviour
{
    public CreateRecord createRecord;

    public SceneAndScoreManager sceneAndScoreManager;

    public string airtableTitle, skeletalTime, muscleLearningTime, muscleTestingTime;

    public TMP_Text  airtableTMP, skeletalTMP, muscleLearningTMP, muscleTestTMP;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        SetAirtableTitle();
        SetSceneTimes();

    }

    public void SetAirtableTitle()
    {
        if(File.Exists(@Application.persistentDataPath + "/CustomAppSettingsFolder/AirtableTitle.txt"))
        {
            airtableTitle = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/AirtableTitle.txt");
            Debug.Log(airtableTitle);
            if (airtableTitle == "")
            {
                airtableTMP.text = "Airtable: No airtable set - Default in use";
                createRecord.TableName = "Anatomy Assessment 2022 Swansea";
                Debug.LogWarning("AirtableTitle.txt is empty or does not exsist!!");

            }
            else
            {
                createRecord.TableName = airtableTitle;
                airtableTMP.text = "Airtable: " + airtableTitle;
                Debug.Log("Airtable title set succesfully");
            }
        }
        else
        {
            airtableTMP.text = "No custom settings found - Anatomy Assessment 2022 Swansea Table in use";
            createRecord.TableName = "Anatomy Assessment 2022 Swansea";
            Debug.LogWarning("AirtableTitle.txt is empty or does not exsist!!");
        }

    }

    public void SetSceneTimes()
    {
        if(File.Exists(@Application.persistentDataPath + "/CustomAppSettingsFolder/SkeletalSceneTime.txt"))
        {
            skeletalTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/SkeletalSceneTime.txt");
            Debug.Log(skeletalTime);
            if (skeletalTime == "")
            {
                skeletalTMP.text = "Skeletal Scene Time: 10 minutes.";
                sceneAndScoreManager.boneSceneMaxTime = 10;
                Debug.LogWarning("SkeletalSceneTime.txt is empty or does not exsist!!");
            }
            else
            {
                if (float.TryParse(skeletalTime, out sceneAndScoreManager.boneSceneMaxTime))
                {
                    skeletalTMP.text = "Skeletal Scene Time: " + skeletalTime + " minutes."; ;
                    Debug.Log("Skeletal time set succesfully");
                }
            }
        }
        else
        {
            skeletalTMP.text = "No custom settings found - Defaults in use - Skeletal Time: 10 minutes.";
            sceneAndScoreManager.boneSceneMaxTime = 10;
            Debug.LogWarning("SkeletalSceneTime.txt is empty or does not exsist!!");
        }


        if(File.Exists(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleLearningTime.txt"))
        {
            muscleLearningTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleLearningTime.txt");
            Debug.Log(muscleLearningTime);
            if (muscleLearningTime == "")
            {
                muscleLearningTMP.text = "Muscle Learning Time: 10 minutes";
                sceneAndScoreManager.muscleLearningMaxTime = 10;
                Debug.LogWarning("MuscleLearningTime.txt is empty or does not exsist!!");
            }
            else
            {
                if (float.TryParse(muscleLearningTime, out sceneAndScoreManager.muscleLearningMaxTime))
                {
                    muscleLearningTMP.text = "Muscle Learning Time: " + muscleLearningTime + " minutes.";
                    Debug.Log("Muscle learning time set succesfully");
                }
            }
        }
        else
        {
            muscleLearningTMP.text = "No custom settings found - Defaults in use - Muscle Learning Time: 10 minutes";
            sceneAndScoreManager.muscleLearningMaxTime =10;
            Debug.LogWarning("MuscleLearningTime.txt is empty or does not exsist!!");
        }

        if(File.Exists(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleTestingTime.txt"))
        {
            muscleTestingTime = System.IO.File.ReadAllText(@Application.persistentDataPath + "/CustomAppSettingsFolder/MuscleTestingTime.txt");
            Debug.Log(muscleTestingTime);
            if (muscleTestingTime == "")
            {
                muscleTestTMP.text = "Muscle Test Time: 10 minutes.";
                sceneAndScoreManager.muscleTestingMaxTime = 10;
                Debug.LogWarning("MuscleTestingTime.txt is empty or does not exsist!!");
            }
            else
            {
                if (float.TryParse(muscleTestingTime, out sceneAndScoreManager.muscleTestingMaxTime))
                {
                    muscleTestTMP.text = "Muscle Test Time: " + muscleTestingTime + " minutes.";
                    Debug.Log("Muscle testing time set succesfully");
                }
            }
        }
        else
        {
            muscleTestTMP.text = "No custom settings found - Defaults in use - Muscle Test Time: 10 minutes.";
            sceneAndScoreManager.muscleTestingMaxTime = 10;
            Debug.LogWarning("MuscleTestingTime.txt is empty or does not exsist!!");
        }
    }
}
