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

        // 1. if tank is secured

        // 2. if regulators are attached
            // if oxy regulator is attached

            // if ace regulator is attached

        // 3. if hoses are connected
            // if connected right hoses

            // if connected hoses to the torch

        // 4. if opened oxygen completely
        if(oxyTank.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<ValveGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }


        // 5. if adjusted oxy psi to 40
        if(oxyReg.transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        

        // 6. if opened acetalyne 1/2 turn
        if(aceTank.transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<ValveGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        }
        else{
            this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
        }

        // 7. if adjusted ace psi to 10
        if(aceReg.transform.GetChild(1).transform.GetChild(0).GetComponentInChildren<RegulatorGauge>().withinRange == true)
        {
            this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }

    }
}
