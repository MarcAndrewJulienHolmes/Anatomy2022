using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingManager : MonoBehaviour
{
    public GameObject OVROnboardingPosition, OVRQuizPosition;

    public GameObject OVRRig;

    public GameObject quizBoard, onboardingBoard;

    public bool languageSelected, bonePositionsSelected;

    public TMP_Text questionText, button1Text, button2Text;

    public GameObject onboardingButton1, onboardingButton2;

    public string answerSelected;

    public OVRScreenFade screenFade;

    // Start is called before the first frame update
    void Start()
    {
        OVRRig.transform.position = OVROnboardingPosition.transform.position;
        OVRRig.transform.rotation = OVROnboardingPosition.transform.rotation;

        quizBoard.SetActive(false);
        onboardingBoard.SetActive(true);

        StartCoroutine(SetLanguage());

    }

    // Update is called once per frame
    void Update()
    {
        if (languageSelected)
        {
            StartCoroutine(SetBoneLocations());
        }

        if (bonePositionsSelected)
        {
            StartCoroutine(SetBoneLocations());
        }
    }

    public void CheckAnswer()
    {
        if (answerSelected == "Cymraeg")
        {
            WelshLanguage();
            languageSelected = true;
        }

        if (answerSelected == "English")
        {
            EnglishLanguage();
            languageSelected = true;
        }

        if (answerSelected == "Random")
        {
            RandomBonePositions();
            bonePositionsSelected = true;
        }

        if (answerSelected == "Ordered")
        {
            RegularBonePositions();
            bonePositionsSelected = true;
        }
    }

    public void WelshLanguage()
    {
        Debug.Log("Welsh selected");
    }

    public void EnglishLanguage()
    {
        Debug.Log("English selected");
    }

    public void RandomBonePositions()
    {
        Debug.Log("Random bone positions selected");
    }

    public void RegularBonePositions()
    {
        Debug.Log("Regular bone positions selected");
    }

    public IEnumerator SetLanguage()
    {
        questionText.text = "Select your language.";
        button1Text.text = "Cymraeg";
        onboardingButton1.GetComponent<OnboardingButton>().thisButtonAnswer = "Cymraeg";
        button2Text.text = "English";
        onboardingButton2.GetComponent<OnboardingButton>().thisButtonAnswer = "English";
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator SetBoneLocations()
    {
        questionText.text = "Select your bone positions.";
        button1Text.text = "Random";
        onboardingButton1.GetComponent<OnboardingButton>().thisButtonAnswer = "Random";

        button2Text.text = "Ordered";
        onboardingButton2.GetComponent<OnboardingButton>().thisButtonAnswer = "Ordered";

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MoveOVRRig()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(1f);
        onboardingBoard.SetActive(false);
        quizBoard.SetActive(true);
        OVRRig.transform.position = OVRQuizPosition.transform.position;
        OVRRig.transform.rotation = OVRQuizPosition.transform.rotation;
        yield return new WaitForSeconds(1f);
        screenFade.FadeIn();

    }
}
