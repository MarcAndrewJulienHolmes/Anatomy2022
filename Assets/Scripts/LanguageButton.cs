using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LanguageButton : MonoBehaviour
{
    public OVRScreenFade ovrScreenFade;

    public Button[] buttons;

    public string languageToSelect;

    public string thisGameObjectName;

    public Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        thisAnimator.Play("Normal");

    }

    public void GoToDemo()
    {
        languageToSelect = thisGameObjectName;
        StartCoroutine(SceneChange());
    }

    public IEnumerator SceneChange()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        if (languageToSelect == "Welsh")
        {
            SceneManager.LoadScene("Urdd_WelshVersion");
        }
        if (languageToSelect == "English")
        {
            SceneManager.LoadScene("Urdd_EnglishVersion");

        }
    }
}
