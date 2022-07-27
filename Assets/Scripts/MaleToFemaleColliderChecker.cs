using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleToFemaleColliderChecker : MonoBehaviour
{
    public OrientatedObjectAttacher orientatedObjectAttacher;

    public GameObject player;

    public string thisColliderName;
    public string receivingCollider;
    public Collider thisCollider;

    public bool male, female;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (male)
        {
            thisCollider = GetComponent<Collider>();
            thisColliderName = transform.parent.name + transform.name;
            receivingCollider = transform.parent.name + " Female" + transform.name + "Receiver";
            orientatedObjectAttacher = transform.parent.GetComponent<OrientatedObjectAttacher>();
        }

        if (female)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
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
