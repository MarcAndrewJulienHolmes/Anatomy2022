using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource selectTone, returnTone;


    public void PlaySelectTone()
    {
        selectTone.Play();
    }

    public void PlayReturnTone()
    {
        returnTone.Play();
    }
}
