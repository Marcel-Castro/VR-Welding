using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class RegulatorGauge : MonoBehaviour
{
    public GameObject UINeedle;
    public GameObject needle;
    public GameObject valve;
    bool haveStopRotate;
    Vector3 stopRotate;
    Quaternion lastPosition;
    public float totalAngle;
    public float stopDegreeLeft;
    public float stopDegreeRight;
    public float turnRatio;
    public bool matchDirection;

    public float acceptableDegreeStart;

    public float acceptableDegreeEnd;

    public bool withinRange;

    

    // Start is called before the first frame update
    void Start()
    {
        haveStopRotate = false;
        totalAngle = 0;
        lastPosition = valve.gameObject.transform.localRotation;

        if(matchDirection)
        {
            turnRatio *= -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(totalAngle > stopDegreeRight || totalAngle < stopDegreeLeft) //if the object turns too far in either direction
        {
            valve.gameObject.GetComponent<ManipulationHandler>().ForceEndManipulation(); //stop manipulation
            
            if(!haveStopRotate) //update when it should stop
            {
                UpdateStopRotate();
                haveStopRotate = true;
            }
            else{ //forcefully rotate it back to that stop position
                valve.gameObject.GetComponent<Transform>().transform.localEulerAngles = stopRotate;
                haveStopRotate = false;
            }
        }
        

        //keep track of rotation angle
        totalAngle += UpdateTotalAngle();

        //adjust gauges
        UINeedle.gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(UINeedle.gameObject.GetComponent<Transform>().localEulerAngles.x, 
                                                                                     UINeedle.gameObject.GetComponent<Transform>().localEulerAngles.y, 
                                                                                     totalAngle * turnRatio);

        needle.gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(needle.gameObject.GetComponent<Transform>().localEulerAngles.x, 
                                                                                    needle.gameObject.GetComponent<Transform>().localEulerAngles.y, 
                                                                                    totalAngle * -turnRatio);
    
        //change if needle is within acceptable state
        if(totalAngle > acceptableDegreeStart && totalAngle < acceptableDegreeEnd)
        {
            withinRange = true;
        }
        else{
            withinRange = false;
        }
    
    }

    //Update at what angle the rotation should stop
    void UpdateStopRotate()
    {
        stopRotate = valve.gameObject.GetComponent<Transform>().transform.localEulerAngles;

        if(totalAngle > 360) {
            stopRotate = new Vector3(stopRotate.x, stopRotate.y - 5f, stopRotate.z );
        }
        else if(totalAngle < -5) {
            stopRotate = new Vector3(stopRotate.x, stopRotate.y + 5f, stopRotate.z );
        }
    }
    
    //Returns how many degrees the object has turned between two positions
    float UpdateTotalAngle()
    {
        float newAngle = Quaternion.Angle(lastPosition, valve.gameObject.transform.localRotation);

        if(!GetRotateDirection(lastPosition, valve.gameObject.transform.localRotation)) {
            newAngle *= -1;
        }
        
        lastPosition = valve.gameObject.transform.localRotation;
        return newAngle;
    }

    //Determines whether the object is turning clockwise or counter-clockwise
    bool GetRotateDirection(Quaternion from, Quaternion to)
    {
        float fromX = from.eulerAngles.y;
        float toX  = to.eulerAngles.y;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromX <= toX)
        {
            clockWise = toX-fromX;
            counterClockWise = fromX + (360-toX);
        }
        else
        {
            clockWise = (360-fromX) + toX;
            counterClockWise = fromX-toX;
        }
        return (clockWise <= counterClockWise);
    }

    
    
    
    
}
