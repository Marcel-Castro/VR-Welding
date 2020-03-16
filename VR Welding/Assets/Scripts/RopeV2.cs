using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeV2 : MonoBehaviour
{
    // Parent containing all rope segments
    Transform ropeParent;
    // Rope segments needed for stabilizing the "bottom" of the rope
    Transform secondLast;
    Transform last;

    // RigidBody Properties
    public float rbMass = 1;

    // Collider size
    public float collSize = .005f;

    void Awake() {
        ropeParent = gameObject.transform.Find("Hose_Armature");
    }

    // Start is called before the first frame update
    void Start()
    {
        addPhysicsToBones(ropeParent);
        secondLast = ropeParent.Find("2ndLast");
        last = ropeParent.Find("end_2");
    }

    // Update is called once per frame
    void Update()
    {
        // Second to last segment should match the transform of the last segment when both are set to kinematic
        if (last.gameObject.GetComponent<Rigidbody>().isKinematic == true) {
            secondLast.SetParent(last);
        } else {
            secondLast.SetParent(ropeParent);
        }

        // The second to last segment should always have the same "kinematic value" as the last segment
        secondLast.gameObject.GetComponent<Rigidbody>().isKinematic = last.gameObject.GetComponent<Rigidbody>().isKinematic;
    }

    void addGrabPoint(GameObject current) {
        ManipulationHandler scriptRef = current.AddComponent<ManipulationHandler>() as ManipulationHandler;
        scriptRef.ManipulationType = ManipulationHandler.HandMovementType.OneHandedOnly;
        scriptRef.SmoothingAmoutOneHandManip = (1e-06f);

        current.gameObject.AddComponent<NearInteractionGrabbable>();
    }

    void addPhysicsToBones(Transform root) {
        for (int i = 0; i < root.childCount; i++) {
            GameObject current = root.GetChild(i).gameObject;
            // Add rigidbody
            Rigidbody rb = current.AddComponent<Rigidbody>() as Rigidbody;
            rb.mass = rbMass;

            // Add collider
            BoxCollider collider = current.AddComponent<BoxCollider>() as BoxCollider;
            collider.size = new Vector3(collSize, collSize, collSize);

            // Add collision layer (for removing collisions between rope segments)
            current.layer = 9;

            // Add grab points to first and last segments
            if (i == 0 || i == root.childCount - 1) {
                addGrabPoint(current);
            }

            // Add and connect character joints for all segments EXCEPT the first
            if (i != 0) {
                CharacterJoint joint = current.AddComponent<CharacterJoint>() as CharacterJoint;
                joint.enablePreprocessing = false;
                joint.enableCollision = false;
                // joint.twistLimitSpring.damper = .02f; TODO ----------

                joint.connectedBody = root.GetChild(i - 1).gameObject.GetComponent<Rigidbody>();
            }
        }
    }
}
