using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSceneControl : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;
    public ToTextSave toTextSave;
    public AirtableRecord airtableRecord;


    public TMP_Text boneSceneFeedbackText;
    public TMP_Text muscleLearningSceneFeedbackText;
    public TMP_Text muscleTestingSceneFeedbackText;


    void Awake()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
        toTextSave = GetComponent<ToTextSave>();
        airtableRecord = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<AirtableRecord>();

        RoundTimeValues();       
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayFeedback();
        SaveFeedback();
    }

    public void RoundTimeValues()
    {
        if (sceneAndScoreManager.boneSceneTime < 0)
        {
            sceneAndScoreManager.boneSceneTime = 0;
        }

        if (sceneAndScoreManager.muscleLearningTime < 0)
        {
            sceneAndScoreManager.muscleLearningTime = 0;
        }

        if (sceneAndScoreManager.muscleTestingTime < 0)
        {
            sceneAndScoreManager.muscleTestingTime = 0;
        }

        sceneAndScoreManager.boneSceneTime = Mathf.Round(sceneAndScoreManager.boneSceneTime * 100) * 0.01f;
        sceneAndScoreManager.muscleLearningTime = Mathf.Round(sceneAndScoreManager.muscleLearningTime * 100) * 0.01f;
        sceneAndScoreManager.muscleTestingTime = Mathf.Round(sceneAndScoreManager.muscleTestingTime * 100) * 0.01f;

    }

    public void DisplayFeedback()
    {
        boneSceneFeedbackText.text = "Bone Scene Score: " + sceneAndScoreManager.boneSceneScore + " of " + sceneAndScoreManager.boneSceneMaxScore + " bones attached.\nBone Scene Time Remaining: " + sceneAndScoreManager.boneSceneTime + " seconds.";
        muscleLearningSceneFeedbackText.text = "Muscle Learning Scene Score: " + sceneAndScoreManager.muscleLearningScore + " of " + sceneAndScoreManager.muscleLearningMaxScore + " muscle groups applied.\nMuscle Learning Time Remaining: " + sceneAndScoreManager.muscleLearningTime + " seconds.";
        muscleTestingSceneFeedbackText.text = "Muscle Testing Scene Score: " + sceneAndScoreManager.muscleTestingScore + " of " + sceneAndScoreManager.muscleTestingMaxScore + " points given for accuracy.\nMuscle Testing Scene Time Remaining: " + sceneAndScoreManager.muscleTestingTime + " seconds.";

    }

    public void SaveFeedback()
    {
        toTextSave.boneSceneFeedback = boneSceneFeedbackText.text.ToString();
        toTextSave.muscleLearningFeedback = muscleLearningSceneFeedbackText.text.ToString();
        toTextSave.muscleTestingFeedback = muscleTestingSceneFeedbackText.text.ToString();

        toTextSave.CreateTextFile();
        toTextSave.CreateCSVFile();

        airtableRecord.SendToAirtable();
    }


}
