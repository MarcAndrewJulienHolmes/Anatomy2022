using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ToTextSave : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;

    public string boneSceneFeedback;
    public string muscleLearningFeedback;
    public string muscleTestingFeedback;

    public string directoryName;

    public string dateTime;
    public string date;
    public string time;

    private void Awake()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        date = System.DateTime.Now.ToString("dd.MM.yyyy");
        time = System.DateTime.Now.ToString("HH.mm");

        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();

        directoryName = Application.persistentDataPath + "/Score_Records/" + date + "/";

        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }
    }


    public void CreateTextFile()
    {
        string textDocumentName = directoryName + time + ".txt";

        //File.WriteAllText(textDocumentName, "Score Record " + dateTime);

        if (!File.Exists(textDocumentName))
        {
            File.WriteAllText(textDocumentName, "Score Record: " + dateTime + "\n \n");
        }

        File.AppendAllText(textDocumentName, boneSceneFeedback + "\n \n" + muscleLearningFeedback + "\n \n" + muscleTestingFeedback);
    }

    public void CreateCSVFile()
    {
        string CSVDocumentName = Application.persistentDataPath + "/Score_Records/All_Score_Records.csv";

        TextWriter tw = new StreamWriter(CSVDocumentName, true);
        tw.WriteLine(sceneAndScoreManager.boneSceneScore.ToString() + ",- bone score., " + sceneAndScoreManager.boneSceneMaxScore.ToString() + ",- bone max score.," + sceneAndScoreManager.boneSceneTime.ToString() + ",- seconds remaining.," +
                        sceneAndScoreManager.muscleLearningScore.ToString() + ",- muscle learning score.," + sceneAndScoreManager.muscleLearningMaxScore.ToString() + ",- muscle learning max score.," + sceneAndScoreManager.muscleLearningTime.ToString() + ",- seconds remaining.," +
                        sceneAndScoreManager.muscleTestingScore.ToString() + ",- muscle testing score.," + sceneAndScoreManager.muscleTestingMaxScore.ToString() + ",- muscle testing max score.," + sceneAndScoreManager.muscleTestingTime.ToString() + ",- time remaining.");
        tw.Close();

    }

}
