using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform Target;
    public Vector2 Offset;

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(Target.position.x + Offset.x, Target.position.y + Offset.y, transform.position.z);
    }
}
