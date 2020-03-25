using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValve : MonoBehaviour
{
    public GameObject needle;
    public GameObject valve;
    bool findAngle = false;
    bool haveOldPos = false;
    Quaternion lastPosition;
    float totalAngle;

    // Start is called before the first frame update
    void Start()
    {
        totalAngle = 0;
        lastPosition = valve.gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
   
        //measure difference

        float newAngle = Quaternion.Angle(lastPosition, valve.gameObject.transform.localRotation);

        if(GetRotateDirection(lastPosition, valve.gameObject.transform.localRotation)) {
            totalAngle += (newAngle);
        }
        else {
            totalAngle -= (newAngle);
        }
        
        lastPosition = valve.gameObject.transform.localRotation;
            
        
        Debug.Log(totalAngle);

        needle.gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(needle.gameObject.GetComponent<Transform>().localEulerAngles.x, 
                                                                                     needle.gameObject.GetComponent<Transform>().localEulerAngles.y, 
                                                                                     totalAngle * 0.125f);
    }



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
