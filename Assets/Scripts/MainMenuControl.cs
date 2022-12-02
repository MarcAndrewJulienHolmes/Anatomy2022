using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public OVRScreenFade ovrScreenFade;
    public Animator canvasAnimation  ;
    public string sceneToLoad;


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
        sceneToLoad = "SportScienceMuscleTesting_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public void SettingsMenu()
    {
    }

    public IEnumerator SceneLoader()
    {
        canvasAnimation.Play("AirTableInfoFadeOut");
        yield return new WaitForSeconds(1f);
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);

    }

    public IEnumerator LoadSettingsMenu()
    {
        canvasAnimation.Play("AirTableInfoFadeOut");
        yield return new WaitForSeconds(1f);



    }


}
