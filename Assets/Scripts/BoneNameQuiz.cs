using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class BoneNameQuiz : MonoBehaviour
{
    public GameObject[] allBones;

    public QuizButton[] quizButton;

    public Animator quizBoardAnimator;

    public AudioSource negativeTone, positiveTone;

    public static List<string> allBonesNames = new List<string>();

    public TMP_Text quizQuestionTextMeshPro, quizScoreFeedbackTextMeshPro;

    public TMP_Text[] quizAnswerTextMeshPro;

    public string [] quizEntries;

    public static string quizEntriesStatic;

    public string lastBoneConnected;

    public string answerSelected, correctAnswer;

    public static string lastBoneConnectedStatic;

    public bool generateQuizBool;

    public bool quizAvailable;

    public float currentQuizScore, maxScore, runningMaxScore, scorePercent;

    public bool welshLanguage;

    public ParticleSystem confeti1, confetti2;


    // Start is called before the first frame update
    void Start()
    {
        SetAllBonesList();
        //quizQuestionTextMeshPro.text = "Pick up and attach your first bone to the skeleton.";
        maxScore = allBones.Length;
        if(SceneManager.GetActiveScene().name == "Urdd_WelshVersion")
        {
            welshLanguage = true;
        }
        else
        {
            welshLanguage = false;
        }

        if (welshLanguage)
        {
            quizQuestionTextMeshPro.text = "Dewch o hyd i’r asgwrn rydych chi’n gallu ei weld mewn lliw glas ar y sgerbwd, a’i ychwanegu at y model i ddatgloi’r asgwrn nesaf.";

        }
        else
        {
            quizQuestionTextMeshPro.text = "Find the bone that you can see is highlighted in blue on the skeleton, and add it to the model to unlock the next bone in the sequence.";
        }
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
        correctAnswer = lastBoneConnected;

        runningMaxScore++;

        if(answerSelected == correctAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }
    }

    public void CorrectAnswer()
    {
        currentQuizScore++;
        positiveTone.Play();
        quizBoardAnimator.Play("CorrectAnswer");
        scorePercent = currentQuizScore / runningMaxScore * 100;
        var scorePercentRounded = System.Math.Round(scorePercent, 1);
        if (welshLanguage)
        {
            quizQuestionTextMeshPro.text = "Gwych, " + correctAnswer + " yw’r ateb cywir.";
            quizScoreFeedbackTextMeshPro.text = "Eich sgȏr ar hyn o bryd yw " + scorePercentRounded + "%.";

        }
        else
        {
            quizQuestionTextMeshPro.text = "Excellent, " + correctAnswer + " is the correct answer.";
            quizScoreFeedbackTextMeshPro.text = "Your current score is " + scorePercentRounded + "%.";
        }

        quizAvailable = false;
        for (int i = 0; i < quizButton.Length; i++)
        {
            quizButton[i].FadeOut();
        }

        if (runningMaxScore == maxScore)
        {
            StartCoroutine(QuizComplete());
        }
    }

    public void IncorrectAnswer()
    {
        negativeTone.Play();
        quizBoardAnimator.Play("IncorrectAnswer");
        scorePercent = currentQuizScore / runningMaxScore * 100;
        var scorePercentRounded = System.Math.Round(scorePercent, 1);
        if (welshLanguage)
        {
            quizQuestionTextMeshPro.text = "Yn anffodus, nid dyna’r ateb cywir. Yr ateb cywir yw " + correctAnswer + ".";
            quizScoreFeedbackTextMeshPro.text = "Eich sgȏr ar hyn o bryd yw " + scorePercentRounded + "%.";
        }
        else
        {
            quizQuestionTextMeshPro.text = "Unfortunately, that is not the right answer. The correct answer was " + correctAnswer + ".";
            quizScoreFeedbackTextMeshPro.text = "Your current score is " + scorePercentRounded + "%.";

        }

        quizAvailable = false;
        for (int i = 0; i < quizButton.Length; i++)
        {
            quizButton[i].FadeOut();
        }

        if(runningMaxScore == maxScore)
        {
            StartCoroutine(QuizComplete());
        }

    }

    public void GenerateQuiz()
    {

        if (welshLanguage)
        {
            quizQuestionTextMeshPro.text = "Dewiswch yr enw asgwrn cywir o'r opsiynau isod.";
        }
        else
        {
            quizQuestionTextMeshPro.text = "Select the correct bone name from the options below.";
        }

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

    public IEnumerator QuizComplete()
    {
        confeti1.Play();
        confetti2.Play();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Urdd_LanguageSelect");
    }
}
