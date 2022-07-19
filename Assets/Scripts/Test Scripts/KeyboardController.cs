using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRKeyboard.Utils;


public class KeyboardController : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public SetEnvironment setEnvironment;
    public CreateRecord createRecord;
    public AnatomyKeyPrefs anatomyKeyPrefs;

    public bool apiActive, appKeyActive, tableNameActive;

    public TMP_Text aPIKeyTMP;
    public TMP_Text appKeyTMP;
    public TMP_Text tableNameTMP;
    public TMP_Text airTableResponseTMP;

    private void Awake()
    {
        anatomyKeyPrefs = GetComponent<AnatomyKeyPrefs>();
        anatomyKeyPrefs.RetrieveAppKeys();
        if(anatomyKeyPrefs.aPIKeyStringPrevious == "" && anatomyKeyPrefs.appKeyStringPrevious == "" && anatomyKeyPrefs.tableNameStringPrevious == "")
        {

        }
        else
        {
            aPIKeyTMP.text = anatomyKeyPrefs.aPIKeyStringPrevious;
            appKeyTMP.text = anatomyKeyPrefs.appKeyStringPrevious;
            tableNameTMP.text = anatomyKeyPrefs.tableNameStringPrevious;

            setEnvironment.ApiKey = anatomyKeyPrefs.aPIKeyStringPrevious;
            setEnvironment.AppKey = anatomyKeyPrefs.appKeyStringPrevious;
            createRecord.TableName = anatomyKeyPrefs.tableNameStringPrevious;
        }
    }

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
            setEnvironment.ApiKey = keyboardManager.Input;
            anatomyKeyPrefs.aPIKeyString = keyboardManager.Input;
        }
        if (appKeyActive)
        {
            appKeyTMP.text = keyboardManager.Input;
            setEnvironment.AppKey = keyboardManager.Input;
            anatomyKeyPrefs.appKeyString = keyboardManager.Input;
        }
        if (tableNameActive)
        {
            tableNameTMP.text = keyboardManager.Input;
            createRecord.TableName = keyboardManager.Input;
            anatomyKeyPrefs.tableNameString = keyboardManager.Input;
        }

        airTableResponseTMP.text = AirtableUnity.PX.Proxy.responseMessage;
    }


    public void EnterAPIKey()
    {
        keyboardManager.Input = "";
        apiActive = true;
        appKeyActive = false;
        tableNameActive = false;
        //Debug.LogError("Entering APIKey");
    }

    public void EnterAppKey()
    {
        keyboardManager.Input = "";
        apiActive = false;
        appKeyActive = true;
        tableNameActive = false;
        //Debug.LogError("Entering APPKey");
    }

    public void EnterTableName()
    {
        keyboardManager.Input = "";
        apiActive = false;
        appKeyActive = false;
        tableNameActive = true;
        //Debug.LogError("Entering TableName");
    }

    public void ProceedToApp()
    {
        anatomyKeyPrefs.aPIKeyString = setEnvironment.ApiKey;
        anatomyKeyPrefs.appKeyString = setEnvironment.AppKey;
        anatomyKeyPrefs.tableNameString = createRecord.TableName;
        anatomyKeyPrefs.SetAppKeys();
    }
}