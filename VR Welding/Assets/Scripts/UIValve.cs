using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValve : MonoBehaviour
{
    public GameObject needle;
    public GameObject valve;
    bool findAngle = false;
    bool haveOldPos = false;

    public bool haveStopRotate = false;

    public Vector3 stopRotate;
    Quaternion lastPosition;
    float totalAngle;

    // Start is called before the first frame update
    void Start()
    {
        totalAngle = 0;
        lastPosition = valve.gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(totalAngle > 360 )
        {
            (this.gameObject.GetComponent("ManipulationHandler") as MonoBehaviour).enabled = false;
            (this.gameObject.GetComponent("NearInteractionGrabbable") as MonoBehaviour).enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            
            if(!haveStopRotate)
            {
                stopRotate = this.gameObject.GetComponent<Transform>().transform.eulerAngles;

                stopRotate = new Vector3(stopRotate.x, stopRotate.y - 10f, stopRotate.z);

                haveStopRotate = true;
            }
            else {
                this.gameObject.GetComponent<Transform>().transform.eulerAngles = stopRotate;
                haveStopRotate = false;
            }
            
            //RotationFreezer(true, stopRotate);
        }
        else{
            (this.gameObject.GetComponent("ManipulationHandler") as MonoBehaviour).enabled = true;
            (this.gameObject.GetComponent("NearInteractionGrabbable") as MonoBehaviour).enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            
        }
   
        
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

        //adjust gauge
        needle.gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(needle.gameObject.GetComponent<Transform>().localEulerAngles.x, 
                                                                                     needle.gameObject.GetComponent<Transform>().localEulerAngles.y, 
                                                                                     totalAngle * -0.125f);
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

    void RotationFreezer(bool dir, Vector3 rotation)
    {

        rotation = new Vector3(rotation.x, rotation.y - 2f, rotation.z);

        this.gameObject.GetComponent<Transform>().transform.eulerAngles = rotation;

    }
    
    
    
}
