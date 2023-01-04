using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using VRKeyboard.Utils;
using UnityEngine.UI;


public class MainMenuControl : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public OVRScreenFade ovrScreenFade;
    public AirtableRecord airtableRecord;
    public GameObject keyboardCanvas,studentNumberCanvas,mainMenuCanvas;
    public Animator mainMenuCanvasAni, studentNumberCanvasAni, keyboardAni;
    public string sceneToLoad;
    public TMP_Text studentNumberTMP;

    public Button loadTestSceneButton;


    public void LoadSkeletalScene()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        sceneToLoad = "SportScienceSkeletal_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public void LoadMuscleTrainingScene()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        sceneToLoad = "SportScienceMuscleLearning_EnglishVersion";
        StartCoroutine(SceneLoader());
        

    }

    public void LoadStudentNumberInput()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        StartCoroutine(LoadMuscleTestingMenu());
    }

    public void LoadMuscleTestingScene()
    {
        studentNumberCanvasAni.Play("AirTableInfoFadeOut");
        keyboardAni.Play("KeyboardFadeOut");
        sceneToLoad = "SportScienceMuscleTesting_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(1f);
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator LoadMuscleTestingMenu()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        yield return new WaitForSeconds(0.5f);
        mainMenuCanvas.SetActive(false);
        studentNumberCanvas.SetActive(true);
    }

    public void OpenKeyboard()
    {
        keyboardCanvas.SetActive(true);
    }

    public void Update()
    {
        studentNumberTMP.text = keyboardManager.Input;
        airtableRecord.studentNumber = studentNumberTMP.text.ToString();

        if (studentNumberCanvas.activeSelf)
        {
            if (studentNumberTMP.text.Length <= 5)
            {
                loadTestSceneButton.enabled = false;
            }
            else
            {
                loadTestSceneButton.enabled = true;
            }
        }
    }
}
