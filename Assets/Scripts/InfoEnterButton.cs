using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoEnterButton : MonoBehaviour
{
    public KeyboardTest keyboardTest;

    public string thisGameObjectName;

    public Animator thisButtonAnimator;

    public bool rightHandSelect, leftHandSelect;

    public Collider thisCollider;

    private void Awake()
    {
        thisButtonAnimator = GetComponent<Animator>();
        thisCollider = GetComponent<Collider>();
        keyboardTest = GameObject.Find("--- SCRIPTHOLDER ---").GetComponent<KeyboardTest>();
        thisButtonAnimator.Play("Start");

    }

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
        //thisCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandSelect || leftHandSelect)
        {
            HighlightedColour();
        }
        else
        { 
            NormalColour();
        }
    }

    public void ButtonSelect()
    {
        if (thisGameObjectName != null)
        {
            keyboardTest.buttonToActivate = thisGameObjectName;
            keyboardTest.ActivateButton();
            Debug.Log(thisGameObjectName + " was selected");
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
}
