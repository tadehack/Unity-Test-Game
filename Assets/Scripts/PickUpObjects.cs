using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    //Move this to Player script later and add a variable for the pickup range instead of changing it here

    RaycastHit pickUpObjectRange;
    GameObject grabbedObj;
    public Transform grabPos;

    void Update()
    {
        PickUpObject();
    }

    private void PickUpObject()
    {
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(transform.position, transform.forward, out pickUpObjectRange, 3.70f) && pickUpObjectRange.transform.GetComponent<Rigidbody>())
        {
            grabbedObj = pickUpObjectRange.transform.gameObject;
            //grabbedObj.GetComponent<Rigidbody>().freezeRotation = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            //grabbedObj.GetComponent<Rigidbody>().freezeRotation = false;
            grabbedObj.GetComponent<Rigidbody>().velocity = 0 * grabbedObj.transform.position;
            grabbedObj = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //grabbedObj.GetComponent<Rigidbody>().freezeRotation = false;
            grabbedObj.GetComponent<Rigidbody>().velocity = 0 * grabbedObj.transform.position;
            grabbedObj.GetComponent<Rigidbody>().AddForce(transform.forward * 700);
            grabbedObj = null;
        }

        if (grabbedObj)
        {
            grabbedObj.GetComponent<Rigidbody>().velocity = 15 * (grabPos.position - grabbedObj.transform.position);
        }
    }
}
