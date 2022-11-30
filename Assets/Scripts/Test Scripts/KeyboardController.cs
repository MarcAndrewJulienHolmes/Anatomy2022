using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRKeyboard.Utils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KeyboardController : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public SetEnvironment setEnvironment;
    public CreateRecord createRecord;
    public AnatomyKeyPrefs anatomyKeyPrefs;
    public OVRScreenFade ovrScreenFade;
    public SceneAndScoreManager sceneAndScoreManager;

    public bool apiActive, appKeyActive, tableNameActive, stringsFilled, clearPlayerPrefs, canvas1;

    public TMP_Text aPIKeyTMP;
    public TMP_Text appKeyTMP;
    public TMP_Text tableNameTMP;
    public TMP_Text airTableResponseTMP;

    public Button proceedToAppBTN;

    public Slider skeletonSceneSlider, muscleLearningSceneSlider, muscleTestingSceneSlider;
    public float skeletonSceneFloat, muscleLearningSceneFloat, muscleTestingSceneFloat;
    public TMP_Text skeletonSceneTMP, muscleLearningSceneTMP, muscleTestingSceneTMP;

    public Animator airTableAni, sliderBoardAni, keyboardAni;

    public GameObject airTableCanvas, sliderCanvas, keyboardObject;

    public bool setSlidersToPlayerPrefs = false;


    private void Awake()
    {
        anatomyKeyPrefs = GetComponent<AnatomyKeyPrefs>();
        anatomyKeyPrefs.RetrieveAppKeys();
        anatomyKeyPrefs.GetTimes();
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();


        if (anatomyKeyPrefs.aPIKeyStringPrevious == "" && anatomyKeyPrefs.appKeyStringPrevious == "" && anatomyKeyPrefs.tableNameStringPrevious == "")
        {

        }
        else
        {
            if(aPIKeyTMP.text.ToString() == "")
            {
                aPIKeyTMP.text = anatomyKeyPrefs.aPIKeyStringPrevious;
                appKeyTMP.text = anatomyKeyPrefs.appKeyStringPrevious;
                tableNameTMP.text = anatomyKeyPrefs.tableNameStringPrevious;

                setEnvironment.ApiKey = anatomyKeyPrefs.aPIKeyStringPrevious;
                setEnvironment.AppKey = anatomyKeyPrefs.appKeyStringPrevious;
                createRecord.TableName = anatomyKeyPrefs.tableNameStringPrevious;
            }
        }
        proceedToAppBTN.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas1)
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

            if (AirtableUnity.PX.Proxy.connectionSuccess)
            {
                proceedToAppBTN.interactable = true;
            }

            if (clearPlayerPrefs)
            {
                PlayerPrefs.DeleteAll();
                Debug.LogError("Player prefs cleared");
                clearPlayerPrefs = false;
            }
        }
        else
        {
            GetSliderValues();

        }
    }

    public IEnumerator CanvasSwitch()
    {
        airTableAni.Play("AirTableInfoFadeOut");
        keyboardAni.Play("KeyboardFadeOut");
        yield return new WaitForSeconds(0.5f);
        sliderCanvas.SetActive(true);
        canvas1 = false;
        airTableCanvas.SetActive(false);
        keyboardObject.SetActive(false);
    }

    public void GetSliderValues()
    {
        if(anatomyKeyPrefs.skeletalSceneTimePrevious < 1)
        {
            skeletonSceneFloat = skeletonSceneSlider.value;
            muscleLearningSceneFloat = muscleLearningSceneSlider.value;
            muscleTestingSceneFloat = muscleTestingSceneSlider.value;

            skeletonSceneTMP.text = "Skeletal Quiz Time: " + skeletonSceneFloat + " minutes";
            muscleLearningSceneTMP.text = "Muscle Learning Time: " + muscleLearningSceneFloat + " minutes";
            muscleTestingSceneTMP.text = "Muscle Testing Time: " + muscleTestingSceneFloat + " minutes";
        }
        else
        {
            if (!setSlidersToPlayerPrefs)
            {
                skeletonSceneSlider.value = anatomyKeyPrefs.skeletalSceneTimePrevious;
                muscleLearningSceneSlider.value = anatomyKeyPrefs.muscleLearningSceneTimePrevious;
                muscleTestingSceneSlider.value = anatomyKeyPrefs.muscleTestingSceneTimePrevious;
                setSlidersToPlayerPrefs = true;
            }

            skeletonSceneFloat = skeletonSceneSlider.value;
            muscleLearningSceneFloat = muscleLearningSceneSlider.value;
            muscleTestingSceneFloat = muscleTestingSceneSlider.value;

            skeletonSceneTMP.text = "Skeletal Quiz Time: " + skeletonSceneFloat + " minutes";
            muscleLearningSceneTMP.text = "Muscle Learning Time: " + muscleLearningSceneFloat + " minutes";
            muscleTestingSceneTMP.text = "Muscle Testing Time: " + muscleTestingSceneFloat + " minutes";
        }
    }



    public void EnterAPIKey()
    {
        keyboardManager.Input = aPIKeyTMP.text.ToString();
        apiActive = true;
        appKeyActive = false;
        tableNameActive = false;
        //Debug.LogError("Entering APIKey");
    }

    public void EnterAppKey()
    {
        keyboardManager.Input = appKeyTMP.text.ToString();
        apiActive = false;
        appKeyActive = true;
        tableNameActive = false;
        //Debug.LogError("Entering APPKey");
    }

    public void EnterTableName()
    {
        keyboardManager.Input = tableNameTMP.text.ToString();
        createRecord.TableName = keyboardManager.Input;
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
        StartCoroutine(ChangeScene());
    }

    public void ProceedToAppSettings()
    {
        StartCoroutine(CanvasSwitch());
    }

    public IEnumerator ChangeScene()
    {
        sceneAndScoreManager.boneSceneMaxTime = skeletonSceneFloat;
        sceneAndScoreManager.muscleLearningMaxTime = muscleLearningSceneFloat;
        sceneAndScoreManager.muscleTestingMaxTime = muscleTestingSceneFloat;

        anatomyKeyPrefs.skeletalSceneTime = skeletonSceneFloat;
        anatomyKeyPrefs.muscleLearningSceneTime = muscleLearningSceneFloat;
        anatomyKeyPrefs.muscleTestingSceneTime = muscleTestingSceneFloat;

        anatomyKeyPrefs.SetTimes();

        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SportScienceSkeletal_EnglishVersion");
    }
}
