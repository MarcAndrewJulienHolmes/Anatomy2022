using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectNameTag : MonoBehaviour
{
    public string parentName;

    public TMP_Text nameText;

    public SelectedObject selectedObject;

    public GameObject thisGameObject;

    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        parentName = transform.parent.name;

        nameText = GetComponentInChildren<TMP_Text>();

        nameText.text = parentName;

        selectedObject = GetComponentInParent<SelectedObject>();

        thisGameObject = transform.gameObject;

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        //transform.LookAt(playerPosition);
    }

    private void Update()
    {

    }

    public void EnableNameTag()
    {
        thisGameObject.SetActive(true);
    }

    public void DisableNameTag()
    {
        thisGameObject.SetActive(false);
    }
}
