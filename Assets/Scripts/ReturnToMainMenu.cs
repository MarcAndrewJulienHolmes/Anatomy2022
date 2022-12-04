using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public OVRScreenFade ovrScreenFade;

    private void Awake()
    {
        ovrScreenFade = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OVRScreenFade>();
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuRoutine());
    }

    public IEnumerator GoToMainMenuRoutine()
    {
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SportScienceMainMenu_EnglishVersion");
    }
}
