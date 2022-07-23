using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleToFemaleColliderChecker : MonoBehaviour
{
    public OrientatedObjectAttacher orientatedObjectAttacher;

    public string thisColliderName;
    public string receivingCollider;
    public Collider thisCollider;

    public bool male, female;

    private void Awake()
    {
        if (male)
        {
            thisCollider = GetComponent<Collider>();
            thisColliderName = transform.parent.name + transform.name;
            receivingCollider = thisColliderName + "Receiver";
            orientatedObjectAttacher = transform.parent.GetComponent<OrientatedObjectAttacher>();
        }

        if (female)
        {
            thisCollider = GetComponent<Collider>();
            thisColliderName = transform.parent.name + transform.name + "Receiver";
            transform.name = thisColliderName;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (male)
        {
            if (other.name == receivingCollider)
            {
                orientatedObjectAttacher.currentEnteredCollidersInt++;
                orientatedObjectAttacher.CheckColliders();
                //Debug.LogError(thisColliderName + " collision confirmed");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (male)
        {
            if (other.name == receivingCollider)
            {
                orientatedObjectAttacher.currentEnteredCollidersInt--;
                //Debug.LogError(thisColliderName + " collision exited");
            }
        }
    }

}
