using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoneNameQuiz : MonoBehaviour
{
    public GameObject[] allBones;

    public static List<string> allBonesNames = new List<string>();

    public TMP_Text quizSpace1, quizSpace2, quizSpace3, quizSpace4;

    public string [] quizEntries;

    public static string quizEntriesStatic;

    public string lastBoneConnected;

    public static string lastBoneConnectedStatic;



    public bool generateQuizBool;


    // Start is called before the first frame update
    void Start()
    {
        SetAllBonesList();
    }

    // Update is called once per frame
    void Update()
    {
        if (generateQuizBool)
        {
            GenerateQuiz();
            generateQuizBool = false;
        }
    }



    public void GenerateQuiz()
    {
        SetAllBonesList();
        ResetQuizEntries();
        lastBoneConnectedStatic = lastBoneConnected;
        int randomNum = Random.Range(0, quizEntries.Length);
        quizEntries[randomNum] = lastBoneConnected;
        for (int i = 0; i < quizEntries.Length; i++)
        {
            if(quizEntries[i] == lastBoneConnected)
            {
                
            }
            else
            {
                GenerateQuizAnswers();
                quizEntries[i] = quizEntriesStatic;
            }

            if(i == quizEntries.Length - 1)
            {
                quizSpace1.text = quizEntries[0];
                quizSpace2.text = quizEntries[1];
                quizSpace3.text = quizEntries[2];
                quizSpace4.text = quizEntries[3];
            }
        }
    }

    public static void GenerateQuizAnswers()
    {
        allBonesNames.Remove(lastBoneConnectedStatic);
        string stringToRetrieve = GetRandomItem(allBonesNames);        
        quizEntriesStatic = stringToRetrieve;
        allBonesNames.Remove(quizEntriesStatic);
    }

    public static string GetRandomItem(List<string> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        string printRandom = listToRandomize[randomNum];
        return printRandom;
    }

    public void SetAllBonesList()
    {
        allBonesNames.Clear();

        for (int i = 0; i < allBones.Length; i++)
        {
            allBonesNames.Add(allBones[i].name);
        }
    }

    public void ResetQuizEntries()
    {
        for (int i = 0; i < quizEntries.Length; i++)
        {
            quizEntries[i] = "";
        }
    }
}
