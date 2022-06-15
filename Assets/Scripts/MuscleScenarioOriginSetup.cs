using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleScenarioOriginSetup : MonoBehaviour
{
    public GameObject[] muscleGroup;

    public List<string> muscleGroupList = new List<string>();

    public List<int> usedMuscleGroupsIntList = new List<int>();

    public List<string> usedMuscleGroupsStringList = new List<string>();

    public GameObject[] muscleOrigin;

    public int muscleOriginCounter;


    private void Awake()
    {
        muscleOriginCounter = 0;
        GatherAllMuscleGroups();
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplyRandomMuscleGroupToOrigin();
    }



    public void GatherAllMuscleGroups()
    {
        for (int i = 0; i < muscleGroup.Length; i++)
        {
            muscleGroupList.Add(muscleGroup[i].name);
            muscleGroup[i].SetActive(false);
        }
    }

    public void ApplyRandomMuscleGroupToOrigin()
    {
        int randomNum = Random.Range(0, muscleGroup.Length);
        if (usedMuscleGroupsIntList.Contains(randomNum))
        {
            ApplyRandomMuscleGroupToOrigin();
        }
        else
        {
            if(muscleOriginCounter == muscleOrigin.Length)
            {
                return;
            }
            else
            {
                usedMuscleGroupsIntList.Add(randomNum);
                usedMuscleGroupsStringList.Add(muscleGroup[randomNum].name);
                muscleGroup[randomNum].SetActive(true);
                muscleGroup[randomNum].transform.position = muscleOrigin[muscleOriginCounter].transform.position;
                muscleOriginCounter++;
                ApplyRandomMuscleGroupToOrigin();
            }           
        }
    }
}
