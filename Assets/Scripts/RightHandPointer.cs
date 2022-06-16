using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class RightHandPointer : MonoBehaviour
{
    public OnboardingManager onboardingManager;

    public OVRScreenFade ovrScreenFade;

    public LineRenderer lineRenderer;

    public BoneNameQuiz boneNameQuiz;

    public QuizButton[] quizButton;

    public LanguageButton[] languageButton;

    public Material normal, highlighted;

    private float flexibleLineLength;
        
    public float countdownTimer;

    public int layerMask = 1;

    public Vector3 endPosition;

    public bool objectHitRight = false;

    public bool holdingObject, coroutineRunning;

    public bool linePointerOn;

    public GameObject pointObject, currenHighlightedObject;

    public string currentHighlightedObjectName;


    public UnityEvent rightHandSelect;
    public UnityEvent rightHandMoveTowards;
    public UnityEvent rightHandDeselect;
    public UnityEvent rightHandReturnOrigin;

    

    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = 3f;
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        lineRenderer.SetPositions(startLinePositions);
        lineRenderer.material = normal;
        linePointerOn = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            rightHandDeselect.Invoke();
        }


        if (OVRInput.Get(OVRInput.Button.One))
        {
            rightHandReturnOrigin.Invoke();
            rightHandDeselect.Invoke();

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            if (!onboardingManager.leftThumbstickMove)
            {
                onboardingManager.leftThumbstickMove = true;
                onboardingManager.UpdateChecklist();
            }

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            if (!onboardingManager.leftThumbstickMove)
            {
                onboardingManager.leftThumbstickMove = true;
                onboardingManager.UpdateChecklist();
            }

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            if (!onboardingManager.leftThumbstickMove)
            {
                onboardingManager.leftThumbstickMove = true;
                onboardingManager.UpdateChecklist();
            }
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            if (!onboardingManager.leftThumbstickMove)
            {
                onboardingManager.leftThumbstickMove = true;
                onboardingManager.UpdateChecklist();
            }
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            if (!onboardingManager.rightThumbstickTurn)
            {
                onboardingManager.rightThumbstickTurn = true;
                onboardingManager.UpdateChecklist();
            }

        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))             
        {
            if (!onboardingManager.rightThumbstickTurn)
            {
                onboardingManager.rightThumbstickTurn = true;
                onboardingManager.UpdateChecklist();
            }
        }

        //if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        //{
        //    rightHandMoveTowards.Invoke();

        //}

        if (OVRInput.Get(OVRInput.Button.Start))
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0 && !coroutineRunning)
            {
                StartCoroutine(RestartApp());                
            }
        }
        else
        {
            countdownTimer = 3f;
        }

        if (linePointerOn)
        {
            lineRenderer.enabled = true;
            ActiveLineRenderer(transform.position, transform.forward, flexibleLineLength);
        }
        else
        {
            lineRenderer.enabled = false;

        }
    }

 

    public void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
    {
        RaycastHit hit;

        Ray lineRendererOut = new Ray(targetPosition, direction);

        endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(lineRendererOut, out hit))
        {
            endPosition = hit.point;

            pointObject = hit.collider.gameObject;

            if (pointObject.GetComponent<SelectedObject>())
            {
                if (!boneNameQuiz.quizAvailable)
                {
                    currentHighlightedObjectName = pointObject.GetComponent<SelectedObject>().thisGameObjectName;
                    currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                    lineRenderer.material = highlighted;

                    if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        if (!holdingObject)
                        {
                            currenHighlightedObject.GetComponent<SelectedObject>().rightHandRay = true;
                            currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                            holdingObject = true;
                            rightHandMoveTowards.Invoke();

                        }
                    }
                }
            }

            else if (pointObject.GetComponent<QuizButton>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<QuizButton>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                lineRenderer.material = highlighted;

                for (int i = 0; i < quizButton.Length; i++)
                {
                    if (quizButton[i].name == currentHighlightedObjectName)
                    {
                        quizButton[i].rightHandRay = true;
                    }
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    pointObject.GetComponent<QuizButton>().ButtonSelect();
                }
            }


            else if (pointObject.GetComponent<LanguageButton>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<LanguageButton>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                lineRenderer.material = highlighted;

                for (int i = 0; i < languageButton.Length; i++)
                {
                    if (languageButton[i].name == currentHighlightedObjectName)
                    {
                        languageButton[i].rightHandRay = true;
                    }
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    pointObject.GetComponent<LanguageButton>().ButtonSelect();
                }
            }

            else if (!pointObject.GetComponent<QuizButton>() && !pointObject.GetComponent<SelectedObject>() && !pointObject.GetComponent<LanguageButton>())
            {
                for (int i = 0; i < quizButton.Length; i++)
                {
                    quizButton[i].rightHandRay = false;
                }

                currentHighlightedObjectName = null;

                lineRenderer.material = normal;
            }
        }


        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public IEnumerator RestartApp()
    {
        coroutineRunning = true;
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Urdd_LanguageSelect");
    }
}
