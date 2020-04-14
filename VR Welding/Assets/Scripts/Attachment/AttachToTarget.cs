using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToTarget : MonoBehaviour
{
    public Collider targetTrigger;
    public Transform toAnchor;
    public Transform container;
    public bool attached;

    void OnTriggerEnter(Collider other) {
        if (other == targetTrigger) {
            // Set rigidbody of parent object to kinematic
            toAnchor.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // Disable grab script on parent object
            toAnchor.gameObject.GetComponent<ManipulationHandler>().enabled = false;

            // Make current object the parent of what was the parent object
            /*
                Note: Unity does not allow you to directly set a child of an object as that object's parent
                Here I had to make the child and parent siblings before being able to "reverse their roles"
            */
            transform.SetParent(container);
            toAnchor.SetParent(transform);

            // Set transform of current object to match the transform of anchor point (both are empties)
            transform.position = targetTrigger.transform.position;
            transform.rotation = targetTrigger.transform.rotation;

            attached = true;
        }
    }
}
