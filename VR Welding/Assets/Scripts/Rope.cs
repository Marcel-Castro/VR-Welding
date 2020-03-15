using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    Transform ropeStart;
    Transform previousSegment;

    // RigidBody Properties
    public float rbMass;

    // Collider size
    public float collSize;

    void Awake() {
        ropeStart = gameObject.transform.Find("Hose_Armature").transform.Find("end_1");
    }

    // Start is called before the first frame update
    void Start() {
        addPhysicsToBones(ropeStart);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void addPhysicsToBones(Transform root) {
        if (root.childCount > 0) {
            // Add collider
            root.gameObject.AddComponent<BoxCollider>();
            BoxCollider collider = root.gameObject.GetComponent<BoxCollider>();
            collider.size = new Vector3(collSize, collSize, collSize);

            // Add collision layer (for removing collisions between rope segments)
            root.gameObject.layer = 9;

            // Add rigidbody
            root.gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = root.gameObject.GetComponent<Rigidbody>();
            rb.mass = rbMass;
            

            if (root.name != "end_1") {
                root.gameObject.AddComponent<CharacterJoint>();
                CharacterJoint joint = root.gameObject.GetComponent<CharacterJoint>();
                joint.enablePreprocessing = false;
                joint.enableCollision = false;

                joint.connectedBody = previousSegment.gameObject.GetComponent<Rigidbody>();
            } else {
                // Set top bone as kinematic (for testing purposes)
                // rb.isKinematic = true;
            }

            // Create reference to parent for next child
            previousSegment = root;

            // Recursive Call
            addPhysicsToBones(root.GetChild(0));
        } else {
            // Final bone
            // Grab point can be added here
        }
    }
}
