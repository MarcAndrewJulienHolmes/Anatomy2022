using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizButton : MonoBehaviour
{
    public string thisGameObjectName;

    public string thisButtonAnswer;

    public Animator thisButtonAnimator;

    

    // Start is called before the first frame update
    void Start()
    {
        thisGameObjectName = transform.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonSelect()
    {
        Debug.Log(thisGameObjectName + " was selected");
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
