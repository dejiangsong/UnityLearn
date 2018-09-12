using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;
    public float Speed = 10f;

    private Rigidbody rb;
    private bool isLocalPlayer;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        if (player == null)
            player = GetComponent<Player>();
    }

    // Use this for initialization
    void Start() {
        isLocalPlayer = player.isLocalPlayer;
    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            rb.velocity = new Vector3(h, rb.velocity.y, v) * Speed;
        }
    }
}
