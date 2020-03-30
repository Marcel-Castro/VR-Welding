using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCapAnchor : MonoBehaviour
{
    public Collider targetTrigger;
    public Collider anchor;

    // Start is called before the first frame update
    void Start() {
        
    }


    // Update is called once per frame
    void Update() {
        
    }


    // Disable anchor if cap is still on tank
    private void OnTriggerEnter(Collider other) {
        if (other == targetTrigger) {
            anchor.enabled = false;
        }
    }


    // Enable anchor if cap is removed from tank
    private void OnTriggerExit(Collider other) {
        if (other == targetTrigger) {
            anchor.enabled = true;
        }
    }
}
