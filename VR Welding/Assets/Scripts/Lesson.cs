using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    public GameObject oxyTank;
    public GameObject aceTank;
    public GameObject oxyReg;
    public GameObject aceReg;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // if tank is secured

        // if oxy regulator is attached

        // if ace regulator is attached

        // if connected right hoses

        // if connected hoses to the torch

        // if opened oxygen completely

        // if adjusted oxy psi to 40
        if(oxyReg.transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            print("oxy psi good");
        }
        

        // if opened acetalyne 1/2 turn

        // if adjusted ace psi to 10
        if(aceReg.transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            print("ace psi good");
        }

        // OPTIONAL if adjusted both psi's to torch setting
    }
}
