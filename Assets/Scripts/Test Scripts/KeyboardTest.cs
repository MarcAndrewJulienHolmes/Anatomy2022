using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;
using TMPro;
using VRKeyboard.Utils;


public class KeyboardTest : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public GameObject keyboardObject;

    public bool activateKeyboard;

    public bool apiActive, appKeyActive, tableNameActive;

    public TMP_Text aPIKeyTMP;
    public TMP_Text appKeyTMP;
    public TMP_Text tableNameTMP;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (apiActive)
        {
            aPIKeyTMP.text = keyboardManager.Input;
        }
        if (appKeyActive)
        {
            appKeyTMP.text = keyboardManager.Input; 
        }
        if (tableNameActive)
        {
            tableNameTMP.text = keyboardManager.Input; 
        }
    }


    public void EnterAPIKey()
    {
        keyboardManager.Input = "";
        apiActive = true;
        appKeyActive = false;
        tableNameActive = false;
        Debug.LogError("Entering APIKey");
    }

    public void EnterAppKey()
    {
        keyboardManager.Input = "";
        apiActive = false;
        appKeyActive = true;
        tableNameActive = false;
        Debug.LogError("Entering APPKey");

    }

    public void EnterTableName()
    {
        keyboardManager.Input = "";
        apiActive = false;
        appKeyActive = false;
        tableNameActive = true;
        Debug.LogError("Entering TableName");

    }
}
