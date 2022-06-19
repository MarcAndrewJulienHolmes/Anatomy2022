using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAndScoreManager : MonoBehaviour
{
    public BoneNameQuiz boneNameQuiz;

    public static float boneSceneScore, boneSceneMaxScore;

    public static float boneSceneTime;

    public static float muscleLearningTime;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoneSceneScores()
    {
        boneSceneMaxScore = boneNameQuiz.maxScore;
        boneSceneScore = boneNameQuiz.currentQuizScore;
       
    }

}
