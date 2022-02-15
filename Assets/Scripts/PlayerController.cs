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

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private bool grounded = true;
    private bool canFire = true;

    void Start() {
        animator = GetComponent<Animator>();
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
            animator.Play("Player_walk_full", 0, 0);
        }
    }

    private void Jump() {
        grounded = false;
        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        animator.Play("Player_jump", 0, 0); // TODO select jump depending on canFire
    }

    private void Fire() {
        canFire = false;
        var obj = GameObject.Instantiate(bullet, firePosition.position, bullet.transform.rotation);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Bullet.bulletForce, 0f));
        animator.Play("Player_walk", 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        StartCoroutine(CountTillNextFire());
    }

    private IEnumerator CountTillNextFire() {
        yield return new WaitForSeconds(deltaFire);
        canFire = true;
        animator.Play("Player_walk_full", 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}
