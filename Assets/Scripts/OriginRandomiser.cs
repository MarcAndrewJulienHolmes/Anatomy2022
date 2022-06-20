using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRandomiser : MonoBehaviour
{
    public GameObject[] origins;
    public GameObject[] looseObjects;
    private List<string> allOriginPointList = new List<string>();
    private List<int> originsUsed = new List<int>();
    private string originToSetString;
    public bool randomiseOrigins;


    private void Awake()
    {
        SetBonesPositions();
    }

    public void SetBonesPositions()
    {
        if (randomiseOrigins)
        {
            for (int i = 0; i < origins.Length; i++)
            {
                allOriginPointList.Add(origins[i].name);
            }

            for (int a = 0; a < origins.Length; a++)
            {
                GenerateRandomOrigin();
                if (originToSetString == null)
                {
                    a--;
                }
                else
                {
                    GameObject originToSet = GameObject.Find(originToSetString);
                    looseObjects[a].transform.position = originToSet.transform.position;
                    looseObjects[a].GetComponent<SelectedObject>().origin = originToSet.transform.position;
                    originToSetString = null;
                }
            }
        }
        else
        {
            for (int b = 0; b < looseObjects.Length; b++)
            {
                var looseBoneName = looseObjects[b].name;
                GameObject looseBoneOrigin = GameObject.Find(looseBoneName + " Origin");
                looseObjects[b].transform.position = looseBoneOrigin.transform.position;

            }
        }
    }

    public void ResetOriginList()
    {
        allOriginPointList.Clear();

        for (int i = 0; i < origins.Length; i++)
        {
            allOriginPointList.Add(origins[i].name);
        }
    }

    public void GenerateRandomOrigin()
    {
        int randomNum = Random.Range(0, origins.Length);
        if (originsUsed.Contains(randomNum))
        {
            GenerateRandomOrigin();
        }
        else
        {
            originsUsed.Add(randomNum);
            originToSetString = origins[randomNum].name;
        }
    }
}
