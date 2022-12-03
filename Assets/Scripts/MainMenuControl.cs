using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using VRKeyboard.Utils;


public class MainMenuControl : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public OVRScreenFade ovrScreenFade;
    public AirtableRecord airtableRecord;
    public GameObject keyboardCanvas,studentNumberCanvas,mainMenuCanvas;
    public Animator mainMenuCanvasAni, studentNumberCanvasAni, keyboardAni;
    public string sceneToLoad;
    public TMP_Text studentNumberTMP;


    public void LoadSkeletalScene()
    {
        sceneToLoad = "SportScienceSkeletal_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public void LoadMuscleTrainingScene()
    {
        sceneToLoad = "SportScienceMuscleLearning_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public void LoadMuscleTestingScene()
    {

        StartCoroutine(LoadMuscleTestingMenu());
        //sceneToLoad = "SportScienceMuscleTesting_EnglishVersion";
        //StartCoroutine(SceneLoader());
    }

    public void SettingsMenu()
    {
    }

    public IEnumerator SceneLoader()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
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
        keyboardCanvas.SetActive(true);
        studentNumberCanvas.SetActive(true);
        //studentNumberCanvasAni.Play("AirTableInfoFadeIn");
    }

    public void Update()
    {
        studentNumberTMP.text = keyboardManager.Input;
        airtableRecord.studentNumber = studentNumberTMP.text.ToString();
    }
}
