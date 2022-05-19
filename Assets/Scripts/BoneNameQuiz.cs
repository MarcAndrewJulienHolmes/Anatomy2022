using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoneNameQuiz : MonoBehaviour
{
    public GameObject[] allBones;

    public QuizButton[] quizButton;

    public static List<string> allBonesNames = new List<string>();

    public TMP_Text quizQuestionTextMeshPro;

    public TMP_Text[] quizAnswerTextMeshPro;

    public string [] quizEntries;

    public static string quizEntriesStatic;

    public string lastBoneConnected;

    public string answerSelected;

    public static string lastBoneConnectedStatic;

    public bool generateQuizBool;

    public bool quizAvailable;

    public int boneScore, maxScore;


    // Start is called before the first frame update
    void Start()
    {
        SetAllBonesList();
        quizQuestionTextMeshPro.text = "Pick up and attach your first bone to the skeleton.";
        maxScore = allBones.Length;
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

    public void CheckAnswer()
    {
        var correctAnswer = lastBoneConnected;

        if(answerSelected == correctAnswer)
        {
            quizQuestionTextMeshPro.text = "Excellent, that's the correct answer.";
            boneScore++;
            quizAvailable = false;
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].FadeOut();
            }
        }
        else
        {
            quizQuestionTextMeshPro.text = "Unfortunately, that is not the right answer. The correct answer was " + correctAnswer + ".";
            quizAvailable = false;
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].FadeOut();
            }
        }
    }

    public void GenerateQuiz()
    {
        quizQuestionTextMeshPro.text = "Select the correct bone name from the options below.";

        quizAvailable = true;

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
                for (int n = 0; n < quizAnswerTextMeshPro.Length; n++)
                {
                    quizAnswerTextMeshPro[n].text = quizEntries[n];
                    quizButton[n].thisButtonAnswer = quizEntries[n];
                }
            }
        }
        for (int i = 0; i < quizButton.Length; i++)
        {
            quizButton[i].FadeIn();
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

        for (int i = 0; i < quizAnswerTextMeshPro.Length; i++)
        {
            quizAnswerTextMeshPro[i].text = "";
        }


    }
}
