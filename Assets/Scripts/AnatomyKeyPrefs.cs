using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnatomyKeyPrefs : MonoBehaviour
{

    public bool setKeysBool, getKeysBool;    

    public string aPIKeyString;
    public string appKeyString;
    public string tableNameString;

    public string aPIKeyStringPrevious;
    public string appKeyStringPrevious;
    public string tableNameStringPrevious;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setKeysBool)
        {
            SetAppKeys();
            setKeysBool = false;
        }

        if (getKeysBool)
        {
            RetrieveAppKeys();
            getKeysBool = false;
        }
    }


    public void SetAppKeys()
    {
        SetString("API Key", aPIKeyString);
        SetString("App Key", appKeyString);
        SetString("Table Name", tableNameString);
    }

    public void RetrieveAppKeys()
    {
        aPIKeyStringPrevious = GetString("API Key");
        appKeyStringPrevious = GetString("App Key");
        tableNameStringPrevious = GetString("Table Name");
    }

    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
}
