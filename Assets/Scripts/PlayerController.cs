using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float jumpForce = 400f;
    public float deltaFire = 1.0f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public Transform firePosition;

    private new Rigidbody2D rigidbody2D;
    private bool grounded = true;
    private bool canFire = true;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!GameManager.Instance.GameIsOn()) return;
        if (!grounded) return;

        if (Input.GetKeyDown("space")) {
            Jump();
        } else if (Input.GetMouseButtonDown(0) && canFire) {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (1 << col.gameObject.layer == groundLayer) {
            grounded = true;
        }
    }

    private void Jump() {
        grounded = false;
        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
    }

    private void Fire() {
        canFire = false;
        var obj = GameObject.Instantiate(bullet, firePosition.position, bullet.transform.rotation);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Bullet.bulletForce, 0f));
        StartCoroutine(CountTillNextFire());
    }

    private IEnumerator CountTillNextFire() {
        yield return new WaitForSeconds(deltaFire);
        canFire = true;
    }
}
