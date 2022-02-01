using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float jumpForce = 400f;
    public LayerMask groundLayer;

    private new Rigidbody2D rigidbody2D;
    private bool grounded = true;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown("space") && grounded) {
            grounded = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (1 << col.gameObject.layer == groundLayer) {
            grounded = true;
        }
    }
}
