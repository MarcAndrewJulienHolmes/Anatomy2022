using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LanguageSelect : MonoBehaviour
{
    public OVRScreenFade ovrScreenFade;

    public string languageToSelect;

    public bool activate, coroutineRunning;


    //// Update is called once per frame
    //void Update()
    //{
    //    if (activate == true && !coroutineRunning)
    //    {
    //        StartCoroutine(SceneChange());
    //    }
    //}


    public void GoToDemo()
    {
        StartCoroutine(SceneChange());
    }

    public IEnumerator SceneChange()
    {
        coroutineRunning = true;
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        if(languageToSelect == "Welsh")
        {
            SceneManager.LoadScene("Urdd_WelshVersion");
        }
        if(languageToSelect == "English")
        {
            SceneManager.LoadScene("Urdd_EnglishVersion");

        }
    }
}
