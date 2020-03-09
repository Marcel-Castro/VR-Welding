using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBoxScript : MonoBehaviour
{
    public GameObject collidee; // the colliding object
    private Vector3 posOnCollide; // position on collision
    private Quaternion rotateOnCollide; // rotation on collision
    private bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(collidee.GetComponent<Rigidbody>().isKinematic == true && inTrigger)
        {
            keepInLine();
        }
    }

    void keepInLine()
    {
        if(collidee.GetComponent<Transform>().transform.position.y < posOnCollide.y)
        collidee.GetComponent<Transform>().transform.position = new Vector3(collidee.GetComponent<Transform>().transform.position.x, 
                                                                            posOnCollide.y, 
                                                                            collidee.GetComponent<Transform>().transform.position.z);
        collidee.GetComponent<Transform>().transform.rotation = rotateOnCollide;
    }

    void OnTriggerEnter(Collider other)
    {
        collidee = other.gameObject;
        rotateOnCollide = collidee.GetComponent<Transform>().transform.rotation;
        posOnCollide = collidee.GetComponent<Transform>().transform.position;
        inTrigger = true;
    }
    
    // void OnTriggerStay(Collider other)
    // {
    //     posOnCollide = new Vector3(posOnCollide.x, posOnCollide.y + 0.01f, posOnCollide.z);
    // }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


}
