using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    public GameObject chain;

    public GameObject oxyAnchor;

    public GameObject oxyHoseAnchor1;
    public GameObject aceHoseAnchor1;

    public GameObject oxyHoseAnchor2;
    public GameObject aceHoseAnchor2;
    public GameObject aceAnchor;
    public GameObject oxyValve;
    public GameObject aceValve;
    public GameObject oxyRegKnob;
    public GameObject aceRegKnob;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 1. if tank is secured
        if(chain.GetComponentInChildren<AttachToTarget>().attached == true)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        // 2. if regulators are attached
        if(oxyAnchor.GetComponentInChildren<AttachToTarget>().attached == true && aceAnchor.GetComponentInChildren<AttachToTarget>().attached == true)
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
           

        // 3. if hoses are connected to both the regulators and the torch
        if(oxyHoseAnchor1.GetComponentInChildren<AttachToTarget>().attached == true && aceHoseAnchor1.GetComponentInChildren<AttachToTarget>().attached == true)
        {
            if(oxyHoseAnchor2.GetComponentInChildren<AttachToTarget>().attached == true && aceHoseAnchor2.GetComponentInChildren<AttachToTarget>().attached == true)
            {
                this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            
        }

        // 4. if opened oxygen completely
        if(oxyValve.GetComponentInChildren<ValveGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }


        // 5. if adjusted oxy psi to 40
        if(oxyRegKnob.GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        

        // 6. if opened acetalyne 1/2 turn
        if(aceValve.GetComponentInChildren<ValveGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }

        // 7. if adjusted ace psi to 10
        if(aceRegKnob.GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(6).gameObject.SetActive(false);
        }

    }
}
