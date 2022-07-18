using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;
using TMPro;


public class KeyboardTest : MonoBehaviour
{
    public Keyboard keyboard;

    public string buttonToActivate;

    public bool activateKeyboard;
    public string aPIKeyString;
    public string appKeyString;
    public string tableNameString;

    public TMP_Text aPIKeyTMP = null;
    public TMP_Text appKeyTMP = null;
    public TMP_Text tableNameTMP = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aPIKeyTMP.text = aPIKeyString;
        appKeyTMP.text = appKeyString;
        tableNameTMP.text = tableNameString;
    }

    public void ActivateButton()
    {
        if(buttonToActivate == "APIKeyBTN")
        {
            EnterAPIKey();
        }
        else if(buttonToActivate == "AppKeyBTN")
        {
            EnterAppKey();
        }
        else if(buttonToActivate == "TableNameBTN")
        {
            EnterTableName();
        }                 
    }

    public void EnterAPIKey()
    {
        keyboard.Enable();
        aPIKeyString = keyboard.text;
    }

    public void EnterAppKey()
    {
        keyboard.Enable();
        appKeyString = keyboard.text;
    }

    public void EnterTableName()
    {
        keyboard.Enable();
        tableNameString = keyboard.text;
    }
}
