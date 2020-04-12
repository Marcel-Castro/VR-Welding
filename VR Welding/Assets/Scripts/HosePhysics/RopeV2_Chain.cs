using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeV2_Chain : MonoBehaviour
{
    // Parent containing all rope segments
    public Transform ropeParent;
    // Rope segments needed for stabilizing the "bottom" of the rope
    Transform secondLast;
    Transform last;
    // Rope segment needed for creating tether between first and last segment
    Transform first;

    // Max distance between first and last segment of the rope
    float maxDist;
    public float maxDistThreshold; // Percentage
    public float tetherSpeed;

    // RigidBody Properties
    public float rbMass = 1;

    // Collider size
    public float collSize = .005f;
    

    void Awake() {
        first = ropeParent.Find("end_1");
        secondLast = ropeParent.Find("2ndLast");
        last = ropeParent.Find("end_2");
    }


    // Start is called before the first frame update
    void Start() {
        addPhysicsToBones(ropeParent);

        // Set first end of the chain to kinematic by default
        first.GetComponent<Rigidbody>().isKinematic = true;

        // Create tether between first and last segment to prevent streching of the rope, here we get the distance between the two
        maxDist = Vector3.Distance(first.position, last.position);
    }


    // Update is called once per frame
    void Update() {
        // Second to last segment should match the transform of the last segment when both are set to kinematic
        if (last.gameObject.GetComponent<Rigidbody>().isKinematic == true) {
            secondLast.SetParent(last);
        } else {
            secondLast.SetParent(ropeParent);
        }

        // The second to last segment should always have the same "kinematic value" as the last segment
        secondLast.gameObject.GetComponent<Rigidbody>().isKinematic = last.gameObject.GetComponent<Rigidbody>().isKinematic;

        // Enforce tether when both ends of the rope are "grabbed" (both are kinematic)
        float step =  tetherSpeed * Time.deltaTime;

        if (last.gameObject.GetComponent<Rigidbody>().isKinematic == true && first.gameObject.GetComponent<Rigidbody>().isKinematic) {
            if (Vector3.Distance(first.position, last.position) >= maxDist - (maxDist * maxDistThreshold)) {
                last.position = Vector3.MoveTowards(last.position, first.position, step);
            }
        }
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

            // Add grab point to last segment
            if (i == root.childCount - 1) {
                addGrabPoint(current);
            }

            // Add and connect character joints for all segments EXCEPT the first
            if (i != 0) {
                ConfigurableJoint joint = current.AddComponent<ConfigurableJoint>() as ConfigurableJoint;

                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;

                // joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                // joint.angularZMotion = ConfigurableJointMotion.Limited;

                SoftJointLimit newSoftJoint = joint.angularYLimit;
                newSoftJoint.limit = 40;
                joint.angularYLimit = newSoftJoint;

                // ------------------------------------------------------------------------------------------------------

                // CharacterJoint joint = current.AddComponent<CharacterJoint>() as CharacterJoint;
                // joint.enablePreprocessing = false;
                // joint.enableCollision = false;

                // SoftJointLimitSpring newSoftJoint = joint.twistLimitSpring;
                // newSoftJoint.spring = 20;
                // newSoftJoint.damper = 1000;
                // joint.twistLimitSpring = newSoftJoint;

                // newSoftJoint = joint.swingLimitSpring;
                // newSoftJoint.spring = 20;
                // newSoftJoint.damper = 1000;
                // joint.swingLimitSpring = newSoftJoint;

                joint.connectedBody = root.GetChild(i - 1).gameObject.GetComponent<Rigidbody>();
            }
        }
    }
}
