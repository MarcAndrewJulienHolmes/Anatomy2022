using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackSceneControl : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;

    public TMP_Text boneSceneFeedbackText;
    public TMP_Text muscleLearningSceneFeedbackText;
    public TMP_Text muscleTestingSceneFeedbackText;


    void Awake()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boneSceneFeedbackText.text = "Bone Scene Score: " + sceneAndScoreManager.boneSceneScore + " of " + sceneAndScoreManager.boneSceneMaxScore + " bones attached.\nBone Scene Time Remaining: " + sceneAndScoreManager.boneSceneTime + " seconds.";
        muscleLearningSceneFeedbackText.text = "Muscle Learning Scene Score: " + sceneAndScoreManager.muscleLearningScore + " of " + sceneAndScoreManager.muscleLearningMaxScore + " muscle groups applied.\nMuscle Learning Time Remaining: " + sceneAndScoreManager.boneSceneTime + " seconds.";
        muscleTestingSceneFeedbackText.text = "Muscle Testing Scene Score: " + sceneAndScoreManager.muscleTestingScore + " of " + sceneAndScoreManager.muscleTestingMaxScore + " muscle groups applied.\nMuscle Testing Scene Time Remaining: " + sceneAndScoreManager.boneSceneTime + " seconds.";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
