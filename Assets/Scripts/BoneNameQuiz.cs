using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class BoneNameQuiz : MonoBehaviour
{
    public Timer timer;

    public GameObject[] allBones;

    public QuizButton[] quizButton;

    public Animator quizBoardAnimator;

    public AudioSource negativeTone, positiveTone;

    public static List<string> allBonesNames = new List<string>();

    public TMP_Text quizQuestionTextMeshPro, quizScoreFeedbackTextMeshPro, introTextMeshPro;

    public TMP_Text[] quizAnswerTextMeshPro;

    public string [] quizEntries;

    public static string quizEntriesStatic;

    public string lastBoneConnected;

    public string answerSelected, correctAnswer;

    public static string lastBoneConnectedStatic;

    public bool generateQuizBool;

    public bool quizAvailable;

    public float currentQuizScore, maxScore, runningMaxScore, scorePercent, timeTaken;

    public ParticleSystem[] confetti;

    public AudioSource[] celebrateSFX;

    public OVRScreenFade ovrScreenFade;

    public OnboardingManager onboardingManager;
    public GameObject onboardingHolder;

    public bool timeRunOut = false;


    // Start is called before the first frame update
    void Start()
    {
        onboardingHolder = GameObject.Find("---ONBOARDING ---");
        onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();
        SetAllBonesList();
        maxScore = allBones.Length;

    }

    public void CheckAnswer()
    {
        if (!timeRunOut)
        {
            onboardingManager.answerQuiz = true;
            onboardingManager.UpdateChecklist();

            correctAnswer = lastBoneConnected;

            runningMaxScore++;

            if (answerSelected == correctAnswer)
            {
                CorrectAnswer();
            }
            else
            {
                IncorrectAnswer();
            }
        }

    }

    public void CorrectAnswer()
    {
        if (!timeRunOut)
        {
            currentQuizScore++;
            positiveTone.Play();
            quizBoardAnimator.Play("CorrectAnswer");
            scorePercent = currentQuizScore / runningMaxScore * 100;
            var scorePercentRounded = System.Math.Round(scorePercent, 1);
            quizQuestionTextMeshPro.text = "Excellent, " + correctAnswer + " is the correct answer. \n \n Find and add the next bone in the sequence.";
            quizScoreFeedbackTextMeshPro.text = "Your current score is " + scorePercentRounded + "%.";
            quizAvailable = false;
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].FadeOut();
            }

            if (runningMaxScore == maxScore)
            {
                QuizCompleteFunction();
            }
        }

    }

    public void IncorrectAnswer()
    {
        if (!timeRunOut)
        {
            negativeTone.Play();
            quizBoardAnimator.Play("IncorrectAnswer");
            scorePercent = currentQuizScore / runningMaxScore * 100;
            var scorePercentRounded = System.Math.Round(scorePercent, 1);
            quizQuestionTextMeshPro.text = "Unfortunately, that is not the right answer. The correct answer was " + correctAnswer + ".";
            quizScoreFeedbackTextMeshPro.text = "Your current score is " + scorePercentRounded + "%.";
            quizAvailable = false;
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].FadeOut();
            }

            if (runningMaxScore == maxScore)
            {
                QuizCompleteFunction();
            }
        }
    }

    public void GenerateQuiz()
    {
        if (!timeRunOut)
        {
            timer.StartTimer();
            introTextMeshPro.text = "";
            quizQuestionTextMeshPro.text = "Select the correct bone name from the options below.";
            quizAvailable = true;
            SetAllBonesList();
            ResetQuizEntries();
            lastBoneConnectedStatic = lastBoneConnected;
            int randomNum = Random.Range(0, quizEntries.Length);
            quizEntries[randomNum] = lastBoneConnected;
            for (int i = 0; i < quizEntries.Length; i++)
            {
                if (quizEntries[i] == lastBoneConnected)
                {

                }
                else
                {
                    GenerateQuizAnswers();
                    quizEntries[i] = quizEntriesStatic;
                }

                if (i == quizEntries.Length - 1)
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
        if (!timeRunOut)
        {
            allBonesNames.Clear();

            for (int i = 0; i < allBones.Length; i++)
            {
                allBonesNames.Add(allBones[i].name);
            }
        }

    }

    public void ResetQuizEntries()
    {
        if (!timeRunOut)
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

    public void QuizCompleteFunction()
    {
        timeRunOut = true;
        StartCoroutine(QuizComplete());

    }

    public IEnumerator QuizComplete()
    {
        timer.StopTimer();

        if (quizAvailable)
        {
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].FadeOut();
            }
        }
        else
        {
            quizAvailable = true;

        }

        timeTaken = timer.currentTime;

        yield return new WaitForSeconds(0.5f);

        scorePercent = currentQuizScore / maxScore * 100;
        var scorePercentRounded = System.Math.Round(scorePercent, 1);

        quizQuestionTextMeshPro.text = "Well done, overall you scored " + scorePercentRounded + " %!!";

        quizScoreFeedbackTextMeshPro.text = "Well done, you got " + currentQuizScore + " out of " + maxScore + " correct.";

        for (int i = 0; i < celebrateSFX.Length; i++)
        {
            celebrateSFX[i].Play();
        }

        for (int i = 0; i < confetti.Length; i++)
        {
            confetti[i].Play();
        }

        yield return new WaitForSeconds(10f);

        ovrScreenFade.FadeOut();

        yield return new WaitForSeconds(2f);

    }
}
