using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float jumpForce = 400f;
    public float bulletForce = 800f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public Transform firePosition;

    private new Rigidbody2D rigidbody2D;
    private bool grounded = true;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!grounded) return;

        if (Input.GetKeyDown("space")) {
            grounded = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        } else if (Input.GetMouseButtonDown(0)) {
            var obj = GameObject.Instantiate(bullet, firePosition.position, bullet.transform.rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce, 0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (1 << col.gameObject.layer == groundLayer) {
            grounded = true;
        }
    }
}
