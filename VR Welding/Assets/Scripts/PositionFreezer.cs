using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFreezer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(0,0,0);
    }
}
