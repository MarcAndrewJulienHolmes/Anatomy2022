using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnboardingButton : MonoBehaviour
{
    public RightHandPointer rightHandPointer;
    public OnboardingManager onboardingManager;

    public GameObject rightHand;
    public GameObject onboardingManagerHolder;

    public string thisGameObjectName;

    public string thisButtonAnswer;

    public Animator thisButtonAnimator;

    public TMP_Text thisOnboardingButtonText;

    public bool rightHandRay;

    public Collider thisCollider;

    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandPointer = rightHand.GetComponent<RightHandPointer>();

        onboardingManagerHolder = GameObject.Find("OnboardingScriptholder");
        onboardingManager = onboardingManagerHolder.GetComponent<OnboardingManager>();

        thisButtonAnimator.Play("FadeIn");

        thisCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        thisCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandRay)
        {
            HighlightedColour();
        }
        else if (!rightHandRay)
        {
            NormalColour();
        }
    }

    public void ButtonSelect()
    {
        if (thisOnboardingButtonText != null)
        {
            onboardingManager.answerSelected = thisButtonAnswer;
            onboardingManager.CheckAnswer();
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
