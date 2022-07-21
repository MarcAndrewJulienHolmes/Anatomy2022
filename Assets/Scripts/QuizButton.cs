using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizButton : MonoBehaviour
{
    public GameObject rightHand, leftHand;

    public BoneNameQuiz boneNameQuiz;

    public string thisGameObjectName;

    public GameObject thisGameObject;

    public string thisButtonAnswer;

    public Animator thisButtonAnimator;

    public TMP_Text thisQuizButtonText;

    public bool rightHandSelect, leftHandSelect;

    public Collider thisCollider;

    public Button thisButton;

    private void Awake()
    {
        //thisButtonAnimator.Play("Start");
        thisCollider = GetComponent<Collider>();
        thisButton = GetComponent<Button>();
        thisGameObject = transform.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        thisGameObject.layer = 0;
        thisCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((rightHandSelect || leftHandSelect) && boneNameQuiz.quizAvailable)
        {
            HighlightedColour();
        }
        else if ((!rightHandSelect || leftHandSelect) && boneNameQuiz.quizAvailable && !boneNameQuiz.timeRunOut)
        {
            NormalColour();
        }
    }

    public void ButtonSelect()
    {
        if (thisQuizButtonText != null)
        {
            boneNameQuiz.answerSelected = thisButtonAnswer;
            boneNameQuiz.CheckAnswer();
            Debug.Log(thisButtonAnswer + " was selected");

        }
        else
        {
            Debug.LogError("Answer not applied to button!!!");
        }
    }

    public void NormalColour()
    {
        //thisButtonAnimator.Play("Normal");
    }

    public void HighlightedColour()
    {
        //thisButtonAnimator.Play("Highlighted");
    }

    public void FadeOut()
    {
        thisButtonAnimator.Play("FadeOut");
        thisGameObject.layer = 0;
        thisCollider.enabled = false;

    }

    public void FadeIn()
    {
        thisButtonAnimator.Play("FadeIn");
        thisGameObject.layer = 9;
        thisCollider.enabled = true;
    }
}
