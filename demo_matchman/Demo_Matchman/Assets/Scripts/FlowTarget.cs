using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowTarget : MonoBehaviour {

    public Transform Target;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Mathf.Abs(transform.position.y - Target.position.y) > 3f)
            transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, Target.position.y, transform.position.z), Time.deltaTime * 3);
        else {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, transform.position.y, transform.position.z), Time.deltaTime * 3);
        }
    }
}
