using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapTrack : MonoBehaviour
{

    public GameObject cap;
    private float startX;
    private float startZ;
    private Quaternion startRotation;
    private bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        inTrigger = false;
        startRotation = cap.GetComponent<Transform>().transform.localRotation;
        startX = cap.GetComponent<Transform>().transform.localPosition.x;
        startZ = cap.GetComponent<Transform>().transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger)
        {
            keepInLine();
        }
    }

    void keepInLine()
    {
        cap.GetComponent<Transform>().transform.localPosition = new Vector3(startX, cap.GetComponent<Transform>().transform.localPosition.y, startZ);
        cap.GetComponent<Transform>().transform.localRotation = startRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CapTrack")
        {
            inTrigger = true;
        }
    }
    
    // void OnTriggerStay(Collider other)
    // {
    //     if(other.tag == "CapTrack")
    //     {
    //         inTrigger = true;
    //     }
    // }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "CapTrack")
        {
            inTrigger = false;
        }
    }

    
 }
