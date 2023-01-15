using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;

    public bool welshLanguage;

    bool timerActive = false;
    public float currentTime;
    public float startMinutes;
    public TMP_Text timerDisplay;

    public UnityEvent timerFinished;

    public bool skeletalScene, muscleLearningScene, muscleTestingScene;

    private void Awake()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (skeletalScene)
        {
            startMinutes = sceneAndScoreManager.boneSceneMaxTime;
            sceneAndScoreManager.skeletalScene = true;
        }
        if (muscleLearningScene)
        {
            startMinutes = sceneAndScoreManager.muscleLearningMaxTime;
            sceneAndScoreManager.muscleLearningScene = true;
        }
        if (muscleTestingScene)
        {
            startMinutes = sceneAndScoreManager.muscleTestingMaxTime;
            sceneAndScoreManager.muscleTestingScene = true;
        }

        currentTime = startMinutes * 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                StopTimer();
                timerFinished.Invoke();
                Debug.Log("Timer finished");
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        if (welshLanguage)
        {
            timerDisplay.text = "Amser yn weddill: " + time.ToString(@"mm\:ss");  // + ":" + time.Seconds.ToString(@"ss");
        }
        else
        {
            timerDisplay.text = "Time Remaing: " + time.ToString(@"mm\:ss");  // + ":" + time.Seconds.ToString(@"ss");
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
