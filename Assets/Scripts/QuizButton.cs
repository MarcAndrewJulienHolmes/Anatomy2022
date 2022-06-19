using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizButton : MonoBehaviour
{
    public RightHandPointer rightHandPointer;

    public GameObject rightHand;

    public BoneNameQuiz boneNameQuiz;

    public string thisGameObjectName;

    public string thisButtonAnswer;

    public Animator thisButtonAnimator;

    public TMP_Text thisQuizButtonText;

    public bool rightHandRay;

    public Collider thisCollider;

    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandPointer = rightHand.GetComponent<RightHandPointer>();

        thisButtonAnimator.Play("Start");

        thisCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        thisCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandRay && boneNameQuiz.quizAvailable)
        {
            HighlightedColour();
        }
        else if (!rightHandRay && boneNameQuiz.quizAvailable && !boneNameQuiz.timeRunOut)
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
        thisButtonAnimator.Play("Normal");
    }

    public void HighlightedColour()
    {
        thisButtonAnimator.Play("Highlighted");
    }

    public void FadeOut()
    {
        thisButtonAnimator.Play("FadeOut");
        thisCollider.enabled = false;
    }

    public void FadeIn()
    {
        thisButtonAnimator.Play("FadeIn");
        thisCollider.enabled = true;
    }
}
