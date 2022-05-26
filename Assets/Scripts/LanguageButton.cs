using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    public LanguageSelect languageSelect;

    public bool rightHandRay;

    public string thisGameObjectName;

    public Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        thisAnimator.Play("Normal");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSelect()
    {
        languageSelect.activate = true;
        languageSelect.languageToSelect = thisGameObjectName;

    }
}
