using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAndScoreManager : MonoBehaviour
{
    public float boneSceneScore, boneSceneMaxScore;

    public float boneSceneTime;

    public int muscleLearningCount;

    public float muscleLearningTime;




    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
