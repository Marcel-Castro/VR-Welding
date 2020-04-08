using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Very similar to SetCapAnchor but the action performed on each trigger event is the oppossite
public class SetConnectedAnchor : MonoBehaviour
{
    public Collider targetTrigger;
    public Collider anchor;

    // Start is called before the first frame update
    void Start()
    {
        // Disable anchor by default
        anchor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enable anchor if the current collider is making contact with target trigger
    private void OnTriggerEnter(Collider other) {
        if (other == targetTrigger) {
            anchor.enabled = true;
        }
    }


    // Disable anchor if the current collider is making contact with target trigger
    private void OnTriggerExit(Collider other) {
        if (other == targetTrigger) {
            anchor.enabled = false;
        }
    }
}
