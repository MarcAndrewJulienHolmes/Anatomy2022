using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRandomiser : MonoBehaviour
{
    public GameObject[] origins;

    public GameObject[] looseBones;

    public List<string> allOriginPointList = new List<string>();

    public List<int> originsUsed = new List<int>();

    public string originToSetString;

    public bool randomiseOrigins;

    private void Awake()
    {
        
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
                    looseBones[a].transform.position = originToSet.transform.position;
                    looseBones[a].GetComponent<SelectedObject>().origin = originToSet.transform.position;
                    originToSetString = null;
                }
            }
        }
        else
        {
            for (int b = 0; b < looseBones.Length; b++)
            {
                var looseBoneName = looseBones[b].name;
                GameObject looseBoneOrigin = GameObject.Find(looseBoneName + " Origin");
                looseBones[b].transform.position = looseBoneOrigin.transform.position;

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
