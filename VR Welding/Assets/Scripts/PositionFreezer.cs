using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFreezer : MonoBehaviour
{
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.gameObject.GetComponent<Transform>().transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Transform>().transform.localPosition = initialPos;
    }
}
